// <copyright file="clsLog.cs" company="Fouroaks">
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
    public class clsLog
    {

        public class clsTestVar
        {
            public int iNr;
            public string szName;

            public override string ToString()
            {
                return iNr+" "+szName;
            }
        }

        public class clsTestLine
        {
            public List<clsTestVar> TestLine = new List<clsTestVar>();
        }
        List<clsTestLine> lstPairs;
        List<clsTestLine> lstResults;
        List<clsTestLine> lstRegressions;

        public string testResults(clsUserTestSet cUserTestSet, clsRegression cRegTestSet, clsResults cPairwiseResults)
        {
            #region prepare by making lists
            // first make list of pair list
            lstPairs = new List<clsTestLine>();
            for (int iPairCnt = 0; iPairCnt < cUserTestSet.AssignedPairs.Count; iPairCnt++)
            {
                clsTestLine cTestPair = new clsTestLine();
                clsPair cPair = cUserTestSet.AssignedPairs[iPairCnt];
                for (int iValCnt = 0; iValCnt < cPair.Pair.Length; iValCnt++)
                {
                    clsTestVar cTestVar = new clsTestVar();
                    cTestVar.iNr = cPair.Pair[iValCnt];
                    cTestVar.szName = cUserTestSet.ValueNames[cTestVar.iNr];
                    cTestPair.TestLine.Add(cTestVar);
                }
                lstPairs.Add(cTestPair);
            }

            // now get all items in result (ordered as parameters replied)
            lstResults = new List<clsTestLine>();
            for (int iRowCnt = 0; iRowCnt < cPairwiseResults.lstResultLines.Count; iRowCnt++)
            {
                clsTestLine cTestResult = new clsTestLine();
                clsResult cTestsetResultItem = cPairwiseResults.lstResultLines[iRowCnt];
                for (int iColCnt = 0; iColCnt < cTestsetResultItem.lstVariables.Count; iColCnt++)
                {
                    clsTestVar cTestVar = new clsTestVar();
                    clsVariable cVariable = cTestsetResultItem.lstVariables[iColCnt];
                    cTestVar.szName = cVariable.Name;
                    // search number of this variable
                    cTestVar.iNr = cVariable.LegalPos;
                    cTestResult.TestLine.Add(cTestVar);
                }
                lstResults.Add(cTestResult);
            }

            // get all items of the regression
            lstRegressions = new List<clsTestLine>();
            for (int iRowCnt = 0; iRowCnt < cRegTestSet.aiHandledRegressionSet.Count; iRowCnt++)
            {
                clsTestLine cTestRegression = new clsTestLine();
                //List<clsTestVar> lstRegression = new List<clsTestVar>();
                clsVariable[] cRegVars = cRegTestSet.aiHandledRegressionSet[iRowCnt].acVariables;
                for (int iColCnt = 0; iColCnt < cRegVars.Length; iColCnt++)
                {
                    clsTestVar cRegressionVar = new clsTestVar();
                    clsVariable cRegVar = cRegVars[iColCnt];
                    if (cRegVar != null)
                    {
                        cRegressionVar.szName = cRegVar.Name;
                        // search number of this variable
                        cRegressionVar.iNr = -1;
                        for (int iNr = 0; iNr < cUserTestSet.ValueNames.Length; iNr++)
                        {
                            if (cRegressionVar.szName.Equals(cUserTestSet.ValueNames[iNr]))
                            {
                                cRegressionVar.iNr = iNr;
                                break;
                            }
                        }
                        if (cRegressionVar.iNr == -1)
                            clsGlobal._stop();
                    }
                    else
                    {
                        cRegressionVar.szName = "";
                        cRegressionVar.iNr = -1;
                    }
                    cTestRegression.TestLine.Add(cRegressionVar);
                }
                lstRegressions.Add(cTestRegression);
            }
            #endregion

            // look if ALL pairs can be found in results
            for (int iPair = 0; iPair < lstPairs.Count; iPair++)
            {
                clsTestLine lstPair = lstPairs[iPair];
                // search for this pair in all results until found
                bool bFound = false;
                for (int iRow = 0; iRow < lstResults.Count; iRow++)
                {
                    clsTestLine lstResult = lstResults[iRow];
                    int iToBeFound = lstPair.TestLine.Count;
                    for (int iCol = 0; iCol < lstResult.TestLine.Count; iCol++)
                    {
                        // get this variable and look if it is lstPair
                        clsTestVar cResultVar = lstResult.TestLine[iCol];
                        for (int iItem = 0; iItem < lstPair.TestLine.Count; iItem++)
                        {
                            clsTestVar cPairVar = lstPair.TestLine[iItem];
                            if (cPairVar.iNr == cResultVar.iNr)
                            {
                                iToBeFound--;
                                if (iToBeFound == 0)
                                    break;
                            }
                        }
                        if (iToBeFound == 0)
                        {
                            bFound = true;
                            break;
                        }
                    }
                    if (iToBeFound == 0)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    clsGlobal._stop();
                }
            }

            //lstPairs
            //lstResults
            //lstRegressions
            // look if all regressions can be found in results
            for (int iRegression = 0; iRegression < lstRegressions.Count; iRegression++)
            {
                clsTestLine lstRegression = lstRegressions[iRegression];

                // get number of regression variables which can be tested
                int iToBeTested = 0;
                foreach (clsTestVar cResultVar in lstRegression.TestLine)
                    if (cResultVar.iNr >= 0)
                        iToBeTested++;

                // search for this pair in all results until found
                bool bFound = false;
                for (int iRow = 0; iRow < lstResults.Count; iRow++)
                {
                    clsTestLine lstResult = lstResults[iRow];
                    int iToBeFound = iToBeTested;
                    for (int iCol = 0; iCol < lstResult.TestLine.Count; iCol++)
                    {
                        // get this variable and look if it is lstPair
                        clsTestVar cResultVar = lstResult.TestLine[iCol];
                        for (int iItem = 0; iItem < lstRegression.TestLine.Count; iItem++)
                        {
                            clsTestVar cPairVar = lstRegression.TestLine[iItem];
                            if (cPairVar.iNr == cResultVar.iNr)
                            {
                                iToBeFound--;
                                //if (iToBeFound == 0)
                                //    break;
                            }
                        }
                        if (iToBeFound == 0)
                        {
                             break;
                        }
                    }
                    if (iToBeFound == 0)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    clsGlobal._stop();
                }
            }


            return "";
        }
    }
}
