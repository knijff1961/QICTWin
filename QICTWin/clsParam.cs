// <copyright file="clsParam.cs" company="Fouroaks">
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
    public class clsParam
    {
        public enum EPARMUTATION
        {
            E_NONE,
            E_SAME,
            E_OBSOLETE,
            E_NEW
        }

        string szName = "";
        int iStartUserVlaue;
        int iEndUserVlaue;
        
        
        public int UserNr = -1;
        public int RegNr = -1;
        public List<clsVariable> lstVars = new List<clsVariable>();
        public EPARMUTATION eParMutation = EPARMUTATION.E_NONE;


        public string Name
        {
            get
            {
                return szName;
            }
            set
            {
                szName = value;
            }
        }
        public int StartUserVlaue
        {
            get
            {
                return iStartUserVlaue;
            }
        }
        public int EndUserVlaue
        {
            get
            {
                return iEndUserVlaue;
            }
        }
        public int nrValues
        {
            get
            {
                return lstVars.Count;
            }
        }
        public int[] LegalValues
        {
            get
            {
                int[] aiRet = new int[lstVars.Count];
                for (int iValCnt = 0; iValCnt < lstVars.Count; iValCnt++)
                {
                    aiRet[iValCnt] = lstVars[iValCnt].LegalPos;
                }
                return aiRet;
            }
        }
        public clsVariable this[int index]
        {
            get
            {
                return lstVars[index];
            }
        }
        public clsVariable this[string szName]
        {
            get
            {
                foreach (clsVariable cUserValue in lstVars)
                    if (cUserValue.Name.Equals(szName))
                        return cUserValue;
                return null;
            }
        }




        public clsParam(string Name, int UserNr, int RegNr)
        {
            this.Name = Name;
            this.UserNr = UserNr;
            this.RegNr = RegNr;
        }
        public clsParam(string szLine, ref int iLegalStartPos)
        {
            iStartUserVlaue = iLegalStartPos;
            string[] lineTokens = szLine.Split(':');
            szName = lineTokens[0].Trim();
            string[] strValues = lineTokens[1].Split(',');
            foreach (string strValue in strValues)
            {
                lstVars.Add(new clsVariable(strValue, iLegalStartPos++));
            }
            iEndUserVlaue = iLegalStartPos - 1;

        }


        public clsVariable getUserValue(int iNr)
        {
            return lstVars[iNr];
        }

        public clsVariable AddVar(string Name)
        {
            for (int iCnt = 0; iCnt < lstVars.Count; iCnt++)
            {
                if (lstVars[iCnt].Name.Equals(Name, StringComparison.OrdinalIgnoreCase))
                    // already exists
                    return lstVars[iCnt];
            }

            clsVariable cVariable;
            // new variable so add
            if (UserNr >= 0) // variable defined in user entry
                cVariable = new clsVariable(Name, UserNr, lstVars.Count, -1, -1);
            else// variable defined in regression entry
                cVariable = new clsVariable(Name, -1, -1, RegNr, lstVars.Count);
            lstVars.Add(cVariable);
            return cVariable;
        }

        public void SyncWithUser(clsUserTestSet cUserTestSet)
        {
            for (int iCnt = 0; iCnt < cUserTestSet.nrParameters; iCnt++)
            {
                if (cUserTestSet[iCnt].Name.Equals(Name, StringComparison.OrdinalIgnoreCase))
                {
                    UserNr = iCnt;
                    for (int iVarCnt = 0; iVarCnt < lstVars.Count; iVarCnt++)
                    {
                        clsVariable cVar = lstVars[iVarCnt];
                        cVar.UserParNr = iCnt;
                        cVar.Sync(cUserTestSet);
                        System.Console.WriteLine(cVar);
                    }
                    return;
                }
            }
        }

        public static int comparer(clsParam T1, clsParam T2)
        {
            return string.Compare(T1.Name, T2.Name, true);
        }

    }
}
