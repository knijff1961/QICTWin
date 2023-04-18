using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QICTWin
{
    public partial class frmPairwise : Form
    {
        public static frmPairwise FORM;
        clsPairwiseParams cPairwiseResults;
        frmRegression frm = new frmRegression();

        string szRegression = "";

        string[] aszStart1 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3, i3",
            "Param3: j4, k4"
        };
        string[] aszStart2 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3, i3",
            "Param3: j4, k4",
            "Param4: l5, m5, n5, o5"
        };

        public frmPairwise()
        {
            InitializeComponent();
            txtText.Text = genStart(aszStart1);
            FORM = this;
            szRegression = frm.txtRegression.Text;
        }
        private string genStart(string[] aszStart)
        {
            string szRet = "";
            for (int iCnt = 0; iCnt < aszStart.Length; iCnt++)
                szRet += aszStart[iCnt] + "\r\n";
            return szRet;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtMessage1.Visible = true;
            txtMessage2.Visible = true;
            txtMessage3.Visible = true;
            txtResults.Visible = false;
            btnHistory.Visible = false;

            txtMessage1.Text = "Start generating tests";
            Application.DoEvents();

            string szRegression = "";
            if (chkRegression.Checked)
                szRegression = this.szRegression;

            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Text = txtText.Text, Log = chkLog.Checked, Regression = szRegression };

            cPairwiseResults = clsPairwise.Pairwise(cPairWiseArgs);

            if (cPairwiseResults == null)
            {
                txtResults.Text = "Error generating tests";
            }
            else
            {
                if (chkResult.Checked)
                    cPairwiseResults.sort();

                string szText = cPairwiseResults.genText();
                txtResults.Text = szText;
                //textBoxText2CSharp(txtText.Text);
                //textBoxText2CSharp(cPairwiseResults.genRegressionText());
            }
            txtMessage1.Visible = false;
            txtMessage2.Visible = false;
            txtMessage3.Visible = false;
            txtResults.Visible = true;

            if (cPairwiseResults != null)
            {
                if (cPairwiseResults.lstHistoryInfo.Count > 0)
                {
                    btnHistory.Visible = true;
                }
                if (cPairwiseResults.lstRegressionInfo.Count > 0)
                {
                    string szTmp = "";
                    for (int iCnt = 0; iCnt < cPairwiseResults.lstRegressionInfo.Count; iCnt++)
                        szTmp += "\r\n" + cPairwiseResults.lstRegressionInfo[iCnt];
                    Console.WriteLine(szTmp.Substring(2));
                    MessageBox.Show(szTmp.Substring(2), "Regression Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }

        private string textBoxText2CSharp(string szText)
        {
            string szRet = "        string[] asz# = {";

            szText = szText.Replace("\r", "\n");
            szText = szText.Replace("\n\n", "\n");
            string[] aLines = szText.Split('\n');
            foreach (string szLine in aLines)
            {
                if (szLine.Length != 0)
                    szRet += "\r\n            \"" + szLine + "\",";
            }
            szRet = szRet.Substring(0, szRet.Length - 1);
            szRet += "\r\n        };\r\n\r\n";
            Console.WriteLine(szRet);
            return szRet;
        }
        private void btnStart1_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(aszStart1);
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(aszStart2);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void regressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm.txtRegression.Text = szRegression;
            frm.ShowDialog();
            szRegression = frm.txtRegression.Text;
        }

        private void loggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogging frm = new frmLogging();
            frm.ShowDialog();

        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if (cPairwiseResults.lstHistoryInfo.Count > 0)
                {
                    txtMessage1.Visible = true;
                    txtMessage2.Visible = false;
                    txtMessage3.Visible = false;
                    txtResults.Visible = false;
                    btnHistory.Visible = false;

                    string szTmp = "";
                    for (int iCnt = 0; iCnt < cPairwiseResults.lstHistoryInfo.Count; iCnt++)
                    {
                        txtMessage1.Text = "Preparing text (" + iCnt + " of " + cPairwiseResults.lstHistoryInfo.Count + ")";
                        Application.DoEvents();
                        szTmp += "\r\n" + cPairwiseResults.lstHistoryInfo[iCnt];
                    }
                    txtMessage1.Visible = false;
                    txtMessage2.Visible = false;
                    txtMessage3.Visible = false;
                    txtResults.Visible = true;
                    btnHistory.Visible = true;

                    frmHistory frm = new frmHistory();
                    frm.txtHistory.Text = szTmp;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
            }
            txtMessage1.Visible = false;
            txtMessage2.Visible = false;
            txtMessage3.Visible = false;
            txtResults.Visible = true;
            btnHistory.Visible = true;

        }

        private void defaultViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string szText = cPairwiseResults.genText();
            txtResults.Text = szText;
        }

        private void regressionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string szText = cPairwiseResults.genRegressionText();
            txtResults.Text = szText;
        }

        private void cSVViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reverseEngineringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string szText = cPairwiseResults.reverseEnginering();
            txtResults.Text = szText;
        }

        private void fill1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(presets.asz1);
        }

        private void fill2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(presets.asz2);
        }

        private void fill3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(presets.asz3);
        }

        private void fill4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(presets.asz4);
        }

        private void fill5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtText.Text = genStart(presets.asz5);
        }


    }
}
