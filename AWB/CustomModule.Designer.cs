namespace AutoWikiBrowser
{
    partial class CustomModule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomModule));
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnMake = new System.Windows.Forms.Button();
            this.cmboLang = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkModuleEnabled = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkFixedwidth = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Font = new System.Drawing.Font("Courier New", 9F);
            this.txtCode.Location = new System.Drawing.Point(12, 231);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(640, 184);
            this.txtCode.TabIndex = 0;
            this.txtCode.Text = resources.GetString("txtCode.Text");
            this.txtCode.WordWrap = false;
            // 
            // btnDone
            // 
            this.btnDone.Enabled = false;
            this.btnDone.Location = new System.Drawing.Point(544, 30);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnMake
            // 
            this.btnMake.Enabled = false;
            this.btnMake.Location = new System.Drawing.Point(451, 30);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(87, 23);
            this.btnMake.TabIndex = 2;
            this.btnMake.Text = "Make module";
            this.btnMake.UseVisualStyleBackColor = true;
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // cmboLang
            // 
            this.cmboLang.FormattingEnabled = true;
            this.cmboLang.Items.AddRange(new object[] {
            "C# 2.0",
            "VB .NET 2.0"});
            this.cmboLang.Location = new System.Drawing.Point(308, 32);
            this.cmboLang.Name = "cmboLang";
            this.cmboLang.Size = new System.Drawing.Size(137, 21);
            this.cmboLang.TabIndex = 3;
            this.cmboLang.SelectedIndexChanged += new System.EventHandler(this.cmboLang_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Language";
            // 
            // lblStart
            // 
            this.lblStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStart.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(12, 76);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(643, 152);
            this.lblStart.TabIndex = 5;
            this.lblStart.Text = resources.GetString("lblStart.Text");
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblEnd
            // 
            this.lblEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnd.Font = new System.Drawing.Font("Courier New", 9F);
            this.lblEnd.Location = new System.Drawing.Point(9, 418);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(643, 39);
            this.lblEnd.TabIndex = 6;
            this.lblEnd.Text = "    }\r\n}";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblStatus.Location = new System.Drawing.Point(82, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(93, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "No module loaded";
            // 
            // chkModuleEnabled
            // 
            this.chkModuleEnabled.AutoSize = true;
            this.chkModuleEnabled.Location = new System.Drawing.Point(11, 34);
            this.chkModuleEnabled.Name = "chkModuleEnabled";
            this.chkModuleEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkModuleEnabled.TabIndex = 8;
            this.chkModuleEnabled.Text = "Enabled";
            this.chkModuleEnabled.UseVisualStyleBackColor = true;
            this.chkModuleEnabled.CheckedChanged += new System.EventHandler(this.chkModuleEnabled_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(664, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guideToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // guideToolStripMenuItem
            // 
            this.guideToolStripMenuItem.Name = "guideToolStripMenuItem";
            this.guideToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.guideToolStripMenuItem.Text = "Guide";
            this.guideToolStripMenuItem.Click += new System.EventHandler(this.guideToolStripMenuItem_Click);
            // 
            // chkFixedwidth
            // 
            this.chkFixedwidth.AutoSize = true;
            this.chkFixedwidth.Checked = true;
            this.chkFixedwidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFixedwidth.Location = new System.Drawing.Point(11, 56);
            this.chkFixedwidth.Name = "chkFixedwidth";
            this.chkFixedwidth.Size = new System.Drawing.Size(100, 17);
            this.chkFixedwidth.TabIndex = 10;
            this.chkFixedwidth.Text = "Fixed width font";
            this.chkFixedwidth.UseVisualStyleBackColor = true;
            this.chkFixedwidth.CheckedChanged += new System.EventHandler(this.chkFixedwidth_CheckedChanged);
            // 
            // CustomModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 466);
            this.Controls.Add(this.chkFixedwidth);
            this.Controls.Add(this.chkModuleEnabled);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboLang);
            this.Controls.Add(this.btnMake);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CustomModule";
            this.ShowIcon = false;
            this.Text = "Module";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CSParser_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnMake;
        private System.Windows.Forms.ComboBox cmboLang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkModuleEnabled;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guideToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkFixedwidth;
    }
}