// <copyright file="clsPairwise.cs" company="Fouroaks">
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
using System.IO;

namespace QICTWin
{
    #region entry points / support classes
    class clsPairwiseArgs
    {
        bool bLog = false;
        ulong bLog_ex = 0x7fffffffffffffff;
        int iDepth = 2;
        int iPoolsize = 2;
        bool bPseudoRandom = true;

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
        public bool PseudoRandom
        {
            get { return bPseudoRandom; }
            set { bPseudoRandom = value; }
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
    public enum E_TESTSET_TYPE
    {
        NEW,
        REGRESSION,
        OBSOLETE
    };
    public class clsTestsetResultItem
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
    #endregion

    class clsPairwise
    {
        #region private functions / can be deleted???
        public static clsResults Pairwise(clsUserTestSet cUserTestset, clsRegression cRegTestSet, clsPairwiseArgs cPairWiseArgs)
        {
            clsPairwiseCore cPairwaiseCore = new clsPairwiseCore();
            return cPairwaiseCore.Pairwise(cUserTestset, cRegTestSet, cPairWiseArgs);
        }

        public static clsResults PairwiseFromText(clsUserTestSet cUserTestset, clsRegression cRegTestSet)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { };
            return clsPairwise.Pairwise(cUserTestset, cRegTestSet, cPairWiseArgs);
        }
        public static clsResults PairwiseFromText(clsUserTestSet cUserTestset, clsRegression cRegTestSet, bool bLog)
        {
            clsPairwiseArgs cPairWiseArgs = new clsPairwiseArgs { Log = bLog };
            return clsPairwise.Pairwise(cUserTestset, cRegTestSet, cPairWiseArgs);
        }
        #endregion

        public class clsPairwiseCore
        {
            clsUserTestSet cUserTestset;
            clsRegression cRegTestSet;

            #region Support functions and class variables
            public static string arrayToString(int[] iArray)
            {
                string szRet = "";
                for (int i = 0; i < iArray.Length; i++)
                    szRet += ", " + iArray[i];
                return "[" + szRet.Substring(2) + "]";
            }

            int m_poolSize = 20; // number of candidate testSet arrays to generate before picking one to add to testSets List

            MultidimensionalSquareArray m_unusedPairsSearch = null; // square array -- changes

            List<clsResult> m_testSets = null; // the main result data structure

            string m_szTmp = "";

            bool m_bLog;
            int m_iDepth;
            bool m_bPseudoRandom = true;

            List<string> m_lstRegressionInfo = new List<string>();

            clsTest m_cTest;
            ulong eLOG = 0x0;
            pairwiseRandom r_new;

            class pairwiseRandom
            {
                bool m_bPseudoRandom;
                Random r; // = new Random(0);
                SimpleRNG r_new; // = new SimpleRNG(0);

                public pairwiseRandom(uint iSeed, bool bPseudoRandom)
                {
                    m_bPseudoRandom = bPseudoRandom;
                    if (m_bPseudoRandom)
                        r_new = new SimpleRNG(iSeed);
                    else
                        r = new Random();
                }

                //r_new.Next
                public int Next(int iMax)
                {
                    if (m_bPseudoRandom)
                        return r_new.Next(iMax);
                    return r.Next(iMax);
                }
                public int Next(int iMin, int iMax)
                {
                    if (m_bPseudoRandom)
                        return r_new.Next(iMin,iMax);
                    return r.Next(iMin, iMax);
                }
            }
            #endregion

            public clsResults Pairwise(clsUserTestSet cUserTestset, clsRegression cRegTestSet, clsPairwiseArgs cPairWiseArgs)
            {
                Message1("Start core calculations");
                m_cTest = new clsTest();
                m_bPseudoRandom = cPairWiseArgs.PseudoRandom;
                r_new = new pairwiseRandom(0, m_bPseudoRandom);

                m_bLog = cPairWiseArgs.Log;
                m_iDepth = cPairWiseArgs.Depth;
                m_poolSize = cPairWiseArgs.Poolsize;
                eLOG = cPairWiseArgs.Log_ex;
                if (!m_bLog)
                    eLOG = 0;
                this.cUserTestset = cUserTestset;
                this.cRegTestSet = cRegTestSet;

                try
                {
                    m_testSets = new List<clsResult>();  // primary data structure
                    prepare();

                    //==============================================================================================================
                    Message1("\nComputing testsets which capture all possible pairs . . .");
                    while (cUserTestset.unusedPairs > 0) // as long as ther are unused pairs to account for . . .
                    {
                        Message2("Still processing " + cUserTestset.unusedPairs + " possible pairs");
                        int[][] candidateSets = genCandidates();


                        // Iterate through candidateSets to determine the best candidate
                        int indexOfBestCandidate_new = r_new.Next(candidateSets.Length); // pick a random index as best
                        int mostPairsCaptured_new = NumberPairsCaptured(candidateSets[indexOfBestCandidate_new], m_unusedPairsSearch);

                        int[] bestTestSet = new int[cUserTestset.nrParameters];
                        int[] bestTestSet_new = new int[cUserTestset.nrParameters];

                        for (int i = 0; i < candidateSets.Length; ++i)
                        {
                            int pairsCaptured = NumberPairsCaptured(candidateSets[i], m_unusedPairsSearch);
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

                if (doLog(E_LOG.E_PREPARE_______________________))
                {
                    Log("There are " + cUserTestset.nrParameters + " parameters");
                    Log("There are " + cUserTestset.nrValues + " parameter values");
                }

                // now do a second scan to create the legalValues array, and the parameterValues array
                Message2("Determine the legalValues array, and the parameterValues array");
                if (doLog(E_LOG.E_PREP_PARAMETER_VALUES_________))
                {
                    Log("Parameter values: ");
                    string szLog = "";
                    for (int iCnt = 0; iCnt < cUserTestset.nrValues; iCnt++)
                        szLog += cUserTestset.getUserValue(iCnt).Name + " ";
                    Log(szLog);
                }

                if (doLog(E_LOG.E_PREP_INTERNAL_PARAMETER_VALUES))
                {
                    Log("Legal values internal representation: ");
                    for (int i = 0; i < cUserTestset.nrParameters; ++i)
                    {
                        string szLog = "Parameter" + i + " - " + cUserTestset[i].Name + ": ";
                        for (int j = 0; j < cUserTestset[i].nrValues; ++j)
                        {
                            szLog += cUserTestset[i][j].LegalPos + " ";
                        }
                        Log(szLog);
                    }

                }

                // determine the number of pairs for this input set
                if (doLog(E_LOG.E_PREPARE_______________________))
                    Log("\nThere are " + cUserTestset.NumberPairs + " pairs ");

                Message2("Determine unused pairs and search array for unused pairs");
                m_unusedPairsSearch = new MultidimensionalSquareArray(m_iDepth, cUserTestset.nrValues);
                for (int i = 0; i < cUserTestset.NumberPairs; ++i)
                {
                    clsPair cPair = cUserTestset.UnassignedPairs[i];
                    m_unusedPairsSearch.setSquared(1, cPair.Pair);
                }
                if (doLog(E_LOG.E_PREP_UNUSED_PAIRS_SEARCH______))
                {
                    Log("unusedPairsSearch array: (only for 2 dimensional pairs)");
                    for (int i = 0; i < cUserTestset.nrValues; i++)
                    {
                        string szTmp = "";
                        for (int j = 0; j < cUserTestset.nrValues; j++)
                        {
                            int iVal = m_unusedPairsSearch.get(i, j);
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
                    for (int row = 0; row < cUserTestset.NumberPairs; ++row)
                    {
                        clsPair cPair = cUserTestset.UnassignedPairs[row];
                        string szValues = "";
                        string szNames = "";
                        foreach (int item in cPair.Pair)
                        {
                            szValues += item.ToString() + " ";
                            szNames += cUserTestset.getUserValue(item).Name + " ";
                        }
                        Log(szValues + " {" + szNames + "}");
                    }
                }

                // process legalValues to populate parameterPositions array
                if (doLog(E_LOG.E_PREP_PARAMETER_POSITIONS______))
                {
                    Log("parameterPositions:");
                    m_szTmp = "";
                    for (int i = 0; i < cUserTestset.nrValues; ++i)
                    {
                        m_szTmp += cUserTestset.getValueOfParamater(i) + " ";
                    }
                    Log(m_szTmp);
                }

                if (doLog(E_LOG.E_PREP_UNUSED_COUNTS____________))
                {
                    Log("unusedCounts: ");
                    m_szTmp = "";
                    for (int i = 0; i < cUserTestset.nrValues; ++i)
                    {
                        m_szTmp += cUserTestset.getUserValue(i).UnusedCount.ToString() + " ";
                    }
                    Log(m_szTmp);
                }
            }

            public clsResults getResults()
            {
                clsResults cFinalResult = new clsResults();
                cFinalResult.lstRegressionInfo = m_lstRegressionInfo;
                cFinalResult.lstResultLines = m_testSets;
                cFinalResult.cUserTestSet = cUserTestset;
                cFinalResult.cRegTestSet = cRegTestSet;

                List<string[]> lstRet = new List<string[]>();
                if (doLog(E_LOG.E_FINAL_________________________))
                    Log("\nResult testsets: \n");

                lstRet.Add(new string[cUserTestset.nrParameters + 1]);
                lstRet[0][0] = "#     ";

                //cPairwiseParams.addValues(getResults("", m_testSets));
                //if (doLog(E_LOG.E_FINAL_________________________))
                //    Log("\nEnd\n");
                cFinalResult.finalize(cUserTestset, cRegTestSet);
                return cFinalResult;
            }
            public List<clsTestsetResultItem> getResults(string szPre, List<clsResult> testSets)
            {
                List<clsTestsetResultItem> aResults = new List<clsTestsetResultItem>();
                for (int i = 0; i < testSets.Count; ++i)
                {
                    string szLog = "";
                    szLog += i.ToString().PadLeft(3) + ": ";
                    clsResult curr = testSets[i];
                    string[] aResult = new string[cUserTestset.nrParameters];
                    for (int j = 0; j < cUserTestset.nrParameters; ++j)
                    {
                        szLog += cUserTestset.getUserValue(curr.iParamValueIdx[j]).Name + " ";
                        aResult[j] = cUserTestset.getUserValue(curr.iParamValueIdx[j]).Name;
                    }
                    aResults.Add(new clsTestsetResultItem(curr.eType, aResult));
                    if (doLog(E_LOG.E_FINAL_________________________))
                        Log(szLog);
                }

                return aResults;

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
                clsGlobal.lstLogHistory.Add(szMessage);
                string szTmp = "\"" + szMessage + "\",";
                Console.WriteLine(szTmp);
                //string szRes = cTest.test(szMessage);
                //if (szRes.Length != 0)
                //{
                //    Console.WriteLine(szTmp);
                //}

            }


            private int[] genOrdering(int[] aPosArray)
            {
                List<int> lst1 = new List<int>();
                for (int i = 0; i < cUserTestset.nrParameters; ++i) // initially all in order
                    lst1.Add(i);

                // first unselect items from list
                for (int i = 0; i < aPosArray.Length; i++)
                {
                    lst1[aPosArray[i]] = -1;
                }

                // now add the unslected items at front of the list
                List<int> ordering = new List<int>();
                for (int i = 0; i < aPosArray.Length; i++)
                {
                    ordering.Add(aPosArray[i]);
                }

                for (int i = 0; i < lst1.Count; i++)
                {
                    if (lst1[i] != -1)
                        ordering.Add(lst1[i]);
                }


                // generate a random order to fill parameter positions
                int[] ordering_new = ordering.ToArray();
                for (int i = aPosArray.Length; i < ordering_new.Length; i++)  // Knuth shuffle. start at i=2 because want first two slots left alone
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
                    m_szTmp = "";
                    for (int i = 0; i < ordering_new.Length; ++i)
                        m_szTmp += ordering_new[i].ToString() + " ";
                    Log(m_szTmp);
                }
                #endregion
                return ordering_new;
            }

            public int[][] genCandidates()
            {
                int[] iRegResult = null;
                if (cRegTestSet != null)
                    iRegResult = cRegTestSet.getRegressionResult();

                int[][] candidateSets;
                if (iRegResult == null)
                {
                    candidateSets = new int[m_poolSize][]; // holds candidate testSets
                }
                else
                {
                    candidateSets = new int[1][];
                }
                for (int candidate = 0; candidate < candidateSets.GetLength(0); ++candidate)
                {
                    Message3("Generate candidate testSets (" + (candidate + 1) + " of " + m_poolSize + ")");

                    int[] ordering_new;
                    int[] testSet;
                    string szTmp;
                    int iDepth = m_iDepth;


                    if (iRegResult == null)
                    {
                        // pick "best" unusedPair -- the pair which has the sum of the most unused values
                        int[] aPosArray;
                        int bestWeight = 0;
                        int indexOfBestPair = 0;

                        for (int i = 0; i < cUserTestset.UnassignedPairs.Count; ++i)
                        {
                            int[] curr = cUserTestset.UnassignedPairs[i].iCompact;
                            int weight = 0;
                            for (int j = 0; j < curr.Length; j++)
                            {
                                weight += cUserTestset.getUserValue(curr[j]).UnusedCount;
                            }
                            if (weight > bestWeight)
                            {
                                bestWeight = weight;
                                indexOfBestPair = i;
                            }
                        }

                        int[] best = new int[iDepth]; // a copy is not strictly necesary here
                        cUserTestset.UnassignedPairs[indexOfBestPair].iCompact.CopyTo(best, 0);
                        #region LOG
                        if (doLog(E_LOG.E_CORE_BEST_PAIR________________))
                        {
                            m_szTmp = "";
                            for (int i = 0; i < iDepth; i++)
                                m_szTmp += ", " + best[i];
                            Log("Best pair is " + m_szTmp.Substring(2) + " at " + indexOfBestPair + " with weight " + bestWeight);
                        }
                        #endregion


                        // aPosArray contains the selected pair
                        aPosArray = new int[iDepth];
                        for (int i = 0; i < iDepth; i++)
                            aPosArray[i] = cUserTestset.getValueOfParamater(best[i]);

                        #region LOG
                        if (doLog(E_LOG.E_CORE_BEST_PAIR_POSITION_______))
                        {
                            m_szTmp = "";
                            for (int i = 0; i < iDepth; i++)
                                m_szTmp += ", " + aPosArray[i];
                            Log("The best pair belongs at positions [" + m_szTmp.Substring(2) + "]");
                        }
                        #endregion

                        // generate a random order to fill parameter positions
                        ordering_new = genOrdering(aPosArray);

                        testSet = FillArray(cUserTestset.nrParameters, -1); //  make an empty candidate testSet

                        // place two parameter values from best unused pair into candidate testSet
                        for (int i = 0; i < best.Length; i++)
                            testSet[aPosArray[i]] = best[i];

                        #region LOG
                        if (doLog(E_LOG.E_CORE_PLACED_PARAMS____________))
                        {
                            string szTmpPosArray = "";
                            m_szTmp = "";
                            for (int i = 0; i < best.Length; i++)
                            {
                                m_szTmp += ", " + best[i];
                                szTmpPosArray += ", " + aPosArray[i];
                            }
                            Log("Placed params [" + m_szTmp.Substring(2) + "] at [" + szTmpPosArray.Substring(2) + "]");
                        }
                        #endregion

                        szTmp = "ordering_new: ";
                        m_szTmp = "";
                        for (int i = 0; i < ordering_new.Length; ++i)
                            m_szTmp += ordering_new[i].ToString() + " ";
                        szTmp += m_szTmp;
                        szTmp += "  testSet: ";
                        m_szTmp = "";
                        for (int i = 0; i < testSet.Length; ++i)
                            m_szTmp += testSet[i].ToString() + " ";
                        szTmp += m_szTmp;
                        szTmp += "  aPosArray: ";
                        m_szTmp = "";
                        for (int i = 0; i < aPosArray.Length; ++i)
                            m_szTmp += aPosArray[i].ToString() + " ";
                        szTmp += m_szTmp;
                    }
                    else
                    {
                        testSet = iRegResult;
                        List<int> lstOrdering = new List<int>();
                        foreach (int iOrder in testSet)
                            if (iOrder >= 0)
                            {
                                int iParPos = cUserTestset.getValueOfParamater(iOrder);
                                lstOrdering.Add(iParPos);
                            }

                        ordering_new = genOrdering(lstOrdering.ToArray());
                        iDepth = lstOrdering.Count;

                        szTmp = "ordering_new: ";
                        m_szTmp = "";
                        for (int i = 0; i < ordering_new.Length; ++i)
                            m_szTmp += ordering_new[i].ToString() + " ";
                        szTmp += m_szTmp;
                        szTmp += "  testSet: ";
                        m_szTmp = "";
                        for (int i = 0; i < testSet.Length; ++i)
                            m_szTmp += testSet[i].ToString() + " ";
                        szTmp += m_szTmp;
                    }
                    // for remaining parameter positions in candidate testSet, try each possible legal value, 
                    // picking the one which captures the most unused pairs . . .
                    ///////////////// iDepth must be changed by nr_pre_placed
                    for (int i = iDepth; i < cUserTestset.nrParameters; ++i) // start at 2 because first two parameter have been placed
                    {
                        // get group number to be handled
                        int currPos = ordering_new[i];
                        // get all values belonging to this group
                        int[] possibleValues = cUserTestset.getLegalValues(currPos);

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
                                if (m_unusedPairsSearch.get(candidatePair) == 1)
                                {
                                    #region LOG
                                    if (doLog(E_LOG.E_CORE_EXAMINE_PAIRS____________))
                                        Log("Found " + candidatePair[0] + "," + candidatePair[1] + " in unusedPairs");
                                    #endregion
                                    ++currentCount_new;
                                    if (m_unusedPairsSearch.get(candidatePair[0], candidatePair[1]) == 0 ||
                                           m_unusedPairsSearch.get(candidatePair[1], candidatePair[0]) == 0)
                                        clsGlobal._stop();
                                }
                                else
                                {
                                    #region LOG
                                    if (doLog(E_LOG.E_CORE_EXAMINE_PAIRS____________))
                                        Log("Did NOT find " + candidatePair[0] + "," + candidatePair[1] + " in unusedPairs");
                                    #endregion
                                    if (m_unusedPairsSearch.get(candidatePair[0], candidatePair[1]) == 1 ||
                                            m_unusedPairsSearch.get(candidatePair[1], candidatePair[0]) == 1)
                                        clsGlobal._stop();
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
                        szTmp += " --> testSet: ";
                        m_szTmp = "";
                        for (int f = 0; f < testSet.Length; ++f)
                            m_szTmp += testSet[f].ToString() + " ";
                        szTmp += m_szTmp;
                    } // i -- each testSet position 
                    szTmp += "-->  final testSet: ";
                    m_szTmp = "";
                    for (int i = 0; i < testSet.Length; ++i)
                        m_szTmp += testSet[i].ToString() + " ";
                    szTmp += m_szTmp;
                    Log(szTmp);

                    #region LOG
                    if (doLog(E_LOG.E_CORE_ADD_CANDIDATE____________))
                    {
                        //=========
                        Log("\n============================");
                        Log("Adding candidate testSet to candidateSets array: ");
                        m_szTmp = "";
                        for (int i = 0; i < cUserTestset.nrParameters; ++i)
                            m_szTmp += testSet[i].ToString() + " ";
                        Log(m_szTmp);
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
                        m_szTmp = "";
                        int[] curr = candidateSets[i];
                        m_szTmp += i.ToString() + ": ";
                        for (int j = 0; j < curr.Length; ++j)
                        {
                            m_szTmp += curr[j].ToString() + " ";
                        }
                        Log(m_szTmp + " -- captures " + NumberPairsCaptured(curr, m_unusedPairsSearch));
                    }
                }
                #endregion

                Console.WriteLine("--------------------------------------");
                return candidateSets;
            }
            public int update(int[] bestTestSet)
            {
                int iRet = 0;
                // now perform all updates
                Message3("Updating unusedPairs, unusedCounts and unusedPairsSearch");

                clsResult cResult = new clsResult(bestTestSet, cUserTestset);
                m_testSets.Add(cResult); // Add the best candidate to the main testSets List

                // try out all possible pairs in
                if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                    Log("bestTestSet:" + clsPairwiseCore.arrayToString(bestTestSet));
                for (int p = cUserTestset.UnassignedPairs.Count - 1; p >= 0; --p)
                {
                    clsPair cPair = cUserTestset.UnassignedPairs[p];
                    if (cPair.IN(bestTestSet))
                    {
                        // cPair belongs to this testset
                        cResult.lstMatchingPairs.Add(cPair);

                        if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                            Log("Decrementing the unused counts for " + clsPairwiseCore.arrayToString(cPair.iCompact));
                        foreach (int iV in cPair.iCompact)
                        {
                            --cUserTestset.getUserValue(iV).UnusedCount;
                        }

                        if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                            Log("Setting unusedPairsSearch at " + clsPairwiseCore.arrayToString(cPair.iCompact) + " to 0");
                        // remove this piar from the m_unusedPairsSearch array
                        m_unusedPairsSearch.setSquared(0, cPair.iCompact);

                        if (doLog(E_LOG.E_CORE_UPDATE_STEPS_____________))
                            Log("Removing " + cPair.ToString() + " from unusedPairs List");

                        cUserTestset.Assign(p);
                        iRet++;
                    }
                } // i

                if (cRegTestSet != null)
                {
                    List<clsRegressionLine> lstRegressionLines = cRegTestSet.Assign(bestTestSet);
                    cResult.lstRegressionLines.AddRange(lstRegressionLines);
                }
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

            #region Logging facilities
            bool doLog(E_LOG eLog)
            {
                ulong lLog = (ulong)eLog;
                if ((eLOG & lLog) == lLog) return true;
                return false;
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
            public void LogNew()
            {
                Log("allPairsDisplay array:");
                int[] iSizes = new int[m_iDepth];
                string[,] szDisplayValues = new string[cUserTestset.NumberPairs, m_iDepth];

                try
                {

                    // first determine sizes
                    for (int i = 0; i < cUserTestset.NumberPairs; ++i)
                    {
                        int[] iTmp = cUserTestset.UnassignedPairs[i].Pair;
                        string szLogTmp = i.ToString();
                        szLogTmp += "-" + m_unusedPairsSearch.get(iTmp).ToString();
                        szLogTmp = Stringsupport.addSpacesToString(szLogTmp, 7);
                        for (int iCnt = 0; iCnt < m_iDepth; iCnt++)
                        {
                            //szLogTmp += "(" + Stringsupport.addSpacesToString(szDisplayValues[i, iCnt], iSizes[iCnt]) + ")  ";
                        }
                        Log(szLogTmp);
                    }
                }
                catch (Exception ex)
                {
                }

            }

            #endregion

        }
    }
}
