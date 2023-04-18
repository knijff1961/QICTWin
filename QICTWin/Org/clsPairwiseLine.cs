using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    public class clsPairwiseLine
    {
        private string szParameterName;
        public List<string> lstParameterValues;
        public int iLegalValuesStart;
        public int iLegalValuesEnd;

        public int nrValues
        {
            get
            {
                return lstParameterValues.Count;
            }
        }
        public string ParameterName
        {
            get
            {
                return szParameterName;
            }
        }
        public string getParameterValue(int iNr)
        {
            return lstParameterValues[iNr];
        }
        public clsPairwiseLine(string szLine)
        {

            string[] lineTokens = szLine.Split(':');
            szParameterName = lineTokens[0].Trim();
            string[] strValues = lineTokens[1].Split(',');
            lstParameterValues = new List<string>();
            foreach (string strValue in strValues)
                lstParameterValues.Add(strValue.Trim());
        }
    }
}
