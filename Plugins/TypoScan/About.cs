﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WikiFunctions.Plugins.ListMaker.TypoScan
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            lblSaved.Text = TypoScanAWBPlugin.SavedPagesThisSession.Count.ToString();
            lblSkipped.Text = TypoScanAWBPlugin.SkippedPagesThisSession.Count.ToString();
            lblLoaded.Text = TypoScanAWBPlugin.PageList.Count.ToString();
            lblUploaded.Text = TypoScanAWBPlugin.UploadedThisSession.ToString();
            lblToUpload.Text = TypoScanAWBPlugin.EditAndIgnoredPages.ToString();
        }

        private void linkMboverload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkMboverload.LinkVisited = true;
            Tools.OpenENArticleInBrowser("Mboverload", true);
        }

        private void linkTypoScanPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkTypoScanPage.LinkVisited = true;
            Tools.OpenENArticleInBrowser("Wikipedia:TypoScan", false);
        }

        private void linkStats_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkStats.LinkVisited = true;
            Tools.OpenURLInBrowser(Common.GetUrlFor("stats"));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
