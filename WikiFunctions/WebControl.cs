/*
Autowikibrowser
Copyright (C) 2006 Martin Richards

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web;

namespace WikiFunctions.Browser
{
    public delegate void WebControlDel();

    public enum enumProcessStage : byte { load, diff, save, delete, none }

    /// <summary>
    /// Provides a webBrowser component adapted speciailly to work with Wikis.
    /// </summary>
    public partial class WebControl : WebBrowser
    {
        #region Constructor etc.

        public WebControl()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Enabled = true;

            this.ScriptErrorsSuppressed = true;
            ProcessStage = enumProcessStage.none;
        }

        Regex LoginRegex = new Regex("var wgUserName = (.*?);", RegexOptions.Compiled);

        Timer timer1 = new Timer();

        /// <summary>
        /// Occurs when the edit page has finished loading
        /// </summary>
        public event WebControlDel Loaded;
        /// <summary>
        /// Occurs when the page has finished saving
        /// </summary>
        public event WebControlDel Saved;
        /// <summary>
        /// Occurs when the diff or preview page has finished loading
        /// </summary>
        public event WebControlDel Diffed;
        /// <summary>
        /// Occurs when the page has finished deleting
        /// </summary>
        public event WebControlDel Deleted;
        /// <summary>
        /// Occurs when the page has finished loading, and it was not a save/load/diff
        /// </summary>
        public event WebControlDel None;
        /// <summary>
        /// Occurs when the page failed to load properly
        /// </summary>
        public event WebControlDel Fault;
        /// <summary>
        /// Occurs when the status changes
        /// </summary>
        public event WebControlDel StatusChanged;
        /// <summary>
        /// Occurs when the Busy state changes
        /// </summary>
        public event WebControlDel BusyChanged;

        #endregion

        #region Properties

        public enumProcessStage pStage = new enumProcessStage();
        public enumProcessStage ProcessStage
        {
            get { return pStage; }
            private set { pStage = value; }
        }

        /// <summary>
        /// Gets a value indicating whether the page can be saved
        /// </summary>
        public bool CanSave
        {
            get
            {
                if (this.Document != null && this.Document.GetElementById("wpSave") != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the page can be diffed
        /// </summary>
        public bool CanDiff
        {
            get
            {
                if (this.Document != null && this.Document.GetElementById("wpDiff") != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the page can be previewed
        /// </summary>
        public bool CanPreview
        {
            get
            {
                if (this.Document != null && this.Document.GetElementById("wpPreview") != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the page can be deleted
        /// </summary>
        public bool CanDelete
        {
            get
            {
                if (this.Document != null && this.Document.GetElementById("wpConfirmB") != null)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user is logged in
        /// </summary>
        public bool LoggedIn
        {
            get
            {
                if (this.Document == null)
                    return false;

                Match m = LoginRegex.Match(this.DocumentText);

                if (!m.Success || m.Groups[1].Value == "null")
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Gets the user name if logged in
        /// </summary>
        public string UserName
        {
            get
            {
                if (this.Document == null)
                    return "";

                Match m = LoginRegex.Match(this.DocumentText);

                if (m.Groups[1].Value == "null")
                    return "";

                return m.Groups[1].Value.Trim('"');
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is a new message
        /// </summary>
        public bool NewMessage
        {
            get
            {
                if (this.Document == null)
                    return false;

                string HTMLsub = "";

                if (this.Document.Body.InnerHtml.Contains("<!-- start content -->"))
                    HTMLsub = this.Document.Body.InnerHtml.Remove(this.Document.Body.InnerHtml.IndexOf("<!-- start content -->"));
                else
                    HTMLsub = this.Document.Body.InnerHtml;

                if (HTMLsub.Contains("<DIV class=usermessage>"))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the textbox is present
        /// </summary>
        public bool HasArticleTextBox
        {
            get
            {
                if (this.Document != null && this.Document.GetElementById("wpTextbox1") != null)
                    return true;
                else
                    return false;
            }
        }

        string strStatus = "";
        /// <summary>
        /// Gets a string indicating the current status
        /// </summary>
        public string Status
        {
            get
            {
                return strStatus;
            }
            private set
            {
                strStatus = value;

                if(this.StatusChanged != null)
                    this.StatusChanged();
            }
        }

        bool boolBusy = false;
        /// <summary>
        /// Gets a value indicating whether articles are still being processed
        /// </summary>
        public bool Busy
        {
            get { return boolBusy; }
            set
            {
                bool b = boolBusy;
                boolBusy = value;
                if (b != boolBusy && BusyChanged != null)
                    BusyChanged();
            }
        }

        /// <summary>
        /// Gets a bool indicating if the current page is a diff
        /// </summary>
        public bool IsDiff
        {
            get
            {
                if (this.Document.Body.InnerHtml.Contains("<DIV id=wikiDiff>"))
                    return true;
                else
                    return false;
            }
        }

        int intDiffFontSize = 150;
        /// <summary>
        /// Gets or sets the font size of the diff text
        /// </summary>
        public int DiffFontSize
        {
            get { return intDiffFontSize; }
            set { intDiffFontSize = value; }
        }

        bool bEnhanceDiff = true;
        /// <summary>
        /// Gets or sets a value indicating whether to use the enhanced diff
        /// </summary>
        public bool EnhanceDiffEnabled
        {
            get { return bEnhanceDiff; }
            set { bEnhanceDiff = value; }
        }

        bool bScrollDown = true;
        /// <summary>
        /// Gets or sets a value indicating whether to scroll down to the diff
        /// </summary>
        public bool ScrollDown
        {
            get { return bScrollDown; }
            set { bScrollDown = value; }
        }

        bool boolTalkExists = true;
        /// <summary>
        /// Gets a value indicating if the associated talk page exists
        /// </summary>
        public bool TalkPageExists
        {
            get { return boolTalkExists; }
            private set { boolTalkExists = value; }

        }

        bool boolArticlePageExists = true;
        /// <summary>
        /// Gets a value indicating if the associated article page exists
        /// </summary>
        public bool ArticlePageExists
        {
            get { return boolArticlePageExists; }
            private set { boolArticlePageExists = value; }
        }

        #endregion

        #region Methods

        public void Stop2()
        {
            StopTimer();
            ProcessStage = enumProcessStage.none;
            Busy = false;
            this.Stop();
        }

        /// <summary>
        /// Gets the text from the textbox
        /// </summary>
        public string GetArticleText()
        {
            if (HasArticleTextBox)
            {
                string txt = this.Document.GetElementById("wpTextbox1").InnerText;
                if (txt == null)
                    return "";
                else
                    return txt;
            }
            else
                return "";
        }

        /// <summary>
        /// Sets the article text
        /// </summary>
        public void SetArticleText(string ArticleText)
        {
            if (HasArticleTextBox)
            {
                this.Document.GetElementById("wpTextbox1").Enabled = true;
                this.Document.GetElementById("wpTextbox1").InnerText = ArticleText.Trim();
            }
        }

        /// <summary>
        /// Sets the minor checkbox
        /// </summary>
        public void SetMinor(bool IsMinor)
        {
            if (this.Document == null || !this.Document.Body.InnerHtml.Contains("wpMinoredit"))
                return;

            if (IsMinor)
                this.Document.GetElementById("wpMinoredit").SetAttribute("checked", "checked");
            else
                this.Document.GetElementById("wpMinoredit").SetAttribute("checked", "");
        }

        /// <summary>
        /// Sets the watch checkbox
        /// </summary>
        public void SetWatch(bool Watch)
        {
            if (this.Document == null || !this.Document.Body.InnerHtml.Contains("wpWatchthis"))
                return;

            if (Watch)
                this.Document.GetElementById("wpWatchthis").SetAttribute("checked", "checked");
            else
                this.Document.GetElementById("wpWatchthis").SetAttribute("checked", "");
        }

        /// <summary>
        /// Sets the edit summary text
        /// </summary>
        public void SetSummary(string Summary)
        {
            if (this.Document == null || !this.Document.Body.InnerHtml.Contains("wpSummary"))
                return;

            this.Document.GetElementById("wpSummary").InnerText = Summary;
        }

        /// <summary>
        /// Sets the reason given for deletion
        /// </summary>
        public void SetDeleteReason(string Reason)
        {
            if (this.Document == null || !this.Document.Body.InnerHtml.Contains("wpReason"))
                return;

            this.Document.GetElementById("wpReason").InnerText = Reason;
        }

        /// <summary>
        /// Gets a value indicating whether the minor checkbox is checked
        /// </summary>
        public bool IsMinor()
        {
            if (this.Document == null || this.Document.GetElementById("wpMinoredit").GetAttribute("checked") != "True")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Gets a value indicating whether the watch checkbox is checked
        /// </summary>
        public bool IsWatched()
        {
            if (this.Document == null || this.Document.GetElementById("wpWatchthis").GetAttribute("checked") != "True")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Gets the entered edit summary
        /// </summary>
        public string GetSummary()
        {
            if (this.Document == null || !this.Document.Body.InnerHtml.Contains("wpSummary"))
                return "";

            return this.Document.GetElementById("wpSummary").InnerText;
        }

        string startMark = "<!-- start content -->";
        string endMark = "<!-- end content -->";

        /// <summary>
        /// Gets the HTML within the <!-- start content --> and <!-- end content --> tags
        /// </summary>
        public string PageHTMLSubstring(string text)
        {
            if (this.Document.Body.InnerHtml.Contains(startMark) && this.Document.Body.InnerHtml.Contains(endMark))
                return text.Substring(text.IndexOf(startMark), text.IndexOf(endMark) - text.IndexOf(startMark));
            else
                return text;
        }

        /// <summary>
        /// Removes excess HTML from the document
        /// </summary>
        public void EnhanceDiff()
        {
            string html = this.Document.Body.InnerHtml;
            html = PageHTMLSubstring(html);
            html = html.Replace("<DIV id=wikiDiff>", "<DIV id=wikiDiff style=\"FONT-SIZE: " + DiffFontSize.ToString() + "%\">");
            html = html.Replace("<TABLE class=diff cellSpacing=4 cellPadding=0 width=\"98%\" border=0>", "<TABLE class=diff cellSpacing=2 cellPadding=0 width=\"100%\" border=0>");
            this.Document.Body.InnerHtml = html;
        }

        public void ScrollToContent()
        {
            this.Document.GetElementById("contentSub").ScrollIntoView(true);
        }

        #endregion

        #region Save/load/diff methods and events

        /// <summary>
        /// Invokes the Save button
        /// </summary>
        public void Save()
        {
            if (CanSave)
            {
                this.AllowNavigation = true;
                ProcessStage = enumProcessStage.save;
                Status = "Saving";
                this.Document.GetElementById("wpSave").InvokeMember("click");
            }
        }

        /// <summary>
        /// Invokes the Show changes button
        /// </summary>
        public void ShowDiff()
        {
            if (CanDiff)
            {
                this.AllowNavigation = true;
                ProcessStage = enumProcessStage.diff;
                Status = "Loading changes";
                this.Document.GetElementById("wpDiff").InvokeMember("click");
            }
        }

        /// <summary>
        /// Invokes the Preview button
        /// </summary>
        public void ShowPreview()
        {
            if (CanPreview)
            {
                this.AllowNavigation = true;
                ProcessStage = enumProcessStage.diff;
                Status = "Loading preview";
                this.Document.GetElementById("wpPreview").InvokeMember("click");
            }
        }

        /// <summary>
        /// Invokes the Delete button
        /// </summary>
        public void Delete()
        {
            if (CanDelete)
            {
                this.AllowNavigation = true;
                ProcessStage = enumProcessStage.delete;
                Status = "Deleting page";
                this.Document.GetElementById("wpConfirmB").InvokeMember("click");
            }
        }

        /// <summary>
        /// Loads the edit page of the given article
        /// </summary>
        public void LoadEditPage(string Article)
        {
            try
            {
                this.AllowNavigation = true;
                ProcessStage = enumProcessStage.load;
                Status = "Loading page";
                this.Navigate(Variables.URLLong + "index.php?title=" + Article + "&action=edit");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Loads the log in page
        /// </summary>
        public void LoadLogInPage()
        {
            this.AllowNavigation = true;
            ProcessStage = enumProcessStage.none;
            Status = "Loading log in page";
            this.Navigate(Variables.URLLong + "index.php?title=Special:Userlogin&returnto=Main_Page");
            Busy = false;
        }

        Regex RegexArticleExists = new Regex("<LI (class=new|class=\"selected new\") id=ca-nstab", RegexOptions.Compiled);
        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
            base.OnDocumentCompleted(e);
            StopTimer();

            if (!this.Document.Body.InnerHtml.Contains("id=siteSub"))
            {

                ProcessStage = enumProcessStage.none;
                if (Fault != null)
                    this.Fault();
                return;
            }

            if (ProcessStage == enumProcessStage.load)
            {
                if (this.Document.Body.InnerHtml.Contains("<LI class=new id=ca-talk>") || this.Document.Body.InnerHtml.Contains("<LI class=\"selected new\" id=ca-talk>"))
                    TalkPageExists = false;
                else
                    TalkPageExists = true;

                if (RegexArticleExists.IsMatch(this.Document.Body.InnerHtml))
                    ArticlePageExists = false;
                else
                    ArticlePageExists = true;

                this.AllowNavigation = false;
                ProcessStage = enumProcessStage.none;
                if (Loaded != null)
                    this.Loaded();
            }
            else if (ProcessStage == enumProcessStage.delete)
            {
                this.AllowNavigation = false;
                ProcessStage = enumProcessStage.none;
                Status = "Deleted";
                if (Deleted != null)
                    this.Deleted();
            }
            else if (ProcessStage == enumProcessStage.diff)
            {
                if (EnhanceDiffEnabled && IsDiff)
                    EnhanceDiff();
                else if (ScrollDown)
                    ScrollToContent();

                this.AllowNavigation = false;
                ProcessStage = enumProcessStage.none;
                Status = "Ready to save";
                if (Diffed != null)
                    this.Diffed();
                //RemoveHTML();
                this.Document.GetElementById("wpTextbox1").Enabled = false;
            }
            else if (ProcessStage == enumProcessStage.none)
            {
                if (None != null)
                    this.None();
            }
        }

        protected override void OnProgressChanged(WebBrowserProgressChangedEventArgs e)
        {
            if (this.ReadyState == WebBrowserReadyState.Interactive && ProcessStage == enumProcessStage.save)
            {
                StopTimer();
                this.OnDocumentCompleted(null);
                this.AllowNavigation = false;
                ProcessStage = enumProcessStage.none;
                this.Stop();
                if (this.Saved != null)
                    this.Saved();
            }
            base.OnProgressChanged(e);
        }

        #endregion

        protected override void OnNavigating(WebBrowserNavigatingEventArgs e)
        {
            base.OnNavigating(e);
            StartTimer();
        }

        int LoadTime = 0;
        private void StartTimer()
        {
            LoadTime = 0;
            timer1.Tick += IncrememntTime;
        }

        private void StopTimer()
        {
            timer1.Tick -= IncrememntTime;
            LoadTime = 0;
        }

        private void IncrememntTime(object sender, EventArgs e)
        {
            LoadTime++;

            if (LoadTime == timeout)
            {
                StopTimer();
                Stop2();
                Status = "Timed out";
                if (this.Fault != null)
                    this.Fault();
            }
        }

        int timeout = 30;
        public int TimeoutLimit
        {
            get { return timeout; }
            set { timeout = value; }
        }

    }
}
