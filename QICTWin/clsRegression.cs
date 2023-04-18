// <copyright file="clsRegression.cs" company="Fouroaks">
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
    public class clsRegressionLine
    {
        public int iID;
        public clsVariable[] acVariables;
        public List<int> resultIds = new List<int>();
    }

    public class clsRegression
    {

        public List<clsParam> lstPars = new List<clsParam>();
        List<clsVariable> lstValues = new List<clsVariable>();

        List<List<clsVariable>> lstVarLookup = new List<List<clsVariable>>();

        public List<clsRegressionLine> aiRegressionSet = new List<clsRegressionLine>();
        public List<clsRegressionLine> aiHandledRegressionSet = new List<clsRegressionLine>();


        public int nrParameters
        {
            get
            {
                return lstPars.Count;
            }
        }
        public int nrValues
        {
            get
            {
                return lstVarLookup.Count;
            }
        }
        public clsParam this[int index]
        {
            get
            {
                return lstPars[index];
            }
        }
        public clsParam this[string szName]
        {
            get
            {
                foreach (clsParam cRegParameter in lstPars)
                    if (cRegParameter.Name.Equals(szName))
                        return cRegParameter;
                return null;
            }
        }

        public clsRegression(string szLines, clsUserTestSet cUserTestSet)
        {
            szLines = szLines.Trim();
            lstPars = new List<clsParam>();
            //lstVarLookup = new List<List<clsVariable>>();

            szLines = szLines.Replace("\r", "\n");
            szLines = szLines.Replace("\n\n", "\n");
            string[] aLines = szLines.Split('\n');

            // Handle first line: contains the parameter namess of the regression set
            string szParamNames = aLines[0];
            string[] aszParamNames = szParamNames.Split(',');
            for (int iCnt = 0; iCnt < aszParamNames.Length; iCnt++)
            {
                string szParamName = aszParamNames[iCnt];
                if (szParamName.Trim().Length != 0)
                {
                    lstPars.Add(new clsParam(szParamName.Trim(), -1, iCnt));
                }
            }

            if (lstPars.Count == 0)
                return;
            // Make List "Array" with all posible sets to be examined
            lstVarLookup = new List<List<clsVariable>>();
            for (int iCnt = 0; iCnt < lstPars.Count; iCnt++)
            {
                lstVarLookup.Add(new List<clsVariable>());
            }

            // Handle rest of lines: contains the variables
            for (int iCnt = 1; iCnt < aLines.Length; iCnt++)
            {
                string szParamValues = aLines[iCnt];
                string[] aszParamValues = szParamValues.Split(',');
                for (int iValCnt = 0; iValCnt < aszParamValues.Length; iValCnt++)
                {
                    clsVariable cVariable = lstPars[iValCnt].AddVar(aszParamValues[iValCnt].Trim());
                    lstVarLookup[iValCnt].Add(cVariable);
                    lstValues.Add(cVariable);
                }
            }


            for (int iCnt = 0; iCnt < lstPars.Count; iCnt++)
            {
                lstPars[iCnt].SyncWithUser(cUserTestSet);
            }

            // display the lstVarLookup array
            string[,] aszTableDisplay = new string[lstVarLookup.Count, lstVarLookup[0].Count + 1];
            for (int iCnt = 0; iCnt < lstPars.Count; iCnt++)
            {
                aszTableDisplay[iCnt, 0] = lstPars[iCnt].Name;
            }
            for (int iColCnt = 0; iColCnt < lstPars.Count; iColCnt++)
            {
                for (int iRowCnt = 0; iRowCnt < lstVarLookup[0].Count; iRowCnt++)
                {
                    aszTableDisplay[iColCnt, iRowCnt + 1] = lstVarLookup[iColCnt][iRowCnt].ToString();
                }
            }

            Test2Lib.qsort<clsVariable>.SortUnique(lstValues, clsVariable.comparer);

            Console.WriteLine(szDisplayArray(aszTableDisplay));
            makeRegressionSet(cUserTestSet);
        }

        public clsVariable getUserValue(int iNr)
        {
            return lstValues[iNr];
        }
        public clsVariable getUserValue(string szName)
        {
            foreach (clsVariable cRegValue in lstValues)
                if (cRegValue.Name.Equals(szName))
                    return cRegValue;
            return null;
        }

        
        
        public string szDisplayArray(string[,] aszStrings)
        {
            int iNrCols = aszStrings.GetLength(0);
            int iNrRows = aszStrings.GetLength(1);

            int[] iDistributed = new int[iNrCols];
            for (int iColCnt = 0; iColCnt < iNrCols; iColCnt++)
            {
                for (int iRowCnt = 0; iRowCnt < iNrRows; iRowCnt++)
                {
                    string szValue = aszStrings[iColCnt, iRowCnt];
                    if (szValue == null)
                    {
                        szValue = "<null>";
                        aszStrings[iColCnt, iRowCnt] = szValue;
                    }
                    if (szValue.Length > iDistributed[iColCnt])
                        iDistributed[iColCnt] = szValue.Length;
                }
            }

            string szRet = "";
            for (int iRowCnt = 0; iRowCnt < iNrRows; iRowCnt++)
            {
                string szTmp = "";
                for (int iColCnt = 0; iColCnt < iNrCols; iColCnt++)
                {
                    szTmp += " | " + Stringsupport.addSpacesToString(aszStrings[iColCnt, iRowCnt], iDistributed[iColCnt]);
                }
                szRet += szTmp.Substring(3) + "\r\n";
            }
            return szRet;
        }
        public static clsVariable[,] FillArray(int iRows, int iCols)
        {
            clsVariable[,] iRet = new clsVariable[iRows, iCols];
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    iRet[i, j] = null;
            return iRet;
        }
        private void makeRegressionSet(clsUserTestSet cUserTestSet)
        {
            aiRegressionSet = new List<clsRegressionLine>();
            aiHandledRegressionSet = new List<clsRegressionLine>();

            for (int iRegCnt = 0; iRegCnt < lstVarLookup[0].Count; iRegCnt++)
            {
                clsRegressionLine cRegressionLine = new clsRegressionLine();
                cRegressionLine.acVariables = new clsVariable[cUserTestSet.nrParameters];
                cRegressionLine.iID = iRegCnt;
                aiRegressionSet.Add(cRegressionLine);
            }

            for (int iCnt = 0; iCnt < cUserTestSet.nrParameters; iCnt++)
            {
                clsParam cUserParameter = cUserTestSet[iCnt];
                // get parameter index in lstPars
                int iParsIndex = -1;
                for (int iParCnt = 0; iParCnt < lstPars.Count; iParCnt++)
                {
                    clsParam cParam = lstPars[iParCnt];
                    if (cParam.UserNr == iCnt)
                    {
                        iParsIndex = iParCnt;
                        break;
                    }
                }
                if (iParsIndex != -1)
                {
                    Console.WriteLine("Found maching par for " + iCnt + " in regression par " + iParsIndex);

                    for (int iRegCnt = 0; iRegCnt < lstVarLookup[0].Count; iRegCnt++)
                    {
                        clsVariable cVariable = lstVarLookup[iParsIndex][iRegCnt];
                        if (cVariable.LegalPos != -1)
                            aiRegressionSet[iRegCnt].acVariables[iCnt] = cVariable;
                    }
                }
            }
        }
        public int[] getRegressionResult()
        {

            // try to find the 'most filled' regression item
            int iBest = -1;
            int iMaxFilled = 0;
            for (int iRegCnt = 0; iRegCnt < aiRegressionSet.Count; iRegCnt++)
            {
                int iFilled = 0;

                for (int iVarCnt = 0; iVarCnt < aiRegressionSet[iRegCnt].acVariables.Length; iVarCnt++)
                {
                    if (aiRegressionSet[iRegCnt] == null) continue;
                    if (aiRegressionSet[iRegCnt].acVariables == null) continue;
                    if (aiRegressionSet[iRegCnt].acVariables[iVarCnt] == null) continue;
                    if (aiRegressionSet[iRegCnt].acVariables[iVarCnt].LegalPos >= 0)
                        iFilled++;
                }
                if (iFilled > iMaxFilled)
                {
                    iBest = iRegCnt;
                    iMaxFilled = iFilled;
                }
            }
            if (iBest < 0)
                return null;

            // create regression "Pair"
            int[] iBestReg = clsGlobal.FillArray(aiRegressionSet[iBest].acVariables.Length, -1);
            int iCur = 0;
            for (int iVarCnt = 0; iVarCnt < aiRegressionSet[iBest].acVariables.Length; iVarCnt++)
            {
                if (aiRegressionSet[iBest] == null) continue;
                if (aiRegressionSet[iBest].acVariables == null) continue;
                if (aiRegressionSet[iBest].acVariables[iVarCnt] == null) continue;
                if (aiRegressionSet[iBest].acVariables[iVarCnt].LegalPos >= 0)
                {
                    iBestReg[iVarCnt] = aiRegressionSet[iBest].acVariables[iVarCnt].LegalPos;
                    iCur++;
                }
            }

            return iBestReg;
        }
        public List<clsRegressionLine> Assign(int[] aiBestSet)
        {
            List<clsRegressionLine> lstRegressionRet = new List<clsRegressionLine>();
            for (int iRegCnt = aiRegressionSet.Count - 1; iRegCnt >= 0; iRegCnt--)
            {
                bool bFullFill = true;
                for (int iVarCnt = 0; iVarCnt < aiRegressionSet[iRegCnt].acVariables.Length; iVarCnt++)
                {
                    clsVariable cVariable = aiRegressionSet[iRegCnt].acVariables[iVarCnt];
                    if (cVariable == null) continue;
                    if (cVariable.LegalPos == -1) continue;
                    if (cVariable.LegalPos == aiBestSet[iVarCnt]) continue;
                    bFullFill = false;
                    break;
                }
                if (bFullFill)
                {
                    clsRegressionLine aVars = aiRegressionSet[iRegCnt];
                    aiRegressionSet.RemoveAt(iRegCnt);
                    aiHandledRegressionSet.Add(aVars);
                    lstRegressionRet.Add(aVars);
                }
            }
            return lstRegressionRet;
        }
    }
}
