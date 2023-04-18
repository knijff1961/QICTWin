using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QICTWin
{
    class clsPairwiseArgs
    {
        string szText = "";
        string szFile = "";
        List<string> lstLines = null;
        string szRegression = "";
        bool bLog = false;
        ulong bLog_ex = 0x7fffffffffffffff;
        int iDepth = 2;
        int iPoolsize = 2;

        public string Text
        {
            get { return szText; }
            set { szText = value; }
        }
        public string File
        {
            get { return szFile; }
            set { szFile = value; }
        }
        public List<string> Lines
        {
            get { return lstLines; }
            set { lstLines = value; }
        }
        public string Regression
        {
            get { return szRegression; }
            set { szRegression = value; }
        }
        public ulong Log_ex
        {
            get { return bLog_ex; }
            set { bLog_ex = value; }
        }
        public bool Log
        {
            get { return bLog; }
            set { bLog = value; }
        }
        public int Depth
        {
            get { return iDepth; }
            set { iDepth = value; }
        }
        public int Poolsize
        {
            get { return iPoolsize; }
            set { iPoolsize = value; }
        }
    }
    enum E_TESTSET_TYPE
    {
        NEW,
        REGRESSION,
        OBSOLETE
    };
    class clsTestsetIndexItem
    {
        public E_TESTSET_TYPE eType = E_TESTSET_TYPE.NEW;
        public int[] iParamValueIdx;

        public clsTestsetIndexItem(E_TESTSET_TYPE eType, int[] iParamValueIdx)
        {
            this.eType = eType;
            this.iParamValueIdx = iParamValueIdx;
        }
    }
    class clsTestsetResultItem
    {
        public E_TESTSET_TYPE eType = E_TESTSET_TYPE.NEW;
        public string[] szParamValues;

        public clsTestsetResultItem(E_TESTSET_TYPE eType, string[] szParamValues)
        {
            this.eType = eType;
            this.szParamValues = szParamValues;
        }

        public static int comparer(clsTestsetResultItem T1, clsTestsetResultItem T2)
        {
            string[] a = T1.szParamValues;
            string[] b = T2.szParamValues;
            for (int iCnt = 0; iCnt < a.Length; iCnt++)
            {
                int iRes = string.Compare(a[iCnt], b[iCnt]);
                if (iRes != 0)
                    return iRes;
            }
            return 0;
        }
        public static int comparerOrder(clsTestsetResultItem T1, clsTestsetResultItem T2, object[] aoOrder)
        {
            int[] iOrder = (int[])aoOrder[0];
            string[] a = T1.szParamValues;
            string[] b = T2.szParamValues;
            for (int iCnt = 0; iCnt < iOrder.Length; iCnt++)
            {
                int iRes = string.Compare(a[iOrder[iCnt]], b[iOrder[iCnt]]);
                if (iRes != 0)
                    return iRes;
            }
            return 0;
        }

    }
    //List<int[]> testSets_new = null
    class clsPairwise
    {
        private static List<string> getFileLines(string file)
        {
            List<string> lstRet = new List<string>();

            // do a preliminary file read to determine number of parameters and number of parameter values
            FileStream fs = new FileStream(file, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                lstRet.Add(line);
            }
            sr.Close();
            fs.Close();
            return lstRet;
        }
        private static List<clsPairwiseLine> getLines(string file)
        {
            List<string> lstLines = getFileLines(file);
            return decodeLines(lstLines);
        }
        private static List<clsPairwiseLine> decodeLines(List<string> lstLines)
        {
            List<clsPairwiseLine> lstRet = new List<clsPairwiseLine>();
            for (int iCnt = 0; iCnt < lstLines.Count; iCnt++)
            {
                clsPairwiseLine cPairwiseLine = new clsPairwiseLine(lstLines[iCnt]);
                lstRet.Add(cPairwiseLine);
            }
            return lstRet;
        }


        private static List<clsPairwiseLine> text2PairwiseLines(string szText)
        {
            szText = szText.Replace("\r", "\n");
            szText = szText.Replace("\n\n", "\n");
            string[] aLines = szText.Split('\n');
            List<string> lstLines = new List<string>();
            foreach (string szLine in aLines)
                if (szLine.Trim().Length != 0)
                    lstLines.Add(szLine.Trim());
            if (lstLines.Count == 0)
                return new List<clsPairwiseLine>();
            return decodeLines(lstLines);
        }

        private static clsPairwiseParams text2string2dim(string szText)
        {
            szText = szText.Replace("\r", "\n");
            szText = szText.Replace("\n\n", "\n");
            string[] aLines = szText.Split('\n');

            List<string> lstLines = new List<string>();
            foreach (string szLine in aLines)
            {
                string szLine_ = szLine.Trim();
                if (szLine_.Length != 0)
                    lstLines.Add(szLine_);
            }
            if (lstLines.Count == 0)
                return null;

            string[][] aaszRet = new string[lstLines.Count][];
            int dim = lstLines[0].Split(',').Length;
            for (int iCnt = 0; iCnt < lstLines.Count; iCnt++)
            {
                string szLine = lstLines[iCnt];
                string[] szValues = lstLines[iCnt].Split(',');
                if (szValues.Length != dim)
                    throw new Exception("Dimension error in regression string:" + szLine);
                for (int iValCnt = 0; iValCnt < szValues.Length; iValCnt++)
                    szValues[iValCnt] = szValues[iValCnt].Trim();
                aaszRet[iCnt] = szValues;
            }
            
            return new clsPairwiseParams(aaszRet);
        }

        public static clsPairwiseParams Pairwise(clsPairwiseArgs cPairWiseArgs)
        {
            List<clsPairwiseLine> lstPairwiseLines = null;
            if (cPairWiseArgs.Text.Trim().Length != 0)
            {
                lstPairwiseLines = text2PairwiseLines(cPairWiseArgs.Text);
            }
            else if (cPairWiseArgs.File.Trim().Length != 0)
            {
                lstPairwiseLines = getLines(cPairWiseArgs.File);
                throw new Exception("TODO File:Cannot find data to process");
            }
            else if (cPairWiseArgs.Lines.Count != 0)
            {
                lstPairwiseLines = decodeLines(cPairWiseArgs.Lines);
                throw new Exception("TODO Lines:Cannot find data to process");
            }
            else
                throw new Exception("Cannot find data to process");

            clsPairwiseParams aaszRegression = text2string2dim(cPairWiseArgs.Regression);

            clsPairwaiseCore cPairwaiseCore = new clsPairwaiseCore();
            return cPairwaiseCore.Pairwise(lstPairwiseLines, aaszRegression, cPairWiseArgs);
        }

        public static clsPairwiseParams PairwiseFromText(string szText)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Text = szText };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromText(string szText, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Text = szText, Log = bLog };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromText(string szText, string szRegression, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Text = szText, Log = bLog, Regression = szRegression };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromList(List<string> lstLines)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Lines = lstLines };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromList(List<string> lstLines, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Lines = lstLines, Log = bLog };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromList(List<string> lstLines, string szRegression, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Lines = lstLines, Log = bLog, Regression = szRegression };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromFile(string file)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { File = file };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromFile(string file, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { File = file, Log = bLog };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }
        public static clsPairwiseParams PairwiseFromFile(string file, string szRegression, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { File = file, Log = bLog, Regression = szRegression };
            return clsPairwise.Pairwise(cPairWiseArgs);
        }

        public class CUnusedPairs
        {
            public int[] iCompact;
            public int[] aiDistributed;

            public CUnusedPairs(int[] iCompact, List<clsPairwiseLine> lstPairwiseLines)
            {
                this.iCompact = iCompact;
                aiDistributed = new int[lstPairwiseLines.Count];

                // set default -1 (not in compact)
                for (int i = 0; i < lstPairwiseLines.Count; i++)
                    aiDistributed[i] = -1;

                try
                {
                    int iLast = 0;
                    for (int i = 0; i < iCompact.Length; i++)
                    {
                        int iVal = iCompact[i];
                        for (int j = iLast; j < lstPairwiseLines.Count; j++)
                        {
                            clsPairwiseLine cPairwiseLine = lstPairwiseLines[j];
                            if (cPairwiseLine.iLegalValuesStart <= iVal && iVal <= cPairwiseLine.iLegalValuesEnd)
                            {
                                iLast = j + 1;
                                aiDistributed[j] = iVal;
                                break;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            public bool IN(int[] iTestset)
            {
                for (int i = 0; i < aiDistributed.Length; i++)
                {
                    if (aiDistributed[i] == -1)
                        continue;
                    if (iTestset[i] != aiDistributed[i])
                        return false;
                }
                return true;
            }

            public override string ToString()
            {
                string szTmp = clsPairwaiseCore.arrayToString(iCompact) + " " + clsPairwaiseCore.arrayToString(aiDistributed);
                return szTmp;
            }
        }
        public class clsPairwaiseCore
        {

            #region Support functions and class variables
            public static string arrayToString(int[] iArray)
            {
                string szRet = "";
                for (int i = 0; i < iArray.Length; i++)
                    szRet += ", " + iArray[i];
                return "[" + szRet.Substring(2) + "]";
            }
            public static string arrayToString(string[] iArray)
            {
                string szRet = "";
                for (int i = 0; i < iArray.Length; i++)
                    szRet += ", " + iArray[i];
                return "[" + szRet.Substring(2) + "]";
            }
            int from(int iFrom, List<clsPairwiseLine> lstPairwiseLines)
            {
                int iRet = 0;

                for (int iCnt = iFrom; iCnt < lstPairwiseLines.Count; iCnt++)
                    iRet += lstPairwiseLines[iCnt].nrValues;
                return iRet;
            }
            int subCount(int iDepth, int iFrom, List<clsPairwiseLine> lstPairwiseLines)
            {
                int iRet = 0;

                if (iDepth == 2)
                {
                    for (int iCnt = iFrom + 1; iCnt < lstPairwiseLines.Count; iCnt++)
                        iRet += lstPairwiseLines[iCnt].nrValues;
                    iRet *= lstPairwiseLines[iFrom].nrValues;
                    return iRet;
                }

                iRet = subCount(iDepth - 1, iFrom + 1, lstPairwiseLines);

                return iRet * lstPairwiseLines[iFrom].nrValues;
            }
            List<int[]> makePairs(int iDepth, int iFrom, List<clsPairwiseLine> lstPairwiseLines)
            {
                int iRet = 0;

                if (iDepth == 2)
                {

                    List<int[]> lstRet2 = new List<int[]>();
                    for (int iCnt1 = lstPairwiseLines[iFrom].iLegalValuesStart; iCnt1 <= lstPairwiseLines[iFrom].iLegalValuesEnd; iCnt1++)
                        for (int iCnt2 = lstPairwiseLines[iFrom + 1].iLegalValuesStart; iCnt2 <= lstPairwiseLines[lstPairwiseLines.Count - 1].iLegalValuesEnd; iCnt2++)
                        {
                            int[] iPairs = new int[2];
                            iPairs[0] = iCnt1;
                            iPairs[1] = iCnt2;
                            lstRet2.Add(iPairs);
                        }
                    return lstRet2;
                }

                List<int[]> lstSub = makePairs(iDepth - 1, iFrom + 1, lstPairwiseLines);

                List<int[]> lstRet = new List<int[]>();
                for (int iCnt = lstPairwiseLines[iFrom].iLegalValuesStart; iCnt <= lstPairwiseLines[iFrom].iLegalValuesEnd; iCnt++)
                {
                    for (int iSubCnt = 0; iSubCnt < lstSub.Count; iSubCnt++)
                    {
                        List<int> lstTmp = new List<int>();
                        lstTmp.Add(iCnt);
                        lstTmp.AddRange(lstSub[iSubCnt]);
                        lstRet.Add(lstTmp.ToArray());
                    }

                }

                return lstRet;
            }

            int numberParameters = 0;
            int numberParameterValues = 0;
            //int numberPairs = 0;
            int numberPairs_new = 0;
            int poolSize = 20; // number of candidate testSet arrays to generate before picking one to add to testSets List

            int[][] legalValues = null; // in-memory representation of input file as ints
            string[] parameterValues = null; // one-dimensional array of all parameter values

            List<int[]> allPairsDisplay = null;
            List<CUnusedPairs> unusedPairs_new = null; // changes
            MultidimensionalSquareArray unusedPairsSearch = null; // square array -- changes

            int[] parameterPositions_new = null; // the parameter position for a given value
            int[] unusedCounts_new = null; // count of each parameter value in unusedPairs List
            List<clsTestsetIndexItem> testSets = null; // the main result data structure
            List<clsTestsetIndexItem> testSets_reg = null; // the main result data structure

            string szTmp = "";

            bool bLog;
            int iDepth;
            List<clsPairwiseLine> lstPairwiseLines;
            List<string> lstLogHistory = new List<string>();
            List<string> lstRegressionInfo = new List<string>();

            clsTest cTest;
            clsPairwiseParams aszRegression;
            ulong eLOG = 0x0;

            bool doLog(E_LOG eLog)
            {
                ulong lLog = (ulong)eLog;
                if ((eLOG & lLog) == lLog) return true;
                return false;
            }
            public int findParameter(string szParameter)
            {
                if (szParameter == null)
                    return -2;
                for (int iCnt = 0; iCnt < lstPairwiseLines.Count; iCnt++)
                {
                    if (lstPairwiseLines[iCnt].ParameterName.Equals(szParameter))
                        return iCnt;
                }
                return -1;
            }
            public void Message1(string szMessage)
            {
                frmPairwise.FORM.txtMessage1.Text = szMessage;
                System.Windows.Forms.Application.DoEvents();
                if (doLog(E_LOG.E_LOG_MESSAGES1_________________))
                    Log(szMessage);
            }
            public void Message2(string szMessage)
            {
                frmPairwise.FORM.txtMessage2.Text = szMessage;
                System.Windows.Forms.Application.DoEvents();
                if (doLog(E_LOG.E_LOG_MESSAGES2_________________))
                    Log(szMessage);
            }
            public void Message3(string szMessage)
            {
                frmPairwise.FORM.txtMessage3.Text = szMessage;
                System.Windows.Forms.Application.DoEvents();
                if (doLog(E_LOG.E_LOG_MESSAGES3_________________))
                    Log(szMessage);
            }
            #endregion

            public clsPairwiseParams Pairwise(List<clsPairwiseLine> lstPairwiseLines, clsPairwiseParams aszRegression, clsPairwiseArgs cPairWiseArgs)
            {
                Message1("Start core calculations");
                Random r = new Random(0);
                SimpleRNG r_new = new SimpleRNG(0);
                cTest = new clsTest();

                bLog = cPairWiseArgs.Log;
                iDepth = cPairWiseArgs.Depth;
                poolSize = cPairWiseArgs.Poolsize;
                eLOG = cPairWiseArgs.Log_ex;
                if (!bLog)
                    eLOG = 0;
                this.lstPairwiseLines = lstPairwiseLines;
                this.aszRegression = aszRegression;

                try
                {
                    MultidimensionalSquareArray.Test();
 
                    if (aszRegression != null)
                    {
                        // Normalise aszRegressionto meet lstPairwiseLines
                        // first do some tests
                        testRegressionParameterInput();
                        aszRegression.Normalise(lstPairwiseLines);
                        //clsPairwiseParams aszRegressionTmp = new clsPairwiseParams();
                        //aszRegressionTmp.Cols = aszRegression.Cols;
                        //aszRegressionTmp.Rows = aszRegression.Rows;

                        //List<string> szRegressionNames = new List<string>( aszRegression.ParamNames);
                        //List<string> szInputNames = new List<string>();
                        //foreach (clsPairwiseLine cPairwiseLine in lstPairwiseLines)
                        //    szInputNames.Add(cPairwiseLine.ParameterName);

                        //List<string> szRegressionNamesTmp = new List<string>();
                        //List<string> szInputNamesTmp = new List<string>();
                        //int[] aiCrossRef_I2R = FillArray(System.Math.Max(szInputNames.Count, szRegressionNames.Count), -1);
                        //int[] aiCrossRef_R2I = FillArray(System.Math.Max(szInputNames.Count, szRegressionNames.Count), -1);
                        //while (szInputNames.Count != 0 && szRegressionNames.Count != 0)
                        //{
                        //    for (int iInputCnt = 0; iInputCnt < szInputNames.Count; iInputCnt++)
                        //    {
                        //        for (int iRegCnt = 0; iRegCnt < szRegressionNames.Count; iRegCnt++)
                        //        {
                        //            if (szInputNames[iInputCnt].Equals(szRegressionNames[iRegCnt]))
                        //            {
                        //                // input name has been found in regression
                        //                aiCrossRef_I2R[iInputCnt] = iRegCnt;
                        //                aiCrossRef_R2I[iRegCnt] = iInputCnt;
                        //                // swap all values
                        //                szRegressionNamesTmp.Add(szRegressionNames[iRegCnt]);
                        //                szRegressionNames.RemoveAt(iRegCnt);
                        //                szInputNamesTmp.Add(szInputNames[iInputCnt]);
                        //                szInputNames.RemoveAt(iInputCnt);
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}

                        throw new Exception("End test");
                    }

                    testSets = new List<clsTestsetIndexItem>();  // primary data structure
                    prepare();
                    if (aszRegression != null)
                        handleRegression();

                    //==============================================================================================================
                    Message1("\nComputing testsets which capture all possible pairs . . .");
                    while (unusedPairs_new.Count > 0) // as long as ther are unused pairs to account for . . .
                    {
                        Message2("Still processing " + unusedPairs_new.Count + " possible pairs");
                        int[][] candidateSets = genCandidates(r_new);


                        // Iterate through candidateSets to determine the best candidate
                        int indexOfBestCandidate_new = r_new.Next(candidateSets.Length); // pick a random index as best
                        int mostPairsCaptured_new = NumberPairsCaptured(candidateSets[indexOfBestCandidate_new], unusedPairsSearch);

                        int[] bestTestSet = new int[numberParameters];
                        int[] bestTestSet_new = new int[numberParameters];

                        for (int i = 0; i < candidateSets.Length; ++i)
                        {
                            int pairsCaptured = NumberPairsCaptured(candidateSets[i], unusedPairsSearch);
                            if (pairsCaptured > mostPairsCaptured_new)
                            {
                                mostPairsCaptured_new = pairsCaptured;
                                indexOfBestCandidate_new = i;
                            }
                            Message3("Candidate " + i + " captured " + mostPairsCaptured_new);
                        }

                        if (doLog(E_LOG.E_CORE_CANDIDATE_NUMBER_________))
                            Log("Candidate number " + indexOfBestCandidate_new + " is best");
                        candidateSets[indexOfBestCandidate_new].CopyTo(bestTestSet, 0);

                        testSets.Add(new clsTestsetIndexItem(E_TESTSET_TYPE.NEW, bestTestSet)); // Add the best candidate to the main testSets List

                        if (update(bestTestSet) == 0)
                            throw new Exception("Strange error detected");
                    } // primary while loop

                    // Display results
                    return getResults();
                }
                catch (Exception ex)
                {
                    Log("Fatal: " + ex.Message);
                    return null;
                }
            } // Main()

            public void prepare()
            {
                Message1("Begin pair-wise testset generation");

                // do a preliminary file read to determine number of parameters and number of parameter values
                Message2("Determine number of parameters and values");

                numberParameters = lstPairwiseLines.Count;
                numberParameterValues = 0;
                for (int iCnt = 0; iCnt < lstPairwiseLines.Count; iCnt++)
                    numberParameterValues += lstPairwiseLines[iCnt].nrValues;

                if (doLog(E_LOG.E_PREPARE_______________________))
                {
                    Log("There are " + numberParameters + " parameters");
                    Log("There are " + numberParameterValues + " parameter values");
                }

                // now do a second scan to create the legalValues array, and the parameterValues array
                Message2("Determine the legalValues array, and the parameterValues array");
                legalValues = new int[numberParameters][];
                parameterValues = new string[numberParameterValues];
                int currRow = 0;
                int kk = 0; // points into parameterValues

                // get parameters and correcponding values
                for (int iCnt = 0; iCnt < lstPairwiseLines.Count; iCnt++)
                {
                    clsPairwiseLine cPairwiseLine = lstPairwiseLines[iCnt];
                    string[] strValues = cPairwiseLine.lstParameterValues.ToArray(); // lineTokens[1].Split(','); // pull out the individual parameter values into an array
                    int[] values = new int[strValues.Length]; // create small row array for legalValues

                    cPairwiseLine.iLegalValuesStart = kk;
                    for (int i = 0; i < strValues.Length; ++i) // trim whitespace
                    {
                        strValues[i] = strValues[i].Trim();
                        values[i] = kk;
                        parameterValues[kk] = strValues[i];
                        ++kk;
                    }
                    cPairwiseLine.iLegalValuesEnd = kk - 1;

                    legalValues[currRow++] = values;
                } // while

                if (doLog(E_LOG.E_PREP_PARAMETER_VALUES_________))
                {
                    Log("Parameter values: ");
                    string szLog = "";
                    for (int i = 0; i < parameterValues.Length; ++i)
                        szLog += parameterValues[i] + " ";
                    Log(szLog);
                }

                if (doLog(E_LOG.E_PREP_INTERNAL_PARAMETER_VALUES))
                {
                    Log("Legal values internal representation: ");
                    for (int i = 0; i < legalValues.Length; ++i)
                    {
                        string szLog = "Parameter" + i + " - " + lstPairwiseLines[i].ParameterName + ": ";
                        for (int j = 0; j < legalValues[i].Length; ++j)
                        {
                            szLog += legalValues[i][j] + " ";
                        }
                        Log(szLog);
                    }
                }
                // determine the number of pairs for this input set
                Message2("Determine the number of pairs for this input set");
                numberPairs_new = 0;
                for (int iCnt = 0; iCnt < lstPairwiseLines.Count - iDepth + 1; iCnt++)
                {
                    numberPairs_new += subCount(iDepth, iCnt, lstPairwiseLines);
                }

                if (doLog(E_LOG.E_PREPARE_______________________))
                    Log("\nThere are " + numberPairs_new + " pairs ");

                Message2("Generating possible pairs");
                allPairsDisplay = new List<int[]>();
                for (int iCnt = 0; iCnt < lstPairwiseLines.Count - iDepth + 1; iCnt++)
                {
                    allPairsDisplay.AddRange(makePairs(iDepth, iCnt, lstPairwiseLines));
                }

                Message2("Determine unused pairs and search array for unused pairs");
                unusedPairs_new = new List<CUnusedPairs>();
                int d = (int)Math.Pow(parameterValues.Length, iDepth);
                unusedPairsSearch = new MultidimensionalSquareArray(iDepth, parameterValues.Length);
                for (int i = 0; i < numberPairs_new; ++i)
                {
                    int[] iOrg = allPairsDisplay[i];
                    int[] iTmp = new int[iDepth];
                    int iUnusedPairSearch = 0;

                    for (int j = 0; j < iDepth; j++)
                    {
                        iTmp[j] = iOrg[j];
                        iUnusedPairSearch *= numberParameterValues;
                        iUnusedPairSearch += iOrg[j];
                    }
                    if (iUnusedPairSearch >= d)
                        Log("");
                    unusedPairsSearch.set(1, iOrg);
                    CUnusedPairs cCUnusedPairs = new CUnusedPairs(iTmp, lstPairwiseLines);
                    unusedPairs_new.Add(cCUnusedPairs);
                }

                if (doLog(E_LOG.E_PREP_UNUSED_PAIRS_SEARCH______))
                {
                    Log("unusedPairsSearch array:");
                    for (int i = 0; i < parameterValues.Length; i++)
                    {
                        string szTmp = "";
                        for (int j = 0; j < parameterValues.Length; j++)
                        {
                            int iVal = unusedPairsSearch.get(i, j);
                            if (iVal != 0)
                            {
                                if (iVal != 1)
                                    Console.WriteLine("Strange unusedPairsSearch_new value");
                                szTmp += "x";
                            }
                            else
                            {
                                szTmp += ".";
                            }
                        }
                        Log(szTmp);
                    }
                }

                if (doLog(E_LOG.E_PREP_DISPLAY_ALL_PAIRS________))
                    LogNew();

                if (doLog(E_LOG.E_PREP_DISPLAY_ALL_PAIRS2_______))
                {
                    Log("allPairsDisplay array:");
                    for (int row = 0; row < numberParameterValues; ++row)
                    {
                        //List<int[]> allPairsDisplay = null;
                        int[] aiTmp = allPairsDisplay[row];
                        szTmp = "";
                        for (int col = 0; col < aiTmp.Length; ++col)
                        {
                            szTmp += aiTmp[col] + " ";
                        }
                        Log(szTmp);
                    }
                }
                // process legalValues to populate parameterPositions array
                Message2("Process legalValues to populate parameterPositions array");
                int k = 0;  // points into parameterPositions
                parameterPositions_new = new int[numberParameterValues]; // the indexes are parameter values, the cell values are positions within a testSet
                for (int i = 0; i < legalValues.Length; ++i)
                {
                    int[] curr = legalValues[i];
                    for (int j = 0; j < curr.Length; ++j)
                    {
                        parameterPositions_new[k++] = i;
                    }
                }

                if (doLog(E_LOG.E_PREP_PARAMETER_POSITIONS______))
                {
                    Log("parameterPositions:");
                    szTmp = "";
                    for (int i = 0; i < parameterPositions_new.Length; ++i)
                    {
                        szTmp += parameterPositions_new[i].ToString() + " ";
                    }
                    Log(szTmp);
                }

                Message2("Determine unused count array for parameters");
                unusedCounts_new = new int[numberParameterValues];  // inexes are parameter values, cell values are counts of how many times the parameter value apperas in the unusedPairs collection
                for (int i = 0; i < allPairsDisplay.Count; ++i)
                {
                    int[] iTmp = allPairsDisplay[i];
                    for (int j = 0; j < iTmp.Length; j++)
                        ++unusedCounts_new[allPairsDisplay[i][j]];
                }

                if (doLog(E_LOG.E_PREP_UNUSED_COUNTS____________))
                {
                    Log("unusedCounts: ");
                    szTmp = "";
                    for (int i = 0; i < unusedCounts_new.Length; ++i)
                    {
                        szTmp += unusedCounts_new[i].ToString() + " ";
                    }
                    Log("");
                }
            }

            enum E_REGRESSION_TYPES
            {
                E_UNKNOWN = 0x0,
                E_NEW_PARAMETERS = 0x1,
                E_OBSOLETE_PARAMETERS = 0x2,
                E_NEW_VALUES = 0x4,
                E_OBSOLETE_VALUES = 0x8
            }
            int regressionTestResult;

            public List<string> getObsoleteParameters()
            {
                List<string> szReturn = new List<string>();
                // get all parameter names of regression
                List<string> szRegressionNames = aszRegression.ParamNames;
                List<string> szInputNames = new List<string>();
                foreach (clsPairwiseLine cPairwiseLine in lstPairwiseLines)
                    szInputNames.Add(cPairwiseLine.ParameterName);

                foreach (string szRegressionName in szRegressionNames)
                {
                    if (!szInputNames.IN(szRegressionName))
                        szReturn.Add(szRegressionName);
                }
                return szReturn;
            }
            public List<string> getNewParameters()
            {
                List<string> szReturn = new List<string>();
                // get all parameter names of regression
                List<string> szRegressionNames = aszRegression.ParamNames;
                List<string> szInputNames = new List<string>();
                foreach (clsPairwiseLine cPairwiseLine in lstPairwiseLines)
                    szInputNames.Add(cPairwiseLine.ParameterName);

                foreach (string szInputName in szInputNames)
                {
                    if (!szRegressionNames.IN(szInputName))
                        szReturn.Add(szInputName);
                }
                return szReturn;
            }

            public int testRegressionParameterInput()
            {
                int testParameterResult = 0;

                if (getObsoleteParameters().Count != 0)
                    testParameterResult = CNum.bitSet(testParameterResult, (int)E_REGRESSION_TYPES.E_OBSOLETE_PARAMETERS);
                if (getNewParameters().Count != 0)
                    testParameterResult = CNum.bitSet(testParameterResult, (int)E_REGRESSION_TYPES.E_NEW_PARAMETERS);

                return testParameterResult;
            }

            public void handleRegression()
            {

                regressionTestResult = 0;
                //int testParameterResult = testRegressionParameterInput();
                //#region LOG
                //if (testParameterResult != 0)
                //{
                //    if (CNum.bitTst(testParameterResult, (int)E_REGRESSION_TYPES.E_OBSOLETE_PARAMETERS))
                //    {
                //        List<string> lstObsoleteNames = getObsoleteParameters();
                //        string szTmp = "Found " + lstObsoleteNames.Count + " obsolete parameters:";
                //        szTmp += "   " + arrayToString(lstObsoleteNames.ToArray());
                //        Log(szTmp);
                //    }
                //    if (CNum.bitTst(testParameterResult, (int)E_REGRESSION_TYPES.E_NEW_PARAMETERS))
                //    {
                //        List<string> lstNewNames = getNewParameters();
                //        string szTmp = "Found " + lstNewNames.Count + " new parameters:";
                //        szTmp += "   " + arrayToString(lstNewNames.ToArray());
                //        Log(szTmp);
                //    }
                //}
                //#endregion

                testSets_reg = new List<clsTestsetIndexItem>();
                // first test if parameters can be found
                //string[] aszParameters = aszRegression.aParamNames;
                int iNrRegressionItems = aszRegression.Rows;
                int dim = aszRegression.Cols;

                int[] iRegParams = FillArray(dim, -1);
                for (int iCnt = 0; iCnt < dim; iCnt++)
                {
                    iRegParams[iCnt] = findParameter(aszRegression.ParamNames[iCnt]);
                }
                for (int iCnt = 0; iCnt < dim; iCnt++)
                {
                    if (iRegParams[iCnt] == -1)
                        throw new Exception("TODO Cannot determine all parameters");
                    //if (iRegParams[iCnt] != iCnt)
                    //    throw new Exception("TODO must nake parameter name independent");
                }

                // make new testcase set for testcases which are already defined
                for (int iTestCnt = 0; iTestCnt < iNrRegressionItems; iTestCnt++)
                {
                    int[] iTestsetReg = new int[dim];
                    string[] aszTestCase = aszRegression.getValues(iTestCnt);
                    for (int iValCnt = 0; iValCnt < dim; iValCnt++)
                    {
                        iTestsetReg[iValCnt] = -1;
                    }
                    for (int iValCnt = 0; iValCnt < dim; iValCnt++)
                    {
                        string szVal = aszTestCase[iValCnt];
                        clsPairwiseLine cPairwiseLine = lstPairwiseLines[iRegParams[iValCnt]];
                        for (int iCnt = 0; iCnt < cPairwiseLine.lstParameterValues.Count; iCnt++)
                        {
                            if (cPairwiseLine.lstParameterValues[iCnt].Equals(szVal))
                            {
                                iTestsetReg[iValCnt] = cPairwiseLine.iLegalValuesStart + iCnt;
                                break;
                            }
                        }
                    }
                    int[] iObsoleteValue = new int[dim];
                    bool bHasError = false;
                    for (int iValCnt = 0; iValCnt < dim; iValCnt++)
                    {
                        if (iTestsetReg[iValCnt] == -1)
                        {
                            lstRegressionInfo.Add(arrayToString(aszTestCase) + " has unknow parameter value (" + aszTestCase[iValCnt] + ")");
                            bHasError = true;
                        }
                    }
                    if (bHasError) continue;
                    clsTestsetIndexItem clsTestsetItem = new clsTestsetIndexItem(update(iTestsetReg) == 0 ? E_TESTSET_TYPE.OBSOLETE : E_TESTSET_TYPE.REGRESSION, iTestsetReg);
                    testSets_reg.Add(clsTestsetItem);
                }
            }
            public clsPairwiseParams getResults()
            {
                clsPairwiseParams cPairwiseParams = new clsPairwiseParams();
                //List<string> lstLogHistory = new List<string>();
                //List<string> lstRegressionInfo = new List<string>();
                cPairwiseParams.lstHistoryInfo = lstLogHistory;
                cPairwiseParams.lstRegressionInfo = lstRegressionInfo;

                List<string[]> lstRet = new List<string[]>();
                if (doLog(E_LOG.E_FINAL_________________________))
                    Log("\nResult testsets: \n");

                lstRet.Add(new string[lstPairwiseLines.Count + 1]);
                lstRet[0][0] = "#     ";
                for (int iCnt = 0; iCnt < lstPairwiseLines.Count; iCnt++)
                    cPairwiseParams.addParamName(lstPairwiseLines[iCnt].ParameterName);
                //lstRet[0][iCnt] = lstPairwiseLines[iCnt - 1].ParameterName;

                if (testSets_reg == null)
                {
                    //lstRet.AddRange(getResults("", testSets));
                    cPairwiseParams.addValues(getResults("", testSets));
                    //return lstRet.ToArray();
                }
                else
                {
                    cPairwiseParams.addValues(getResults("-", testSets_reg));
                    cPairwiseParams.addValues(getResults("n", testSets));
                }
                if (doLog(E_LOG.E_FINAL_________________________))
                    Log("\nEnd\n");

                return cPairwiseParams;
                //return lstRet.ToArray();
            }
            public List<clsTestsetResultItem> getResults(string szPre, List<clsTestsetIndexItem> testSets)
            {
                List<clsTestsetResultItem> aResults = new List<clsTestsetResultItem>();
                for (int i = 0; i < testSets.Count; ++i)
                {
                    string szLog = "";
                    szLog += i.ToString().PadLeft(3) + ": ";
                    clsTestsetIndexItem curr = testSets[i];
                    string[] aResult = new string[numberParameters];
                    //aResult[0] = szPre + i.ToString();
                    for (int j = 0; j < numberParameters; ++j)
                    {
                        szLog += parameterValues[curr.iParamValueIdx[j]] + " ";
                        aResult[j] = parameterValues[curr.iParamValueIdx[j]];
                    }
                    aResults.Add(new clsTestsetResultItem(curr.eType, aResult));
                    if (doLog(E_LOG.E_FINAL_________________________))
                        Log(szLog);
                }

                return aResults;

            }
            static string getValue(int iRefNr, List<clsPairwiseLine> lstPairwiseLines, string[] parameterValues)
            {
                string szRet = "";
                foreach (clsPairwiseLine cPairwiseLine in lstPairwiseLines)
                {
                    if (iRefNr >= cPairwiseLine.iLegalValuesStart && iRefNr <= cPairwiseLine.iLegalValuesEnd)
                    {
                        szRet = Stringsupport.addCharsToString(iRefNr.ToString(), '.', 5) + " {" + cPairwiseLine.ParameterName + " - " + parameterValues[iRefNr] + "}";
                        return szRet;
                    }
                }
                return szRet;
            }

            int NumberPairsCaptured(int[] ts, MultidimensionalSquareArray unusedPairsSearch_new)  // number of unused pairs captured by testSet ts 
            {
                int ans = 0;
                for (int i = 0; i <= ts.Length - 2; ++i)
                {
                    for (int j = i + 1; j <= ts.Length - 1; ++j)
                    {
                        // TODO update to multple dimension
                        if (unusedPairsSearch_new.get(ts[i], ts[j]) == 1)
                            //if (unusedPairsSearch[ts[i], ts[j]] == 1)
                            ++ans;
                    }
                }
                return ans;
            } // NumberPairsCaptured()

            public void Log(string szMessage)
            {
                if (doLog(E_LOG.E_CONSOLE_______________________))
                    Console.WriteLine(szMessage);
                szMessage = szMessage.Replace("\r", "");
                szMessage = szMessage.Replace("\n", "");
                lstLogHistory.Add(szMessage);
                string szTmp = "\"" + szMessage + "\",";
                //Console.WriteLine(szTmp);
                //string szRes = cTest.test(szMessage);
                //if (szRes.Length != 0)
                //{
                //    Console.WriteLine(szTmp);
                //}

            }

            public void LogNew()
            {

                //List<int[]> unusedPairs_new = null; // changes
                //MultidimensionalArraySupport unusedPairsSearch_new = null; // square array -- changes
                Log("allPairsDisplay array:");
                int[] iSizes = new int[iDepth];
                string[,] szDisplayValues = new string[numberPairs_new, iDepth];

                try
                {

                    // first determine sizes
                    for (int i = 0; i < numberPairs_new; ++i)
                    {

                        int[] iTmp = allPairsDisplay[i];
                        for (int iCnt = 0; iCnt < iDepth; iCnt++)
                        {
                            string szLogTmp = getValue(iTmp[iCnt], lstPairwiseLines, parameterValues);
                            szLogTmp += "-" + unusedPairs_new[i].iCompact[iCnt].ToString();
                            szDisplayValues[i, iCnt] = szLogTmp;
                            if (szLogTmp.Length > iSizes[iCnt]) iSizes[iCnt] = szLogTmp.Length;
                        }
                    }
                    for (int i = 0; i < numberPairs_new; ++i)
                    {
                        int[] iTmp = allPairsDisplay[i];
                        string szLogTmp = i.ToString();
                        szLogTmp += "-" + unusedPairsSearch.get(iTmp).ToString();
                        szLogTmp = Stringsupport.addSpacesToString(szLogTmp, 7);
                        for (int iCnt = 0; iCnt < iDepth; iCnt++)
                        {
                            szLogTmp += "(" + Stringsupport.addSpacesToString(szDisplayValues[i, iCnt], iSizes[iCnt]) + ")  ";
                        }
                        Log(szLogTmp);
                    }
                }
                catch (Exception ex)
                {
                }

            }

            public int[][] genCandidates(SimpleRNG r_new)
            {
                int[][] candidateSets = new int[poolSize][]; // holds candidate testSets

                for (int candidate = 0; candidate < poolSize; ++candidate)
                {
                    Message3("Generate candidate testSets (" + (candidate + 1) + " of " + poolSize + ")");

                    // pick "best" unusedPair -- the pair which has the sum of the most unused values
                    int bestWeight = 0;
                    int indexOfBestPair = 0;

                    for (int i = 0; i < unusedPairs_new.Count; ++i)
                    {
                        int[] curr = unusedPairs_new[i].iCompact;
                        int weight = 0;
                        for (int j = 0; j < curr.Length; j++)
                            weight += unusedCounts_new[curr[j]];
                        if (weight > bestWeight)
                        {
                            bestWeight = weight;
                            indexOfBestPair = i;
                        }
                    }

                    int[] best = new int[iDepth]; // a copy is not strictly necesary here
                    unusedPairs_new[indexOfBestPair].iCompact.CopyTo(best, 0);

                    #region LOG
                    if (doLog(E_LOG.E_CORE_BEST_PAIR________________))
                    {
                        szTmp = "";
                        for (int i = 0; i < iDepth; i++)
                            szTmp += ", " + best[i];
                        Log("Best pair is " + szTmp.Substring(2) + " at " + indexOfBestPair + " with weight " + bestWeight);
                    }
                    #endregion

                    // aPosArray contains the selected pair
                    int[] aPosArray = new int[iDepth];
                    for (int i = 0; i < iDepth; i++)
                        aPosArray[i] = parameterPositions_new[best[i]];

                    #region LOG
                    if (doLog(E_LOG.E_CORE_BEST_PAIR_POSITION_______))
                    {
                        szTmp = "";
                        for (int i = 0; i < iDepth; i++)
                            szTmp += ", " + aPosArray[i];
                        Log("The best pair belongs at positions [" + szTmp.Substring(2) + "]");
                    }
                    #endregion

                    // generate a random order to fill parameter positions
                    int[] ordering_new = new int[numberParameters];
                    for (int i = 0; i < numberParameters; ++i) // initially all in order
                        ordering_new[i] = i;

                    // put firstPos at ordering[0] && secondPos at ordering[1]
                    ordering_new[0] = aPosArray[0];
                    ordering_new[aPosArray[0]] = 0;
                    for (int i = 1; i < iDepth; i++)
                    {
                        int t_new = ordering_new[i];
                        ordering_new[i] = aPosArray[i];
                        ordering_new[aPosArray[1]] = t_new;
                    }

                    for (int i = iDepth; i < ordering_new.Length; i++)  // Knuth shuffle. start at i=2 because want first two slots left alone
                    {
                        int j = r_new.Next(i, ordering_new.Length);

                        int temp = ordering_new[j];
                        ordering_new[j] = ordering_new[i];
                        ordering_new[i] = temp;
                    }

                    ///////////
                    // PROBABLY LINK FROM HERE
                    // best contains the selected variable index,
                    // aPosArray contains the group where these variables can be found
                    // oredring new: first n groups are the already placed; the rest has to be determined
                    //////////
                    #region LOG
                    if (doLog(E_LOG.E_CORE_BEST_PAIR_ORDER__________))
                    {
                        Log("Order: ");
                        szTmp = "";
                        for (int i = 0; i < ordering_new.Length; ++i)
                            szTmp += ordering_new[i].ToString() + " ";
                        Log(szTmp);
                    }
                    #endregion

                    int[] testSet = FillArray(numberParameters, -1); // new int[numberParameters]; // make an empty candidate testSet

                    // place two parameter values from best unused pair into candidate testSet
                    for (int i = 0; i < best.Length; i++)
                        testSet[aPosArray[i]] = best[i];

                    #region LOG
                    if (doLog(E_LOG.E_CORE_PLACED_PARAMS____________))
                    {
                        string szTmpPosArray = "";
                        szTmp = "";
                        for (int i = 0; i < best.Length; i++)
                        {
                            szTmp += ", " + best[i];
                            szTmpPosArray += ", " + aPosArray[i];
                        }
                        Log("Placed params [" + szTmp.Substring(2) + "] at [" + szTmpPosArray.Substring(2) + "]");
                    }
                    #endregion

                    // for remaining parameter positions in candidate testSet, try each possible legal value, 
                    // picking the one which captures the most unused pairs . . .
                    ///////////////// iDepth must be changed by nr_pre_placed
                    for (int i = iDepth; i < numberParameters; ++i) // start at 2 because first two parameter have been placed
                    {
                        // get group number to be handled
                        int currPos = ordering_new[i];
                        // get all values belonging to this group
                        int[] possibleValues = legalValues[currPos];

                        #region LOG
                        if (doLog(E_LOG.E_CORE_POSSIBLE_VALUES__________))
                        {

                            Log("possibles are ");
                            for (int z = 0; z < possibleValues.Length; ++z)
                                Log(possibleValues[z].ToString());
                            Log("");
                        }
                        #endregion

                        int highestCount = 0;  // highest of these counts
                        int bestJ = 0;         // index of the possible value which yields the highestCount
                        for (int iCurrGrpItemCnt = 0; iCurrGrpItemCnt < possibleValues.Length; ++iCurrGrpItemCnt) // examine pairs created by each possible value and each parameter value already there
                        {
                            int currentCount_new = 0;  // count the unusedPairs grabbed by adding a possible value
                            for (int iPrePlacedCnt = 0; iPrePlacedCnt < i; ++iPrePlacedCnt)  // parameters already placed
                            {
                                /// this becomes tricky when a multiple depth is used; candidate pair must be multi dimensional, not only 2
                                int[] candidatePair = new int[] { possibleValues[iCurrGrpItemCnt], testSet[ordering_new[iPrePlacedCnt]] };
                                #region LOG
                                if (doLog(E_LOG.E_CORE_EXAMINE_PAIRS____________))
                                    Log("Considering pair " + possibleValues[iCurrGrpItemCnt] + ", " + testSet[ordering_new[iPrePlacedCnt]]);
                                #endregion

                                // because of the random order of positions, must check both possibilities
                                // in multi dimensional pairs, all possible options must be taken
                                // 2 dim: 0,1 & 1,0; 3 dim:0,1,2 0,2,1 1,0,2 1,2,0 2,1,0 2,0,1
                                // Can be made more efficient by:
                                //      1. sort candidatePair AND
                                //      2. Organize unusedPairsSearch such that only "sorted" vectors are filled
                                if (unusedPairsSearch.get(candidatePair[0], candidatePair[1]) == 1 ||
                                       unusedPairsSearch.get(candidatePair[1], candidatePair[0]) == 1)
                                {
                                    #region LOG
                                    if (doLog(E_LOG.E_CORE_EXAMINE_PAIRS____________))
                                        Log("Found " + candidatePair[0] + "," + candidatePair[1] + " in unusedPairs");
                                    #endregion
                                    ++currentCount_new;
                                }
                                else
                                {
                                    #region LOG
                                    if (doLog(E_LOG.E_CORE_EXAMINE_PAIRS____________))
                                        Log("Did NOT find " + candidatePair[0] + "," + candidatePair[1] + " in unusedPairs");
                                    #endregion
                                }

                            } // iPrePlacedCnt -- each previously placed paramter

                            if (currentCount_new > highestCount)
                            {
                                highestCount = currentCount_new;
                                bestJ = iCurrGrpItemCnt;
                            }

                        } // j -- each possible value at currPos
                        #region LOG
                        if (doLog(E_LOG.E_CORE_BEST_VALUE_______________))
                            Log("The best value is " + possibleValues[bestJ] + " with count = " + highestCount);
                        #endregion

                        testSet[currPos] = possibleValues[bestJ]; // place the value which captured the most pairs
                    } // i -- each testSet position 

                    #region LOG
                    if (doLog(E_LOG.E_CORE_ADD_CANDIDATE____________))
                    {
                        //=========
                        Log("\n============================");
                        Log("Adding candidate testSet to candidateSets array: ");
                        szTmp = "";
                        for (int i = 0; i < numberParameters; ++i)
                            szTmp += testSet[i].ToString() + " ";
                        Log(szTmp);
                        Log("============================\n");
                    }
                    #endregion
                    candidateSets[candidate] = testSet;  // add candidate testSet to candidateSets array
                } // for each candidate testSet

                #region LOG
                if (doLog(E_LOG.E_CORE_ALL_CANDIDATE_TESTSETS___))
                {
                    Log("Candidates testSets are: ");
                    for (int i = 0; i < candidateSets.Length; ++i)
                    {
                        szTmp = "";
                        int[] curr = candidateSets[i];
                        szTmp += i.ToString() + ": ";
                        for (int j = 0; j < curr.Length; ++j)
                        {
                            szTmp += curr[j].ToString() + " ";
                        }
                        Log(szTmp + " -- captures " + NumberPairsCaptured(curr, unusedPairsSearch));
                    }
                }
                #endregion

                return candidateSets;
            }
            public int update(int[] bestTestSet)
            {
                int iRet = 0;
                // now perform all updates
                Message3("Updating unusedPairs, unusedCounts and unusedPairsSearch");

                // try out all possible pairs in
                if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                    Log("bestTestSet:" + clsPairwaiseCore.arrayToString(bestTestSet));
                for (int p = unusedPairs_new.Count - 1; p >= 0; --p)
                {
                    if (unusedPairs_new[p].IN(bestTestSet))
                    {
                        if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                            Log("Decrementing the unused counts for " + clsPairwaiseCore.arrayToString(unusedPairs_new[p].iCompact));
                        foreach (int iV in unusedPairs_new[p].iCompact)
                            --unusedCounts_new[iV];

                        if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                            Log("Setting unusedPairsSearch at " + clsPairwaiseCore.arrayToString(unusedPairs_new[p].iCompact) + " to 0");
                        unusedPairsSearch.set(0, unusedPairs_new[p].iCompact);

                        if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                            Log("Removing " + unusedPairs_new[p].ToString() + " from unusedPairs List");
                        unusedPairs_new.RemoveAt(p);
                        iRet++;
                    }
                } // i
                if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                    Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                return iRet;
            }

            public static int[] FillArray(int iSize, int iVal)
            {
                int[] iRet = new int[iSize];
                for (int i = 0; i < iSize; i++)
                    iRet[i] = iVal;
                return iRet;
            }
        }
    }
}
