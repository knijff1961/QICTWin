// <copyright file="clsGlobal.cs" company="Fouroaks">
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
    public static class clsGlobal
    {
        public static List<string> lstLogHistory = new List<string>();

        public static void _stop()
        {
        }
        public static int[] FillArray(int iSize, int iVal)
        {
            int[] iRet = new int[iSize];
            for (int i = 0; i < iSize; i++)
                iRet[i] = iVal;
            return iRet;
        }
        public static int[,] FillArray(int iRows, int iCols, int iVal)
        {
            int[,] iRet = new int[iRows, iCols];
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    iRet[i, j] = iVal;
            return iRet;
        }


    }

    public class arrayFormat
    {
        List<List<string>> m_lstLines = new List<List<string>>();
        int[] aiColWidth = new int[0];

        public arrayFormat(List<List<string>> lstLines)
        {
            aiColWidth = new int[lstLines[0].Count];
            for (int iRowCnt = 0; iRowCnt < lstLines.Count; iRowCnt++)
            {
                List<string> lstLine = lstLines[iRowCnt];
                List<string> lstCopy = new List<string>();
                for (int iColCnt = 0; iColCnt < lstLine.Count; iColCnt++)
                {
                    string szValue = lstLine[iColCnt];
                    lstCopy.Add(szValue);
                    if (aiColWidth[iColCnt] < szValue.Length)
                        aiColWidth[iColCnt] = szValue.Length;
                }
                m_lstLines.Add(lstCopy);
            }
        }
        public arrayFormat(List<string[]> lstLines)
        {
        }
        public arrayFormat(string[][] lstLines)
        {
        }
        public arrayFormat(string[,] lstLines)
        {
        }

        public string getText()
        {
            return "";
        }
        public string getText(int Offset)
        {
            return getText(Offset, char.MinValue, false);
         }

        public string getText(int Offset, char sep, bool bTrim)
        {
            string szRet = "";
            for (int iRowCnt = 0; iRowCnt < m_lstLines.Count; iRowCnt++)
            {
                List<string> lstLine = m_lstLines[iRowCnt];
                string szLine = "";
                for (int iColCnt = 0; iColCnt < lstLine.Count; iColCnt++)
                {
                    string szValue = lstLine[iColCnt];
                    szValue=Stringsupport.addSpacesToString(szValue, aiColWidth[iColCnt]);
                    if (bTrim)
                        szValue = szValue.Trim();
                    szLine += sep;
                    szLine += szValue;
                }
                szLine = szLine.Substring(1);
                if (sep == char.MinValue)
                {
                    string szSep = sep.ToString();
                    string szReplace = " ";
                    szLine = szLine.Replace(szSep, szReplace);
                }
                if (Offset > 0)
                {
                    string szOffset = Stringsupport.space(Offset);
                    szLine = szOffset + szLine;
                }
                szRet += "\r\n" + szLine;
            }
            if (szRet.Length > 2)
                szRet = szRet.Substring(2);


            return szRet;
        }
    }
}
