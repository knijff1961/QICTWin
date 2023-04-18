// <copyright file="frmPairwise.cs" company="Fouroaks">
// Copyright (c) 2007, 2008 All Right Reserved, http://www.fouroaks.nl/
//
// This source is subject to the Microsoft Permissive License.
// Please see the License.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>
// <author>Peter Knijff</author>
// <email>peter.knijff@fouroaks.nl</email>
// <date>2006-2010</date>
//
﻿using System;
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
        clsResults cPairwiseResults;
        frmRegression frm = new frmRegression();
        string szExpected = "";

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
            txtUserText.Text = genStart(aszStart1);
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
            string szRegression = "";
            if (chkRegression.Checked)
                szRegression = this.szRegression;

            //szRegression = "Param0,Param1,Param2,Param3\r\na1,c2,g3,j4\r\nb2,c2,h3,k4\r\na1,d2,i3,k4\r\nb2,e2,i3,j4\r\nb2,f2,g3,k4\r\na1,d2,h3,j4\r\na1,e2,g3,k4\r\na1,f2,h3,j4\r\na1,c2,i3,j4\r\nb2,d2,g3,j4\r\na1,e2,h3,j4\r\na1,f2,i3,j4";
            //szRegression = "Param0,Param1,Param2,Param3\r\na1,c2,g3,j4\r\nb2,c2,h3,k4\r\na1,d2,i3,k4\r\nb2,e2,i3,j4\r\nb2,f2,g3,k4\r\na1,d2,h3,j4\r\na1,e2,g3,k4\r\na1,f2,h3,j4\r\na1,c2,i3,j4\r\nb2,d2,g3,j4\r\na1,e2,h3,j4\r\na1,f2,i3,j4";

            string szTestSet = txtUserText.Text;
            string szExpect = "       Param0 Param1 Param2 Param3\r\n\r\n+ 0    a1     c2     g3     j4    \r\n+ 1    b2     c2     h3     k4    \r\n+ 2    a1     d2     i3     k4    \r\n+ 3    b2     e2     i3     j4    \r\n+ 4    b2     f2     g3     k4    \r\n+ 5    a1     d2     h3     j4    \r\n+ 6    a1     e2     g3     k4    \r\n+ 7    a1     f2     h3     j4    \r\n+ 8    a1     c2     i3     j4    \r\n+ 9    b2     d2     g3     j4    \r\n+ 10   a1     e2     h3     j4    \r\n+ 11   a1     f2     i3     j4";

            doGenerate(szTestSet, szRegression, szExpect);
        }

        private void doGenerate(string szTestSet, string szRegression, string szExpect)
        {
            clsGlobal.lstLogHistory = new List<string>();
            //Console.WriteLine("@TestSet");
            //Console.WriteLine(szTestSet);
            //Console.WriteLine("@Regression");
            //Console.WriteLine(szRegression);
            //Console.WriteLine("@Expect");
            //Console.WriteLine(szExpect);
            txtMessage1.Visible = true;
            txtMessage2.Visible = true;
            txtMessage3.Visible = true;
            txtResults.Visible = false;
            btnHistory.Visible = false;

            txtMessage1.Text = "Start generating tests";
            Application.DoEvents();

            int iPoolsize = 2;
            int.TryParse(txtLoopSearch.Text, out iPoolsize);
            bool bPseudoRandom = chkPseudoRandom.Checked;

            int iDepth = int.Parse(txtPairsize.Text);
            clsUserTestSet cUserTestSet = new clsUserTestSet(szTestSet, iDepth);
            clsRegression cRegTestSet = new clsRegression(szRegression, cUserTestSet);
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Log = chkLog.Checked, Depth = iDepth, Poolsize = iPoolsize, PseudoRandom = bPseudoRandom };

            clsGlobal.lstLogHistory = new List<string>();

            cPairwiseResults = clsPairwise.Pairwise(cUserTestSet, cRegTestSet, cPairWiseArgs);

            clsLog cLog = new clsLog();
            cLog.testResults(cUserTestSet, cRegTestSet, cPairwiseResults);
            if (cPairwiseResults == null)
            {
                txtResults.Text = "Error generating tests";
            }
            else
            {
                string szText = "";
                if (szExpect == null)
                    szExpect = "";
                szExpect = szExpect.Trim();

                //if (szExpect != null)
                //{
                //    //szText = cPairwiseResults.genText().Trim();
                //    if (!szText.Equals(szExpect))
                //    {
                //        //for (int icnt = 0; icnt < szText.Length; icnt++)
                //        //{
                //        //    Console.Write(szText[icnt]);
                //        //    if (szText[icnt] != szExpect[icnt])
                //        //    {
                //        //        szText = "";
                //        //        break;
                //        //    }
                //        //}
                //        szText = "WITH ERRORS\r\n";
                //    }
                //    else
                //        szText = "";
                //}
                //if (chkResult.Checked)
                //    cPairwiseResults.sort();

                szText += cPairwiseResults.genText();

                txtResults.Text = szText;
            }
            txtMessage1.Visible = false;
            txtMessage2.Visible = false;
            txtMessage3.Visible = false;
            txtResults.Visible = true;

            if (cPairwiseResults != null)
            {
                if (clsGlobal.lstLogHistory.Count > 0)
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
            List<string> lstFiles = getFile(@"C:\Exchange\Develop\QICTWin\TestFiles\Test1.txt");

            //doGenerate(lstFiles[0],lstFiles[1],lstFiles[2]);
            txtUserText.Text = lstFiles[0];
            txtRegression.Text = lstFiles[1];
            szExpected = lstFiles[2];
        }
        private void btnStart1b_Click(object sender, EventArgs e)
        {
            List<string> lstFiles = getFile(@"C:\Exchange\Develop\QICTWin\TestFiles\Test_v_p.txt");

            //doGenerate(lstFiles[0],lstFiles[1],lstFiles[2]);
            txtUserText.Text = lstFiles[0];
            txtRegression.Text = lstFiles[1];
            szExpected = lstFiles[2];
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            doGenerate(txtUserText.Text, txtRegression.Text, szExpected);
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
                if (clsGlobal.lstLogHistory.Count > 0)
                {
                    txtMessage1.Visible = true;
                    txtMessage2.Visible = false;
                    txtMessage3.Visible = false;
                    txtResults.Visible = false;
                    btnHistory.Visible = false;

                    string szTmp = "";
                    for (int iCnt = 0; iCnt < clsGlobal.lstLogHistory.Count; iCnt++)
                    {
                        txtMessage1.Text = "Preparing text (" + iCnt + " of " + clsGlobal.lstLogHistory.Count + ")";
                        Application.DoEvents();
                        szTmp += "\r\n" + clsGlobal.lstLogHistory[iCnt];
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
        //    string szText = cPairwiseResults.genText();
        //    txtResults.Text = szText;
        }

        private void regressionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string szText = cPairwiseResults.genRegressionText();
            //txtResults.Text = szText;
        }

        private void cSVViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reverseEngineringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string szText = cPairwiseResults.reverseEnginering();
            //txtResults.Text = szText;
        }

        private void fill1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUserText.Text = genStart(presets.asz1);
        }

        private void fill2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUserText.Text = genStart(presets.asz2);
        }

        private void fill3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUserText.Text = genStart(presets.asz3);
        }

        private void fill4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUserText.Text = genStart(presets.asz4);
        }

        private void fill5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUserText.Text = genStart(presets.asz5);
        }

        public List<string> getFile(string szFile)
        {
            List<string> aszRet = new List<string>();
            aszRet.Add("");
            aszRet.Add("");
            aszRet.Add("");

            int iPos = -1;
            System.IO.TextReader txSrcFile = new System.IO.StreamReader(szFile);

            while (true)
            {
                string szLine = txSrcFile.ReadLine();
                if (szLine == null)
                    break;
                if (szLine.StartsWith("@"))
                {
                    switch (szLine)
                    {
                        case "@TestSet":
                            iPos = 0;
                            continue;
                        case "@Regression":
                            iPos = 1;
                            continue;
                        case "@Expect":
                            iPos = 2;
                            continue;
                    }
                }
                if (iPos != -1)
                    aszRet[iPos] += "\r\n" + szLine;
            }
            txSrcFile.Close();

            return aszRet;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cPairwiseResults == null)
                return;

            Console.WriteLine("Final results:");
            Console.WriteLine(cPairwiseResults.genText());
            Console.WriteLine("");
            Console.WriteLine("Generated testsets:");
            List<List<string>> lstResults = new List<List<string>>();
            for (int iCnt = 0; iCnt < cPairwiseResults.cUserTestSet.AssignedPairs.Count; iCnt++)
            {
                clsPair cPair = cPairwiseResults.cUserTestSet.AssignedPairs[iCnt];
                List<string> lstResult = new List<string>();
                for (int iPairCnt = 0; iPairCnt < cPair.Pair.Length; iPairCnt++)
                {
                    int iVar = cPair.Pair[iPairCnt];
                    string szVar = cPairwiseResults.cUserTestSet.getUserValue(iVar).Name;
                    szVar += " ("+iVar+")";
                    lstResult.Add(szVar); 
                }
                lstResults.Add(lstResult);
            }
            arrayFormat cArrayFormat = new arrayFormat(lstResults);
            string szText = cArrayFormat.getText(4);
            Console.WriteLine(szText);
            Console.WriteLine("");

            Console.WriteLine("Pairs ordered to result lines:");
            for (int iCnt = 0; iCnt < cPairwiseResults.lstResultLines.Count; iCnt++)
            {
                clsResult cResult = cPairwiseResults.lstResultLines[iCnt];
                Console.WriteLine("TODO: make nice routine to show variables in a list");
                for (int iPairCnt = 0; iPairCnt < cResult.lstMatchingPairs.Count; iPairCnt++)
                {
                    clsPair cPair = cResult.lstMatchingPairs[iPairCnt];
                    Console.WriteLine("    " + cPair.ToString() + " TODO write vars");
                }
            }
            Console.WriteLine("");

            Console.WriteLine("Regression lines ordered to result lines");
            for (int iCnt = 0; iCnt < cPairwiseResults.lstResultLines.Count; iCnt++)
            {
                clsResult cResult = cPairwiseResults.lstResultLines[iCnt];
                if (cResult.lstRegressionLines.Count != 0)
                {
                    Console.WriteLine("TODO: make nice routine to show variables in a list");
                    for (int iRegCnt = 0; iRegCnt < cResult.lstRegressionLines.Count; iRegCnt++)
                    {
                        clsRegressionLine RegressionLine = cResult.lstRegressionLines[iRegCnt];
                        Console.WriteLine("    " + RegressionLine.ToString() + " TODO write vars");
                    }
                }
            }

            Console.WriteLine("TODO obsolete parameters");
            Console.WriteLine("TODO new Parameters");
            Console.WriteLine("TODO obsolete variables");
            Console.WriteLine("TODO new variables");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

    }
}
