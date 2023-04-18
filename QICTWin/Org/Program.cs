// <copyright file="Program.cs" company="Fouroaks">
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
using System.Linq;
using System.Windows.Forms;

namespace QICTWin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPairwise());
        }
    }
}
