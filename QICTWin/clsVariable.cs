// <copyright file="clsVariable.cs" company="Fouroaks">
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
    public class clsVariable
    {
        public enum EVARMUTATION
        {
            E_NONE,
            E_SAME,
            E_OBSOLETE,
            E_NEW
        }

        string szName = "";
        int iLegalPos = -1;
        int iUnusedCount;

        int iUserParNr = -1;
        int iUserNr = -1;
        int iRegParNr = -1;
        int iRegNr = -1;

        public EVARMUTATION eVarMutation = EVARMUTATION.E_NONE;

        public int LegalPos
        {
            get
            {
                return iLegalPos;
            }
        }
        public int UnusedCount
        {
            get
            {
                return iUnusedCount;
            }
            set
            {
                iUnusedCount = value;
            }
        }
        public string Name
        {
            get
            {
                return szName;
            }
        }
        public int UserParNr
        {
            get
            {
                return iUserParNr;
            }
            set
            {
                iUserParNr = value;
            }
        }
        public int UserNr
        {
            get
            {
                return iUserNr;
            }
            set
            {
                iUserNr = value;
            }
        }
        public int RegParNr
        {
            get
            {
                return iRegParNr;
            }
            set
            {
                iRegParNr = value;
            }
        }
        public int RegNr
        {
            get
            {
                return iRegNr;
            }
            set
            {
                iRegNr = value;
            }
        }

        public clsVariable(string szName, int iLegalStartPos)
        {
            iLegalPos = iLegalStartPos;
            this.szName = szName.Trim();
        }

        public clsVariable(string Name, int UserParNr, int UserNr, int RegParNr, int RegNr)
        {
            this.szName = Name;
            this.UserParNr = UserParNr;
            this.UserNr = UserNr;
            this.RegParNr = RegParNr;
            this.RegNr = RegNr;
        }
        public void Sync(clsUserTestSet cUserTestSet)
        {
            clsParam cUserParameter = cUserTestSet[UserParNr];
            for (int iCnt = 0; iCnt < cUserParameter.nrValues; iCnt++)
            {
                if (cUserParameter.getUserValue(iCnt).Name.Equals(szName, StringComparison.OrdinalIgnoreCase))
                {
                    UserNr = iCnt;
                    iLegalPos = cUserParameter.getUserValue(iCnt).LegalPos;
                    return;
                }
            }
        }


        public static int comparer(clsVariable T1, clsVariable T2)
        {
            if (T1.LegalPos == -1 || T2.LegalPos == -1)
            {
                if (T1.LegalPos == T2.LegalPos) return 0;
                if (T1.LegalPos == -1) return 1;
                return -1;
            }
            if (T1.LegalPos < T2.LegalPos) return -1;
            if (T1.LegalPos > T2.LegalPos) return 1;
#if DEBUG
            if (!T1.szName.Equals(T2.szName))
                clsGlobal._stop();
#endif
            return 0;
        }

        public override string ToString()
        {
            string szRet = szName + " U{";
            szRet += UserParNr.ToString("00");
            szRet += " ";
            szRet += UserNr.ToString("00");
            szRet += " ";
            szRet += iLegalPos.ToString("00");
            szRet += "} R{";
            szRet += RegParNr.ToString("00");
            szRet += " ";
            szRet += RegNr.ToString("00");
            szRet += "}";

            return szRet;
        }

        /// <summary>Constructs a string with user information: 'name' U{'parnr' 'regnr' 'position'}
        /// </summary>
        /// <returns>The user information string</returns>
        public string ToUserStringPos()
        {
            string szRet = szName + " U{";
            szRet += ToUserString();
            szRet += " ";
            szRet += iLegalPos.ToString("00");
            szRet += "}";

            return szRet;
        }

        /// <summary>Constructs a string with user information: 'parnr' 'regnr'}
        /// </summary>
        /// <returns>The user information string</returns>
        public string ToUserString()
        {
            string szRet = "";
            szRet += UserParNr.ToString("00");
            szRet += " ";
            szRet += UserNr.ToString("00");

            return szRet;
        }

        /// <summary>Constructs a string with regression information: 'name' R{'parnr' 'regnr' 'position'}
        /// </summary>
        /// <returns>The regression information string</returns>
        public string ToRegressionStringPos()
        {
            string szRet = szName + " R{";
            szRet += ToRegressionString();
            szRet += " ";
            szRet += iLegalPos.ToString("00");
            szRet += "}";

            return szRet;
        }

        /// <summary>Constructs a string with regression information: 'parnr' 'regnr'}
        /// </summary>
        /// <returns>The regression information string</returns>
        public string ToRegressionString()
        {
            string szRet = RegParNr.ToString("00");
            szRet += " ";
            szRet += RegNr.ToString("00");
 
            return szRet;
        }

        /// <summary>Constructs a string with user information: 'name' U{'parnr' 'regnr' 'position'}
        /// </summary>
        /// <returns>The user information string</returns>
        public string ToUserStringPosStr(clsUserTestSet cUserTestSet)
        {
            string szRet = szName + " U{";
            szRet += ToUserString();
            szRet += " ";
            szRet += iLegalPos.ToString("00");
            szRet += "}";

            return szRet;
        }

        /// <summary>Constructs a string with user information: 'parnr' 'regnr'}
        /// </summary>
        /// <returns>The user information string</returns>
        public string ToUserStringStr(clsUserTestSet cUserTestSet)
        {
            string szRet = "";
            szRet += UserParNr.ToString("00");
            szRet += " ";
            szRet += UserNr.ToString("00");

            return szRet;
        }

        /// <summary>Constructs a string with regression information: 'name' R{'parnr' 'regnr' 'position'}
        /// </summary>
        /// <returns>The regression information string</returns>
        public string ToRegressionStringPosStr(clsUserTestSet cUserTestSet)
        {
            string szRet = szName + " R{";
            szRet += ToRegressionString();
            szRet += " ";
            szRet += iLegalPos.ToString("00");
            szRet += "}";

            return szRet;
        }

        /// <summary>Constructs a string with regression information: 'parnr' 'regnr'}
        /// </summary>
        /// <returns>The regression information string</returns>
        public string ToRegressionStringStr(clsUserTestSet cUserTestSet)
        {
            string szRet = RegParNr.ToString("00");
            szRet += " ";
            szRet += RegNr.ToString("00");

            return szRet;
        }

    }
}
