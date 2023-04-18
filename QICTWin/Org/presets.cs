// <copyright file="presets.cs" company="Fouroaks">
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
using System.Text;

namespace QICTWin
{
    class presets
    {
        public static string[] asz1 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3, i3",
            "Param3: j4, k4"
        };


        public static string[] aszres1 = {
            "Param0,Param1,Param2,Param3",
            "a1,c2,g3,j4",
            "b2,c2,h3,k4",
            "b2,d2,i3,j4",
            "a1,e2,h3,j4",
            "a1,f2,i3,k4",
            "a1,d2,g3,k4",
            "b2,e2,g3,k4",
            "b2,f2,h3,j4",
            "a1,c2,i3,j4",
            "a1,d2,h3,j4",
            "a1,e2,i3,j4",
            "a1,f2,g3,j4"
        };

        public static string[] asz2 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3, i3, j3",
            "Param3: j4, k4"
        };
        public static string[] aszres2 = {
            "Param0,Param1,Param2,Param3",
            "a1,c2,g3,j4",
            "b2,c2,h3,k4",
            "b2,d2,i3,j4",
            "a1,e2,j3,k4",
            "b2,f2,g3,k4",
            "a1,d2,h3,j4",
            "a1,e2,i3,j4",
            "a1,f2,j3,j4",
            "b2,d2,j3,k4",
            "a1,c2,i3,k4",
            "b2,e2,g3,j4",
            "a1,f2,h3,j4",
            "a1,c2,j3,j4",
            "a1,d2,g3,j4",
            "a1,e2,h3,j4",
            "a1,f2,i3,j4"
        };

        public static string[] asz3 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3",
            "Param3: j4, k4"
        };
        public static string[] aszres3 = {
            "Param0,Param1,Param2,Param3",
            "a1,c2,g3,j4",
            "b2,d2,h3,j4",
            "a1,e2,h3,k4",
            "b2,f2,g3,k4",
            "a1,d2,g3,k4",
            "b2,c2,h3,k4",
            "b2,e2,g3,j4",
            "a1,f2,h3,j4"
        };

        public static string[] asz4 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3",
            "Param3: j4, k4",
            "Param4: l5, m5"
        };
        public static string[] aszres4 = {
            "Param0,Param1,Param2,Param3,Param4",
            "a1,c2,g3,j4,l5",
            "b2,d2,h3,k4,l5",
            "b2,e2,g3,j4,m5",
            "a1,f2,h3,k4,m5",
            "b2,c2,h3,j4,m5",
            "a1,d2,g3,j4,m5",
            "a1,e2,g3,k4,l5",
            "b2,f2,g3,j4,l5",
            "a1,c2,g3,k4,l5",
            "a1,e2,h3,j4,l5"
        };

        public static string[] asz5 = {
            "Param0: a1, b2",
            "Param1: c2, d2, e2, f2",
            "Param2: g3, h3"
        };
        string[] aszres5 = {
            "Param0,Param1,Param2",
            "a1,c2,g3",
            "b2,c2,h3",
            "a1,d2,h3",
            "b2,e2,g3",
            "a1,f2,g3",
            "b2,d2,g3",
            "a1,e2,h3",
            "b2,f2,h3"
        };
    }
}
