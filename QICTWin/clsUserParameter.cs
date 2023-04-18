using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    public class clsUserParameter
    {
        string szName;
        List<clsUserValue> lstUserValues = new List<clsUserValue>();
        int iStartUserVlaue;
        int iEndUserVlaue;

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
                return lstUserValues.Count;
            }
        }
        public string Name
        {
            get
            {
                return szName;
            }
        }
        public clsUserValue getUserValue(int iNr)
        {
            return lstUserValues[iNr];
        }
        public int[] LegalValues
        {
            get
            {
                int[] aiRet = new int[lstUserValues.Count];
                for (int iValCnt = 0; iValCnt < lstUserValues.Count; iValCnt++)
                {
                    aiRet[iValCnt] = lstUserValues[iValCnt].LegalPos;
                }
                return aiRet;
            }
        }
        public clsUserValue this[int index]
        {
            get
            {
                return lstUserValues[index];
            }
        }
        public clsUserValue this[string szName]
        {
            get
            {
                foreach (clsUserValue cUserValue in lstUserValues)
                    if (cUserValue.Name.Equals(szName))
                        return cUserValue;
                return null;
            }
        }

        public clsUserParameter(string szLine, ref int iLegalStartPos)
        {
            iStartUserVlaue = iLegalStartPos;
            string[] lineTokens = szLine.Split(':');
            szName = lineTokens[0].Trim();
            string[] strValues = lineTokens[1].Split(',');
            foreach (string strValue in strValues)
            {
                lstUserValues.Add(new clsUserValue(strValue, iLegalStartPos++));
            }
            iEndUserVlaue = iLegalStartPos - 1;

        }

 


    }
}
