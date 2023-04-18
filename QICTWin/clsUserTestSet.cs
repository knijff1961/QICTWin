// <copyright file="clsUserTestSet.cs" company="Fouroaks">
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
    /// <summary>
    /// Contains and processes all information about the user testset
    /// </summary>
    public class clsUserTestSet
    {
        /// <summary>Parameters of the user testset</summary>
        List<clsParam> lstUserParameter = new List<clsParam>();
        /// <summary>All Variables of the user testset</summary>
        List<clsVariable> lstUserValues = new List<clsVariable>();
        /// <summary>Number of generated pairs</summary>
        int iNumberPairs;
        /// <summary>
        /// All pairs in the user testset
        /// </summary>
        List<clsPair> allPairs;
        /// <summary>
        /// All pairs which are not handled (assigned) yet
        /// </summary>
        List<clsPair> allUnassignedPairs;
        /// <summary>
        /// All pairs which are assigned (and are found in the results)
        /// </summary>
        List<clsPair> allAssignedPairs;
        int[] m_ParameterOfValue;
        string[] m_ValueNames;

        public int NumberPairs
        {
            get
            {
                return iNumberPairs;
            }
        }
        public int nrParameters
        {
            get
            {
                return lstUserParameter.Count;
            }
        }
        public int nrValues
        {
            get
            {
                return lstUserValues.Count;
            }
        }
        public string[] ValueNames
        {
            get
            {
                return m_ValueNames;
            }
        }

        public List<clsPair> Pairs
        {
            get
            {
                return allUnassignedPairs;
            }
        }
        public List<clsPair> UnassignedPairs
        {
            get
            {
                return allUnassignedPairs;
            }
        }
        public List<clsPair> AssignedPairs
        {
            get
            {
                return allAssignedPairs;
            }
        }

        public int unusedPairs
        {
            get
            {
                int iRet = 0;
                foreach (clsPair clsPair in allUnassignedPairs)
                {
                    if (clsPair.bUsed)
                        iRet++;
                }
                if (iRet != allUnassignedPairs.Count)
                    clsGlobal._stop();
                return iRet;
            }
        }
        public clsParam this[int index]
        {
            get
            {
                return lstUserParameter[index];
            }
        }
        public clsParam this[string szName]
        {
            get
            {
                foreach (clsParam cUserParameter in lstUserParameter)
                    if (cUserParameter.Name.Equals(szName))
                        return cUserParameter;
                return null;
            }
        }




        public clsUserTestSet(string szLines, int iDepth)
        {
            try
            {
                szLines = szLines.Trim();
                lstUserParameter = new List<clsParam>();
                lstUserValues = new List<clsVariable>();
                allAssignedPairs = new List<clsPair>();
                szLines = szLines.Replace("\r", "\n");
                szLines = szLines.Replace("\n\n", "\n");
                string[] aLines = szLines.Split('\n');
                int iLegalStartPos = 0;

                clsGlobal.lstLogHistory.Add("Reading user test lines (format: <paramname>: <var1>, ..., <varn>");
                foreach (string szLine in aLines)
                    if (szLine.Trim().Length != 0)
                    {
                        clsGlobal.lstLogHistory.Add(" parsing line "+szLine);
                        lstUserParameter.Add(new clsParam(szLine, ref iLegalStartPos));
                    }
                clsGlobal.lstLogHistory.Add(" Found "+lstUserParameter.Count+"parameters");

                clsGlobal.lstLogHistory.Add("Determining user variables:");
                foreach (clsParam cUserParameter in lstUserParameter)
                    for (int iCnt = 0; iCnt < cUserParameter.nrValues; iCnt++)
                    {
                        lstUserValues.Add(cUserParameter[iCnt]);
                    }
                clsGlobal.lstLogHistory.Add(" Found " + lstUserValues.Count + "variables");


                clsGlobal.lstLogHistory.Add("Determining number of pairs:");
                iNumberPairs = getNumberOfPairs(iDepth);
                clsGlobal.lstLogHistory.Add(" Found " + lstUserValues.Count + "variables");

                allUnassignedPairs = new List<clsPair>();
                for (int iCnt = 0; iCnt < lstUserParameter.Count - iDepth + 1; iCnt++)
                {
                    allUnassignedPairs.AddRange(makePairs(iDepth, iCnt));
                }

                foreach (clsPair cPair in allUnassignedPairs)
                {
                    Console.WriteLine(cPair.ToStringStr(this));
                }
                allPairs = new List<clsPair>(allUnassignedPairs);

                m_ValueNames = new string[nrValues];
                m_ParameterOfValue = new int[nrValues]; // the indexes are parameter values, the cell values are positions within a testSet
                int k = 0;
                for (int i = 0; i < nrParameters; ++i)
                {
                    clsParam cUserParameter = lstUserParameter[i];
                    int[] curr = getLegalValues(i);
                    for (int j = 0; j < curr.Length; ++j)
                    {
                        m_ValueNames[k] = lstUserValues[cUserParameter.StartUserVlaue + j].Name;
                        m_ParameterOfValue[k++] = i;
                    }
                }

                initUnused();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Assign a pair by removing it from the unassigned list and place it in the assigned list
        /// </summary>
        /// <param name="iNr">Place of the pair in the unassigned list</param>
        /// <returns>Always true</returns>
        public bool Assign(int iNr)
        {
            clsPair cPair = UnassignedPairs[iNr];
            cPair.bUsed = false;
            UnassignedPairs.RemoveAt(iNr);
            AssignedPairs.Add(cPair);

            return true;
        }

        public int getValueOfParamater(int iValueNr)
        {
            return m_ParameterOfValue[iValueNr];
        }
        public clsVariable getUserValue(int iNr)
        {
            return lstUserValues[iNr];
        }
        public clsVariable getUserValue(string szName)
        {
            foreach (clsVariable cUserValue in lstUserValues)
                if (cUserValue.Name.Equals(szName))
                    return cUserValue;
            return null;
        }
        public int[] getLegalValues(int iNr)
        {
            return lstUserParameter[iNr].LegalValues;
        }

        private int getNumberOfPairs(int iDepth)
        {
            int numberPairs = 0;
            for (int iCnt = 0; iCnt < lstUserParameter.Count - iDepth + 1; iCnt++)
            {
                numberPairs += subCount(iDepth, iCnt);
            }
            return numberPairs;
        }
        private int subCount(int iDepth, int iFrom)
        {
            int iRet = 0;

            if (iDepth == 2)
            {
                for (int iCnt = iFrom + 1; iCnt < lstUserParameter.Count; iCnt++)
                    iRet += lstUserParameter[iCnt].nrValues;
                iRet *= lstUserParameter[iFrom].nrValues;
                return iRet;
            }

            iRet = subCount(iDepth - 1, iFrom + 1);
            return iRet * lstUserParameter[iFrom].nrValues;
        }
        List<clsPair> makePairs(int iDepth, int iFrom)
        {
            if (iDepth == 2)
            {

                List<clsPair> lstRet2 = new List<clsPair>();

                for (int iCnt1 = lstUserParameter[iFrom][0].LegalPos; iCnt1 <= lstUserParameter[iFrom][lstUserParameter[iFrom].nrValues - 1].LegalPos; iCnt1++)
                    for (int iCnt2 = lstUserParameter[iFrom + 1][0].LegalPos; iCnt2 <= lstUserParameter[lstUserParameter.Count - 1][lstUserParameter[lstUserParameter.Count - 1].nrValues - 1].LegalPos; iCnt2++)
                    {
                        clsPair cPair = new clsPair(iCnt1, iCnt2);
                        lstRet2.Add(cPair);
                        //Console.WriteLine(cPair.ToStringStr(this));
                     }
                return lstRet2;
            }

            // make lower pair
            List<clsPair> lstRet = new List<clsPair>();
            for (int iStart = iFrom; iStart <= nrParameters - iDepth; iStart++)
            {
                for (int iCnt1 = lstUserParameter[iStart][0].LegalPos; iCnt1 <= lstUserParameter[iStart][lstUserParameter[iStart].nrValues - 1].LegalPos; iCnt1++)
                {
                    //Console.WriteLine(iStart + "-" + iCnt1);
                    List<clsPair> lstSub = makePairs(iDepth - 1, iStart+1);
                    foreach (clsPair cPair in lstSub)
                    {
                        cPair.appendPos(iCnt1);
                        lstRet.Add(cPair);
                    }
                }
            }

            return lstRet;
        }

        private void initUnused()
        {
            for (int i = 0; i < allUnassignedPairs.Count; ++i)
            {
                int[] iTmp = allUnassignedPairs[i].Pair;
                for (int j = 0; j < iTmp.Length; j++)
                {
                    ++lstUserValues[iTmp[j]].UnusedCount;
                }
                allUnassignedPairs[i].prepareSelection(this);
            }
        }

    }

}
