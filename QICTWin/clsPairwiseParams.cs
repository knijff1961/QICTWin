using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    class clsPairwiseParams
    {
        public List<clsTestsetResultItem> ParamValues;
        public List<string> ParamNames;
        public int Cols = 0;
        public int Rows = 0;
        public List<string> lstHistoryInfo = new List<string>();
        public List<string> lstRegressionInfo = new List<string>();
        public string[][] aaszOrgMatrix = null;
        //List<clsPairwiseLine> lstPairwiseLines
        public clsPairwiseParams(string[][] aaszOrgMatrix)
        {
            initClass(aaszOrgMatrix);
        }
        public clsPairwiseParams()
        {
            ParamValues = new List<clsTestsetResultItem>();
            ParamNames = new List<string>();
        }

        public void initClass(string[][] aaszOrgMatrix)
        {
            this.aaszOrgMatrix = aaszOrgMatrix;
            ParamNames = new List<string>(aaszOrgMatrix[0]);
            Cols = ParamNames.Count;

            ParamValues = new List<clsTestsetResultItem>();
            for (int iCnt = 1; iCnt < aaszOrgMatrix.GetLength(0); iCnt++)
            {
                ParamValues.Add(new clsTestsetResultItem(E_TESTSET_TYPE.NEW, aaszOrgMatrix[iCnt]));
            }
            Rows = ParamValues.Count;
        }

        public void Normalise(List<clsPairwiseLine> lstPairwiseLines)
        {
            List<string> szRegressionNames = new List<string>(aaszOrgMatrix[0]);
            List<string> szInputNames = new List<string>();
            foreach (clsPairwiseLine cPairwiseLine in lstPairwiseLines)
                szInputNames.Add(cPairwiseLine.ParameterName);

            //Make cross references
            int[] aiCrossRef_I2R = clsPairwise.clsPairwaiseCore.FillArray(System.Math.Max(szInputNames.Count, szRegressionNames.Count), -1);
            int[] aiCrossRef_R2I = clsPairwise.clsPairwaiseCore.FillArray(System.Math.Max(szInputNames.Count, szRegressionNames.Count), -1);
            for (int iInputCnt = 0; iInputCnt < szInputNames.Count; iInputCnt++)
            {
                for (int iRegCnt = 0; iRegCnt < szRegressionNames.Count; iRegCnt++)
                {
                    if (szInputNames[iInputCnt].Equals(szRegressionNames[iRegCnt]))
                    {
                        // input name has been found in regression
                        aiCrossRef_I2R[iInputCnt] = iRegCnt;
                        aiCrossRef_R2I[iRegCnt] = iInputCnt;
                        break;
                    }
                }
            }


            // make temporary matrix
            string[][] aaszTmpMatrix = new string[aaszOrgMatrix.GetLength(0)][];
            string[][] aaszNewParameters = new string[aaszOrgMatrix.GetLength(0)][];
            string[][] aaszObsoleteParameters = new string[aaszOrgMatrix.GetLength(0)][];

            for (int iCnt = 0; iCnt < aaszTmpMatrix.GetLength(0); iCnt++)
            {
                string[] aszEmpty = new string[szInputNames.Count];
                aaszTmpMatrix[iCnt] = aszEmpty;
            }

            for (int iParCnt = 0; iParCnt < aiCrossRef_I2R.Length; iParCnt++)
            {
                if (aiCrossRef_I2R[iParCnt] < 0)
                {
                    // do nothing; has a -1 reference
                }
                else
                {
                    for (int iCnt = 0; iCnt < aaszTmpMatrix.GetLength(0); iCnt++)
                    {
                        aaszTmpMatrix[iCnt][iParCnt] = aaszOrgMatrix[iCnt][aiCrossRef_I2R[iParCnt]];
                    }
                }
            }

            initClass(aaszTmpMatrix);
        }

        public void addParamName(string aParamName)
        {
            if (Rows != 0)
                throw new Exception("Cannot add parameter name while values are already added");
            ParamNames.Add(aParamName);
            Cols += 1;
        }
        public void addValues(List<clsTestsetResultItem> lstValues)
        {
            ParamValues.AddRange(lstValues);
            Rows = ParamValues.Count;
        }
        public string[] getValues(int iRow)
        {
            return ParamValues[iRow].szParamValues;
        }

        public void sort()
        {
            Test2Lib.qsort<clsTestsetResultItem>.Sort(ParamValues, clsTestsetResultItem.comparer);
        }

        //public static void Sort(List<T> numArray, dlgParCompare comparer, params object[] args)
        public void sort(int[] iOrder)
        {
            object[] ao = new object[] { iOrder };
            Test2Lib.qsort<clsTestsetResultItem>.Sort(ParamValues, clsTestsetResultItem.comparerOrder, ao);
        }

        public string genText()
        {
            //int iColumns = aaResults[0].Length;
            int[] iColumnWidth = new int[Cols];
            for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
                if (iColumnWidth[iColCnt] < ParamNames[iColCnt].Length)
                    iColumnWidth[iColCnt] = ParamNames[iColCnt].Length;
            for (int iRowCnt = 0; iRowCnt < Rows; iRowCnt++)
                for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
                    if (iColumnWidth[iColCnt] < ParamValues[iRowCnt].szParamValues[iColCnt].Length)
                        iColumnWidth[iColCnt] = ParamValues[iRowCnt].szParamValues[iColCnt].Length;

            string szText = "." + Stringsupport.addSpacesToString("", 6);
            //szNew += iRowCnt.ToString();
            //if (iRowCnt != 0)
            //    szNew = iRowCnt.ToString();
            for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
            {
                string szVal = ParamNames[iColCnt];
                szVal = Stringsupport.addSpacesToString(szVal, iColumnWidth[iColCnt]);
                szText += " " + szVal;
            }
            szText += "\r\n";
            for (int iRowCnt = 0; iRowCnt < Rows; iRowCnt++)
            {
                string szNew = "";
                switch (ParamValues[iRowCnt].eType)
                {
                    case E_TESTSET_TYPE.NEW:
                        szNew = "+";
                        break;
                    case E_TESTSET_TYPE.OBSOLETE:
                        szNew = "x";
                        break;
                    case E_TESTSET_TYPE.REGRESSION:
                        szNew = " ";
                        break;
                    default:
                        throw new Exception("Unknown parameter type");
                }
                szNew += " " + iRowCnt.ToString();
                szNew = Stringsupport.addSpacesToString(szNew, 6);

                for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
                {
                    string szVal = ParamValues[iRowCnt].szParamValues[iColCnt];
                    szVal = Stringsupport.addSpacesToString(szVal, iColumnWidth[iColCnt]);
                    szNew += " " + szVal;
                }
                szText += "\r\n" + szNew;
            }

            return szText.Trim().Substring(1);
        }
        public string genRegressionText()
        {
            string szText = "";// "." + Stringsupport.addSpacesToString("", 6);

            string szParamLine = "";
            for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
            {
                string szVal = ParamNames[iColCnt];
                szParamLine += "," + szVal;
            }
            szText += szParamLine.Substring(1);
            for (int iRowCnt = 0; iRowCnt < Rows; iRowCnt++)
            {
                string szNew = "";

                for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
                {
                    string szVal = ParamValues[iRowCnt].szParamValues[iColCnt];
                    //szVal = Stringsupport.addSpacesToString(szVal, iColumnWidth[iColCnt]);
                    szNew += "," + szVal;
                }
                szText += "\r\n" + szNew.Substring(1);
            }

            return szText.Trim();
        }
        public string genCsv()
        {
            return "";
        }
        public string reverseEnginering()
        {
            // first get the parameter names
            //ParamNames
            // make array of lists containing all values on correct position
            List<string>[] aVarNames = new List<string>[ParamNames.Count];
            for (int iCnt = 0; iCnt < ParamNames.Count; iCnt++)
            {
                aVarNames[iCnt] = new List<string>();
            }

            // now fill all lists with the correct variable if it isn't added yet

            for (int iRowCnt = 0; iRowCnt < Rows; iRowCnt++)
            {
                for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
                {
                    string szVal = ParamValues[iRowCnt].szParamValues[iColCnt];
                    //szVal = Stringsupport.addSpacesToString(szVal, iColumnWidth[iColCnt]);
                    if (!aVarNames[iColCnt].IN(szVal))
                        aVarNames[iColCnt].Add(szVal);
                }
            }

            string szText = "";
            for (int iColCnt = 0; iColCnt < Cols; iColCnt++)
            {
                string szNewLine = "";
                foreach (string szVal in aVarNames[iColCnt])
                {
                    szNewLine += ", " + szVal;
                }
                szNewLine = ParamNames[iColCnt] + ": " + szNewLine.Substring(2);
                szText += "\r\n" + szNewLine;
            }

            return szText.Substring(2);
        }

    }
}
