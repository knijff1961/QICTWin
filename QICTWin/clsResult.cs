// <copyright file="clsResult.cs" company="Fouroaks">
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
    public class clsResult
    {
        public E_TESTSET_TYPE eType = E_TESTSET_TYPE.NEW;
        public int[] iParamValueIdx;
        public int iResultIdx = 0;
        public List<clsVariable> lstVariables;
        public List<clsPair> lstMatchingPairs = new List<clsPair>();
        public List<clsRegressionLine> lstRegressionLines = new List<clsRegressionLine>();

        public clsResult(int[] iParamValueIdx, clsUserTestSet cUserTestset)
        {
            //this.eType = eType;
            this.iParamValueIdx = iParamValueIdx;
            lstVariables = new List<clsVariable>();
            for (int iTestParamCnt = 0; iTestParamCnt < iParamValueIdx.Length; iTestParamCnt++)
            {
                int iTestParam = iParamValueIdx[iTestParamCnt];
                clsVariable cVariable = cUserTestset.getUserValue(iTestParam);
#if DEBUG
                if (cVariable.LegalPos != iTestParam)
                    clsGlobal._stop();
#endif
                lstVariables.Add(cVariable);
            }
        }
    }

    public class clsResults
    {
        #region information variables
        public List<clsResult> lstResultLines;
        //public List<string> lstHistoryInfo = new List<string>();
        public List<string> lstRegressionInfo = new List<string>();
        public clsUserTestSet cUserTestSet;
        public clsRegression cRegTestSet;

        /// <summary>
        /// All user parameters
        /// </summary>
        public List<clsParam> userParams = new List<clsParam>();
        /// <summary>
        /// All user variables
        /// </summary>
        public List<clsVariable> userVars = new List<clsVariable>();

        /// <summary>
        /// All regression parameters
        /// </summary>
        public List<clsParam> regParams = new List<clsParam>();
        /// <summary>
        /// All regression variables
        /// </summary>
        public List<clsVariable> regVars = new List<clsVariable>();

        /// <summary>
        /// Parameters found both in regression and new set
        /// </summary>
        public List<clsParam> lstPars_same = new List<clsParam>();
        /// <summary>
        /// Parameters only found in new set
        /// </summary>
        public List<clsParam> lstPars_new = new List<clsParam>();
        /// <summary>
        /// Parameters only found in regression set
        /// </summary>
        public List<clsParam> lstPars_obsolete = new List<clsParam>();

        /// <summary>
        /// Variables found both in regression and new set
        /// </summary>
        public List<clsVariable> lstVars_same = new List<clsVariable>();
        /// <summary>
        /// Variables only found in new set
        /// </summary>
        public List<clsVariable> lstVars_new = new List<clsVariable>();
        /// <summary>
        /// Variables only found in regression set
        /// </summary>
        public List<clsVariable> lstVars_obsolete = new List<clsVariable>();
        #endregion

        public void finalize(clsUserTestSet cUserTestSet, clsRegression cRegression)
        {
            // get all parameters of user set
            for (int iCnt = 0; iCnt < cUserTestSet.nrParameters; iCnt++)
                userParams.Add(cUserTestSet[iCnt]);

            // get all variables of user set
            for (int iCnt = 0; iCnt < cUserTestSet.nrValues; iCnt++)
                regVars.Add(cUserTestSet.getUserValue(iCnt));

            // get all parameters of regression set
            for (int iCnt = 0; iCnt < cRegression.nrParameters; iCnt++)
                regParams.Add(cRegression[iCnt]);

            // get all variables of regression set
            for (int iCnt = 0; iCnt < cRegression.nrValues; iCnt++)
                regVars.Add(cRegression.getUserValue(iCnt));

            // find new parameters (not in regression)
            for (int iUserCnt = 0; iUserCnt < cUserTestSet.nrParameters; iUserCnt++)
            {
                clsParam cUserParam = cUserTestSet[iUserCnt];
                bool bFound = false;
                for (int iRegCnt = 0; iRegCnt < cRegression.nrParameters; iRegCnt++)
                {
                    clsParam cRegParam = cRegression[iRegCnt];
                    if (clsParam.comparer(cUserParam, cRegParam) == 0)
                    {
                        lstPars_same.Add(cUserParam);
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                    lstPars_new.Add(cUserParam);
            }

            // find obsolete parameters (only in regression)
            for (int iRegCnt = 0; iRegCnt < cRegression.nrParameters; iRegCnt++)
            {
                clsParam cRegParam = cRegression[iRegCnt];
                bool bFound = false;
                for (int iUserCnt = 0; iUserCnt < cUserTestSet.nrParameters; iUserCnt++)
                {
                    clsParam cUserParam = cUserTestSet[iUserCnt];
                    if (clsParam.comparer(cUserParam, cRegParam) == 0)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                    lstPars_obsolete.Add(cRegParam);
            }

            // find new parameters (not in regression)
            for (int iUserCnt = 0; iUserCnt < cUserTestSet.nrValues; iUserCnt++)
            {
                clsVariable cUserVar = cUserTestSet.getUserValue(iUserCnt);
                bool bFound = false;
                for (int iRegCnt = 0; iRegCnt < cRegression.nrValues; iRegCnt++)
                {
                    clsVariable cRegVar = cRegression.getUserValue(iRegCnt);
                    if (clsVariable.comparer(cUserVar, cRegVar) == 0)
                    {
                        lstVars_same.Add(cUserVar);
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                    lstVars_new.Add(cUserVar);
            }

            // find obsolete parameters (only in regression)
            for (int iRegCnt = 0; iRegCnt < cRegression.nrValues; iRegCnt++)
            {
                clsVariable cRegVar = cRegression.getUserValue(iRegCnt);
                bool bFound = false;
                for (int iUserCnt = 0; iUserCnt < cUserTestSet.nrValues; iUserCnt++)
                {
                    clsVariable cUserVar = cUserTestSet.getUserValue(iUserCnt);
                    if (clsVariable.comparer(cUserVar, cRegVar) == 0)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                    lstVars_obsolete.Add(cRegVar);
            }

        }

        public string genText()
        {
            int[] iColumnWidth = new int[userParams.Count];
            for (int iColCnt = 0; iColCnt < userParams.Count; iColCnt++)
                if (iColumnWidth[iColCnt] < userParams[iColCnt].Name.Length)
                    iColumnWidth[iColCnt] = userParams[iColCnt].Name.Length;
            for (int iRowCnt = 0; iRowCnt < lstResultLines.Count; iRowCnt++)
                for (int iColCnt = 0; iColCnt < lstResultLines[iRowCnt].lstVariables.Count; iColCnt++)
                    if (iColumnWidth[iColCnt] < lstResultLines[iRowCnt].lstVariables[iColCnt].Name.Length)
                        iColumnWidth[iColCnt] = lstResultLines[iRowCnt].lstVariables[iColCnt].Name.Length;

            string szText = "." + Stringsupport.addSpacesToString("", 6);
            //szNew += iRowCnt.ToString();
            //if (iRowCnt != 0)
            //    szNew = iRowCnt.ToString();
            for (int iColCnt = 0; iColCnt < userParams.Count; iColCnt++)
            {
                string szVal = userParams[iColCnt].Name;
                szVal = Stringsupport.addSpacesToString(szVal, iColumnWidth[iColCnt]);
                szText += " " + szVal;
            }
            szText += "\r\n";
            for (int iRowCnt = 0; iRowCnt < lstResultLines.Count; iRowCnt++)
            {
                string szNew = " ";
                //switch (ParamValues[iRowCnt].eType)
                //{
                //    case E_TESTSET_TYPE.NEW:
                //        szNew = "+";
                //        break;
                //    case E_TESTSET_TYPE.OBSOLETE:
                //        szNew = "x";
                //        break;
                //    case E_TESTSET_TYPE.REGRESSION:
                //        szNew = " ";
                //        break;
                //    default:
                //        throw new Exception("Unknown parameter type");
                //}
                szNew += " " + iRowCnt.ToString();
                szNew = Stringsupport.addSpacesToString(szNew, 6);

                for (int iColCnt = 0; iColCnt < userParams.Count; iColCnt++)
                {
                    string szVal = lstResultLines[iRowCnt].lstVariables[iColCnt].Name;
                    szVal = Stringsupport.addSpacesToString(szVal, iColumnWidth[iColCnt]);
                    szNew += " " + szVal;
                }
                szText += "\r\n" + szNew;
            }

            return szText.Trim().Substring(1);
        }


    }
}
