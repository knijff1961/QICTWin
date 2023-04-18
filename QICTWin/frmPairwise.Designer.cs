namespace QICTWin
{
    partial class frmPairwise
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
            this.components = new System.ComponentModel.Container();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btnStart1 = new System.Windows.Forms.Button();
            this.chkResult = new System.Windows.Forms.CheckBox();
            this.btnStart2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkRegression = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPairsize = new System.Windows.Forms.TextBox();
            this.txtMessage1 = new System.Windows.Forms.TextBox();
            this.txtMessage2 = new System.Windows.Forms.TextBox();
            this.txtMessage3 = new System.Windows.Forms.TextBox();
            this.btnHistory = new System.Windows.Forms.Button();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defaultViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regressionViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reverseEngineringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fill1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fill2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fill3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fill4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fill5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 34);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(84, 26);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtText.Location = new System.Drawing.Point(12, 66);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(687, 138);
            this.txtText.TabIndex = 1;
            this.txtText.Text = "Param0: a1, b2\r\nParam1: c2, d2, e2, f2\r\nParam2: g3, h3, i3\r\nParam3: j4, k4\r\nParam" +
                "4: l5, m5, n5, o5\r\n";
            this.txtText.WordWrap = false;
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.ContextMenuStrip = this.contextMenuStrip1;
            this.txtResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(12, 210);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(687, 178);
            this.txtResults.TabIndex = 2;
            this.txtResults.WordWrap = false;
            // 
            // btnStart1
            // 
            this.btnStart1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart1.Location = new System.Drawing.Point(525, 28);
            this.btnStart1.Name = "btnStart1";
            this.btnStart1.Size = new System.Drawing.Size(84, 26);
            this.btnStart1.TabIndex = 3;
            this.btnStart1.Text = "Start 1";
            this.btnStart1.UseVisualStyleBackColor = true;
            this.btnStart1.Click += new System.EventHandler(this.btnStart1_Click);
            // 
            // chkResult
            // 
            this.chkResult.AutoSize = true;
            this.chkResult.Location = new System.Drawing.Point(102, 34);
            this.chkResult.Name = "chkResult";
            this.chkResult.Size = new System.Drawing.Size(73, 17);
            this.chkResult.TabIndex = 4;
            this.chkResult.Text = "Sort result";
            this.chkResult.UseVisualStyleBackColor = true;
            // 
            // btnStart2
            // 
            this.btnStart2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart2.Location = new System.Drawing.Point(615, 28);
            this.btnStart2.Name = "btnStart2";
            this.btnStart2.Size = new System.Drawing.Size(84, 26);
            this.btnStart2.TabIndex = 5;
            this.btnStart2.Text = "Start 2";
            this.btnStart2.UseVisualStyleBackColor = true;
            this.btnStart2.Click += new System.EventHandler(this.btnStart2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.fillToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(711, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open...";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regressionToolStripMenuItem,
            this.loggingToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // regressionToolStripMenuItem
            // 
            this.regressionToolStripMenuItem.Name = "regressionToolStripMenuItem";
            this.regressionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.regressionToolStripMenuItem.Text = "Regression...";
            this.regressionToolStripMenuItem.Click += new System.EventHandler(this.regressionToolStripMenuItem_Click);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loggingToolStripMenuItem.Text = "Logging...";
            this.loggingToolStripMenuItem.Click += new System.EventHandler(this.loggingToolStripMenuItem_Click);
            // 
            // chkRegression
            // 
            this.chkRegression.AutoSize = true;
            this.chkRegression.Location = new System.Drawing.Point(172, 34);
            this.chkRegression.Name = "chkRegression";
            this.chkRegression.Size = new System.Drawing.Size(79, 17);
            this.chkRegression.TabIndex = 7;
            this.chkRegression.Text = "Regression";
            this.chkRegression.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(322, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Pairsize:";
            // 
            // txtPairsize
            // 
            this.txtPairsize.Location = new System.Drawing.Point(374, 32);
            this.txtPairsize.Name = "txtPairsize";
            this.txtPairsize.Size = new System.Drawing.Size(42, 20);
            this.txtPairsize.TabIndex = 9;
            this.txtPairsize.Text = "2";
            // 
            // txtMessage1
            // 
            this.txtMessage1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage1.Location = new System.Drawing.Point(12, 210);
            this.txtMessage1.Name = "txtMessage1";
            this.txtMessage1.Size = new System.Drawing.Size(687, 20);
            this.txtMessage1.TabIndex = 10;
            this.txtMessage1.Visible = false;
            // 
            // txtMessage2
            // 
            this.txtMessage2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage2.Location = new System.Drawing.Point(12, 236);
            this.txtMessage2.Name = "txtMessage2";
            this.txtMessage2.Size = new System.Drawing.Size(687, 20);
            this.txtMessage2.TabIndex = 11;
            this.txtMessage2.Visible = false;
            // 
            // txtMessage3
            // 
            this.txtMessage3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage3.Location = new System.Drawing.Point(12, 262);
            this.txtMessage3.Name = "txtMessage3";
            this.txtMessage3.Size = new System.Drawing.Size(687, 20);
            this.txtMessage3.TabIndex = 12;
            this.txtMessage3.Visible = false;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.Location = new System.Drawing.Point(422, 28);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(84, 26);
            this.btnHistory.TabIndex = 13;
            this.btnHistory.Text = "History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Visible = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Location = new System.Drawing.Point(257, 34);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(44, 17);
            this.chkLog.TabIndex = 14;
            this.chkLog.Text = "Log";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultViewToolStripMenuItem,
            this.regressionViewToolStripMenuItem,
            this.cSVViewToolStripMenuItem,
            this.toolStripSeparator2,
            this.reverseEngineringToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 98);
            // 
            // defaultViewToolStripMenuItem
            // 
            this.defaultViewToolStripMenuItem.Name = "defaultViewToolStripMenuItem";
            this.defaultViewToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.defaultViewToolStripMenuItem.Text = "Default view";
            this.defaultViewToolStripMenuItem.Click += new System.EventHandler(this.defaultViewToolStripMenuItem_Click);
            // 
            // regressionViewToolStripMenuItem
            // 
            this.regressionViewToolStripMenuItem.Name = "regressionViewToolStripMenuItem";
            this.regressionViewToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.regressionViewToolStripMenuItem.Text = "Regression view";
            this.regressionViewToolStripMenuItem.Click += new System.EventHandler(this.regressionViewToolStripMenuItem_Click);
            // 
            // cSVViewToolStripMenuItem
            // 
            this.cSVViewToolStripMenuItem.Name = "cSVViewToolStripMenuItem";
            this.cSVViewToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.cSVViewToolStripMenuItem.Text = "CSV view";
            this.cSVViewToolStripMenuItem.Click += new System.EventHandler(this.cSVViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // reverseEngineringToolStripMenuItem
            // 
            this.reverseEngineringToolStripMenuItem.Name = "reverseEngineringToolStripMenuItem";
            this.reverseEngineringToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.reverseEngineringToolStripMenuItem.Text = "Reverse enginering...";
            this.reverseEngineringToolStripMenuItem.Click += new System.EventHandler(this.reverseEngineringToolStripMenuItem_Click);
            // 
            // fillToolStripMenuItem
            // 
            this.fillToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fill1ToolStripMenuItem,
            this.fill2ToolStripMenuItem,
            this.fill3ToolStripMenuItem,
            this.fill4ToolStripMenuItem,
            this.fill5ToolStripMenuItem});
            this.fillToolStripMenuItem.Name = "fillToolStripMenuItem";
            this.fillToolStripMenuItem.Size = new System.Drawing.Size(31, 20);
            this.fillToolStripMenuItem.Text = "Fill";
            // 
            // fill1ToolStripMenuItem
            // 
            this.fill1ToolStripMenuItem.Name = "fill1ToolStripMenuItem";
            this.fill1ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fill1ToolStripMenuItem.Text = "Fill 1 (4 params)";
            this.fill1ToolStripMenuItem.Click += new System.EventHandler(this.fill1ToolStripMenuItem_Click);
            // 
            // fill2ToolStripMenuItem
            // 
            this.fill2ToolStripMenuItem.Name = "fill2ToolStripMenuItem";
            this.fill2ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fill2ToolStripMenuItem.Text = "Fill 2 (extra value)";
            this.fill2ToolStripMenuItem.Click += new System.EventHandler(this.fill2ToolStripMenuItem_Click);
            // 
            // fill3ToolStripMenuItem
            // 
            this.fill3ToolStripMenuItem.Name = "fill3ToolStripMenuItem";
            this.fill3ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fill3ToolStripMenuItem.Text = "Fill 3 (lost value)";
            this.fill3ToolStripMenuItem.Click += new System.EventHandler(this.fill3ToolStripMenuItem_Click);
            // 
            // fill4ToolStripMenuItem
            // 
            this.fill4ToolStripMenuItem.Name = "fill4ToolStripMenuItem";
            this.fill4ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fill4ToolStripMenuItem.Text = "Fill 4 (extra parameter)";
            this.fill4ToolStripMenuItem.Click += new System.EventHandler(this.fill4ToolStripMenuItem_Click);
            // 
            // fill5ToolStripMenuItem
            // 
            this.fill5ToolStripMenuItem.Name = "fill5ToolStripMenuItem";
            this.fill5ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.fill5ToolStripMenuItem.Text = "Fill 5 (lost parameter)";
            this.fill5ToolStripMenuItem.Click += new System.EventHandler(this.fill5ToolStripMenuItem_Click);
            // 
            // frmPairwise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 400);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.txtMessage3);
            this.Controls.Add(this.txtMessage2);
            this.Controls.Add(this.txtMessage1);
            this.Controls.Add(this.txtPairsize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkRegression);
            this.Controls.Add(this.btnStart2);
            this.Controls.Add(this.chkResult);
            this.Controls.Add(this.btnStart1);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPairwise";
            this.Text = "QICTWin";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btnStart1;
        private System.Windows.Forms.CheckBox chkResult;
        private System.Windows.Forms.Button btnStart2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regressionToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkRegression;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPairsize;
        private System.Windows.Forms.TextBox txtResults;
        internal System.Windows.Forms.TextBox txtMessage1;
        internal System.Windows.Forms.TextBox txtMessage2;
        internal System.Windows.Forms.TextBox txtMessage3;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem defaultViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regressionViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem reverseEngineringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fill1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fill2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fill3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fill4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fill5ToolStripMenuItem;
    }
}

