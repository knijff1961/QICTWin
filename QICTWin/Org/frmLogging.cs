// <copyright file="frmLogging.cs" company="Fouroaks">
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
    public partial class frmLogging : Form
    {
        public frmLogging()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            checkControls();
        }
        private void checkControls()
        {
            string[] enumnames = System.Enum.GetNames(typeof(E_LOG));
            Array values = System.Enum.GetValues(typeof(E_LOG));

            for(int iCnt=0;iCnt<enumnames.Length;iCnt++)
            {
                string szTmpName = enumnames[iCnt];
                szTmpName = szTmpName.Replace("_", "").Substring(1).ToLower();
                enumnames[iCnt] = szTmpName;
            }
            checkControls(Controls, enumnames);
        }

        private void checkControls(Control.ControlCollection controlCollection, string[] enumnames)
        {
            foreach (Control cControl in controlCollection)
            {
                if (cControl is CheckBox)
                {
                    CheckBox chk = (CheckBox)cControl;
                    string szName = chk.Name;
                    szName = szName.Replace("_", "").Substring(3).ToLower();

                    bool bFound = false;
                    for (int iCnt = 0; iCnt < enumnames.Length; iCnt++)
                    {
                        string szTmpName = enumnames[iCnt];
                        if (szTmpName.Equals(szName))
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if(!bFound)
                        Console.WriteLine(szName);
                    if(!chk.Name.Equals(chk.AccessibleName))
                        Console.WriteLine(chk.Name);

                }
                else if (cControl is Button) ;
                else if (cControl is TabControl)
                {
                    TabControl cTabControl = (TabControl)cControl;
                    checkControls(cTabControl.Controls, enumnames);
                }
                else if (cControl is TabPage)
                {
                    TabPage cTabPage = (TabPage)cControl;
                    checkControls(cTabPage.Controls, enumnames);
                }

                else
                    checkControls(cControl.Controls, enumnames);

            }
        }
    }
}
