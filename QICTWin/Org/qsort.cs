// <copyright file="qsort.cs" company="Fouroaks">
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
using System.Reflection;

namespace Test2Lib
{
    public static class qsort<T>
    {
        public delegate int dlgCompare(T i1, T i2);
        public delegate int dlgParCompare(T i1, T i2, params object[] args);
        public delegate int dlgTypedCompare(T i1, T i2, int iCompare);
        public delegate int dlgPartCompare(object i1, T i2);
        public delegate int dlgTypedPartCompare(object i1, T i2, int iCompare);

        internal static System.Reflection.MethodInfo[] getMethods(Type clsClass, System.Reflection.BindingFlags bflags)
        {
            // get all public static methods of MyClass type
            System.Reflection.MethodInfo[] methodInfos = clsClass.GetMethods(bflags);

            // sort methods by name
            Array.Sort(methodInfos,
                    delegate(System.Reflection.MethodInfo methodInfo1, System.Reflection.MethodInfo methodInfo2)
                    {
                        return methodInfo1.Name.CompareTo(methodInfo2.Name);
                    });
            return methodInfos;
        }
        internal static List<MethodInfo> getMethods(Type clsClass, string szName, System.Reflection.BindingFlags bflags)
        {
            // get all public static methods of MyClass type
            System.Reflection.MethodInfo[] methodInfos = clsClass.GetMethods(bflags);

            List<MethodInfo> lstRet = new List<MethodInfo>();
            foreach (MethodInfo mi in methodInfos)
                if (mi.Name.Equals(szName))
                    lstRet.Add(mi);
            return lstRet;
        }

        public static MethodInfo getCompareMethodFromClass(object searchNum, string szComparer)
        {
            List<MethodInfo> lst = getMethods(searchNum.GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                            | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (lst.Count != 1) throw new Exception("compare method not unique");
            return lst[0];

        }

        class clsIComparer : IComparer<T>
        {
            int iScanType;
            int iCompareType;
            dlgCompare comparer;
            dlgParCompare parComparer;
            dlgTypedCompare typedComparer;
            dlgTypedPartCompare typedPartComparer;
            MethodInfo method;
            T methodObject;
            object[] args = null;
            public clsIComparer(int iType)
            {
                iScanType = iType;
            }
            public clsIComparer(int iType, dlgCompare comparer)
            {
                iScanType = iType;
                this.comparer = comparer;
            }
            public clsIComparer(int iType, dlgParCompare parComparer, params object[] args)
            {
                this.args = args;
                iScanType = iType;
                this.parComparer = parComparer;
            }
            public clsIComparer(int iType, int iCompareType, dlgTypedCompare comparer)
            {
                iScanType = iType;
                this.iCompareType = iCompareType;
                this.typedComparer = comparer;
            }
            public clsIComparer(int iType, T methodObject, MethodInfo method)
            {
                iScanType = iType;
                this.method = method;
                this.methodObject = methodObject;
            }
            public clsIComparer(int iType, int iCompareType, T methodObject, MethodInfo method)
            {
                iScanType = iType;
                this.method = method;
                this.methodObject = methodObject;
                this.iCompareType = iCompareType;
            }
            public clsIComparer(int iType, int iCompareType, dlgTypedPartCompare comparer)
            {
                iScanType = iType;
                this.typedPartComparer = comparer;
                this.iCompareType = iCompareType;
            }
            public int Compare(T obj1, T obj2)
            {
                if (iScanType == 1) return comparer(obj1, obj2);
                if (iScanType == 6) return parComparer(obj1, obj2, this.args);

                if (iScanType == 2) return typedComparer(obj1, obj2, iCompareType);
                if (iScanType == 3)
                {
                    object[] args = { obj1, obj2 };
                    return (int)method.Invoke(methodObject, args);
                }
                if (iScanType == 4)
                {
                    object[] args = { iCompareType, obj1, obj2 };
                    return (int)method.Invoke(methodObject, args);
                }
                if (iScanType == 5) return typedPartComparer(obj1, obj2, iCompareType);
                return 0;
            }

        }

        public static List<T> newSort(List<T> numArray, dlgCompare comparer)
        {
            List<T> cloneList = new List<T>(numArray);
            clsIComparer cIComparer = new qsort<T>.clsIComparer(1, comparer);
            cloneList.Sort(cIComparer);
            return cloneList;
        }
        public static List<T> newSort(List<T> numArray, int iCompareType, dlgTypedCompare comparer)
        {
            List<T> cloneList = new List<T>(numArray);
            clsIComparer cIComparer = new qsort<T>.clsIComparer(2, iCompareType, comparer);
            cloneList.Sort(cIComparer);
            return cloneList;
        }
        public static List<T> newSort(List<T> numArray, string szComparer)
        {
            List<T> cloneList = new List<T>(numArray);
            if (cloneList.Count != 0)
            {
                List<MethodInfo> lst = getMethods(numArray[0].GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                                | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (lst.Count != 1) throw new Exception("compare method not unique");
                MethodInfo method = lst[0];

                clsIComparer cIComparer = new qsort<T>.clsIComparer(3, cloneList[0], method);
                cloneList.Sort(cIComparer);
            }
            return cloneList;
        }
        public static List<T> newSort(List<T> numArray, int iCompareType, string szComparer)
        {
            List<T> cloneList = new List<T>(numArray);
            if (cloneList.Count != 0)
            {
                List<MethodInfo> lst = getMethods(numArray[0].GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                                | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (lst.Count != 1) throw new Exception("compare method not unique");
                MethodInfo method = lst[0];

                clsIComparer cIComparer = new qsort<T>.clsIComparer(4, iCompareType, cloneList[0], method);
                cloneList.Sort(cIComparer);
            }
            return cloneList;
        }
        public static List<T> newSort(List<T> numArray, MethodInfo method)
        {
            List<T> cloneList = new List<T>(numArray);
            if (cloneList.Count != 0)
            {
                clsIComparer cIComparer = new qsort<T>.clsIComparer(3, cloneList[0], method);
                cloneList.Sort(cIComparer);
            }
            return cloneList;
        }
        public static List<T> newSort(List<T> numArray, int iCompareType, MethodInfo method)
        {
            List<T> cloneList = new List<T>(numArray);
            if (cloneList.Count != 0)
            {
                clsIComparer cIComparer = new qsort<T>.clsIComparer(4, iCompareType, cloneList[0], method);
                cloneList.Sort(cIComparer);
            }
            return cloneList;
        }
        public static List<T> newSort(List<T> numArray, int iCompareType, dlgTypedPartCompare comparer)
        {
            List<T> cloneList = new List<T>(numArray);
            if (cloneList.Count != 0)
            {
                clsIComparer cIComparer = new qsort<T>.clsIComparer(5, iCompareType, comparer);
                cloneList.Sort(cIComparer);
            }
            return cloneList;
        }

        public static void Sort(List<T> numArray, dlgCompare comparer)
        {
            clsIComparer cIComparer = new qsort<T>.clsIComparer(1, comparer);
            numArray.Sort(cIComparer);
        }
        public static void Sort(List<T> numArray, dlgParCompare comparer, params object[] args)
        {
            clsIComparer cIComparer = new qsort<T>.clsIComparer(6, comparer, args);
            numArray.Sort(cIComparer);
        }
        public static void Sort(List<T> numArray, int iCompareType, dlgTypedCompare comparer, params object[] args)
        {
            clsIComparer cIComparer = new qsort<T>.clsIComparer(2, iCompareType, comparer);
            numArray.Sort(cIComparer);
        }
        public static void Sort(List<T> numArray, string szComparer)
        {
            if (numArray.Count != 0)
            {
                List<MethodInfo> lst = getMethods(numArray[0].GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                                | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (lst.Count != 1) throw new Exception("compare method not unique");
                MethodInfo method = lst[0];

                clsIComparer cIComparer = new qsort<T>.clsIComparer(3, numArray[0], method);
                numArray.Sort(cIComparer);
            }
        }
        public static void Sort(List<T> numArray, int iCompareType, string szComparer)
        {
            if (numArray.Count != 0)
            {
                List<MethodInfo> lst = getMethods(numArray[0].GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                                | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (lst.Count != 1) throw new Exception("compare method not unique");
                MethodInfo method = lst[0];

                clsIComparer cIComparer = new qsort<T>.clsIComparer(4, iCompareType, numArray[0], method);
                numArray.Sort(cIComparer);
            }
        }
        public static void Sort(List<T> numArray, MethodInfo method)
        {
            if (numArray.Count != 0)
            {
                clsIComparer cIComparer = new qsort<T>.clsIComparer(3, numArray[0], method);
                numArray.Sort(cIComparer);
            }
        }
        public static void Sort(List<T> numArray, int iCompareType, MethodInfo method)
        {
            if (numArray.Count != 0)
            {
                clsIComparer cIComparer = new qsort<T>.clsIComparer(4, iCompareType, numArray[0], method);
                numArray.Sort(cIComparer);
            }
        }
        public static void Sort(List<T> numArray, int iCompareType, dlgTypedPartCompare comparer)
        {
            if (numArray.Count != 0)
            {
                clsIComparer cIComparer = new qsort<T>.clsIComparer(5, iCompareType, comparer);
                numArray.Sort(cIComparer);
            }
        }

        public static List<T> SortUnique(List<T> numArray, dlgCompare comparer)
        {
            List<T> removedList = new List<T>();
            Sort(numArray, comparer);

            // remove duplicates
            for (int iCnt = numArray.Count - 1; iCnt > 0; iCnt--)
            {
                if (comparer(numArray[iCnt], numArray[iCnt - 1]) == 0)
                {
                    removedList.Add(numArray[iCnt]);
                    numArray.RemoveAt(iCnt);
                }
            }
            return removedList;
        }
        public static List<T> SortUnique(List<T> numArray, dlgParCompare comparer, params object[] args)
        {
            List<T> removedList = new List<T>();
            Sort(numArray, comparer, args);

            // remove duplicates
            for (int iCnt = numArray.Count - 1; iCnt > 0; iCnt--)
            {
                if (comparer(numArray[iCnt], numArray[iCnt - 1], args) == 0)
                {
                    removedList.Add(numArray[iCnt]);
                    numArray.RemoveAt(iCnt);
                }
            }
            return removedList;
        }


        public static int binaryArraySearch(T[] numArray, T searchNum, dlgCompare comparer)
        {
            int sizeNum = numArray.Length;

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                int iComp = comparer(searchNum, numArray[midNum]);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return 0;
        }
        public static int binaryArraySearch(T[] numArray, object searchNum, dlgPartCompare comparer)
        {
            int sizeNum = numArray.Length;

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                int iComp = comparer(searchNum, numArray[midNum]);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return 0;
        }

        public static int binarysearch(IList<T> numArray, T searchNum, dlgCompare comparer)
        {
            MethodInfo method = comparer.Method;

            int sizeNum = numArray.Count;

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;
            int iComp = 0;
            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                iComp = comparer(searchNum, numArray[midNum]);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found; return (minus) place where it should be placed BEFORE
            if (iComp == 1)
                return -highNum - 1;
            return -highNum;
        }
        public static int binarysearch(IList<T> numArray, T searchNum, int iCompareType, dlgTypedCompare comparer)
        {
            int sizeNum = numArray.Count;

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;
            int iComp = 0;
            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                iComp = comparer(searchNum, numArray[midNum], iCompareType);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return -highNum;
        }
        public static int binarysearch(IList<T> numArray, object searchNum, string szComparer)
        {
            List<MethodInfo> lst = getMethods(searchNum.GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                                        | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (lst.Count != 1) throw new Exception("compare method not unique");
            MethodInfo method = lst[0];

            //return binarysearchComparable(numArray, searchNum, method);

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = numArray.Count - 1;
            int iComp = 0;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                // call the compare method
                object[] paramArray = { numArray[midNum] };
                iComp = (int)method.Invoke(searchNum, paramArray);
                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return -highNum;
        }
        public static int binarysearch(IList<T> numArray, object searchNum, int iCompareType, string szComparer)
        {
            List<MethodInfo> lst = getMethods(searchNum.GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                                        | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (lst.Count != 1) throw new Exception("compare method not unique");
            MethodInfo method = lst[0];

            //return binarysearchComparable(numArray, searchNum, method);

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = numArray.Count - 1;
            int iComp = 0;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                // call the compare method
                object[] paramArray = { numArray[midNum], iCompareType };
                iComp = (int)method.Invoke(searchNum, paramArray);
                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return -highNum;
        }
        public static int binarysearch(IList<T> numArray, object searchNum, MethodInfo method)
        {
            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = numArray.Count - 1;
            int iComp = 0;
            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                // call the compare method
                object[] paramArray = { numArray[midNum] };
                iComp = (int)method.Invoke(searchNum, paramArray);
                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return -highNum;
        }
        public static int binarysearch(IList<T> numArray, object searchNum, int iCompareType, MethodInfo method)
        {
            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = numArray.Count - 1;
            int iComp = 0;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                // call the compare method
                object[] paramArray = { numArray[midNum], iCompareType };
                iComp = (int)method.Invoke(searchNum, paramArray);
                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return -highNum;
        }
        public static int binarysearch(IList<T> numArray, object searchNum, int iCompareType, dlgTypedPartCompare comparer)
        {
            int sizeNum = numArray.Count;

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;
            int iComp = 0;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                iComp = comparer(searchNum, numArray[midNum], iCompareType);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return -highNum;
        }

        public static int binaryInsert(IList<T> lst, T newObject, dlgCompare comparer)
        {
            MethodInfo method = comparer.Method;

            int sizeNum = lst.Count;
            if (sizeNum == 0)
            {
                lst.Add(newObject);
                return 0;
            }
            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;
            int iComp = 0;
            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                iComp = comparer(newObject, lst[midNum]);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    lst.Insert(midNum, newObject);
                    return midNum;
                }
            }
            //no match found; return (minus) place where it should be placed at lowNum
            lst.Insert(lowNum, newObject);
            return lowNum;
        }
        public static int binaryInsert(IList<T> numArray, T searchNum, int iCompareType, dlgTypedCompare comparer)
        {
            int sizeNum = numArray.Count;
            if (sizeNum == 0)
            {
                numArray.Add(searchNum);
                return 0;
            }

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;
            int iComp = 0;
            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                iComp = comparer(searchNum, numArray[midNum], iCompareType);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found; return (minus) place where it should be placed at lowNum
            numArray.Insert(lowNum, searchNum);
            return lowNum;
        }

        public static int binarySearchRange(List<T> lst, T item, dlgCompare comparer, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;
            //int i = lst.BinarySearch(item, comparer);
            int i = qsort<T>.binarysearch(lst, item, comparer);
            if (i < 0) return 0;

            iFirst = i;
            iLast = i;
            while (iFirst != 0)
            {
                if (comparer(lst[iFirst - 1], item) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                if (comparer(lst[iLast + 1], item) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }
        public static int binarySearchRange(List<T> lst, T item, int iCompareType, dlgTypedCompare comparer, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;
            //int i = lst.BinarySearch(item, comparer);
            int i = qsort<T>.binarysearch(lst, item, iCompareType, comparer);
            if (i < 0) return 0;

            iFirst = i;
            iLast = i;
            while (iFirst != 0)
            {
                if (comparer(lst[iFirst - 1], item, iCompareType) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                if (comparer(lst[iLast + 1], item, iCompareType) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }
        public static int binarySearchRange(List<T> lst, object searchNum, string szComparer, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;
            int i = qsort<T>.binarysearch(lst, searchNum, szComparer);
            if (i < 0) return 0;

            MethodInfo method = getMethods(searchNum.GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                            | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)[0];

            iFirst = i;
            iLast = i;
            while (iFirst != 0)
            {
                object[] paramArray = { lst[iFirst - 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                object[] paramArray = { lst[iFirst + 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }
        public static int binarySearchRange(List<T> lst, object searchNum, int iCompareType, string szComparer, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;
            int i = qsort<T>.binarysearch(lst, searchNum, iCompareType, szComparer);
            if (i < 0) return 0;

            MethodInfo method = getMethods(searchNum.GetType(), szComparer, BindingFlags.Default | BindingFlags.DeclaredOnly | BindingFlags.Instance
                            | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)[0];

            if (i < 0) return 0;

            iFirst = i;
            iLast = i;
            while (iFirst != 0)
            {
                object[] paramArray = { iCompareType, lst[iFirst - 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                object[] paramArray = { iCompareType, lst[iFirst + 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }
        public static int binarySearchRange(IList<T> lst, object searchNum, MethodInfo method, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;
            int i = qsort<T>.binarysearch(lst, searchNum, method);
            if (i < 0) return 0;

            if (i < 0) return 0;

            iFirst = i;
            iLast = i;
            while (iFirst != 0)
            {
                object[] paramArray = { lst[iFirst - 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                object[] paramArray = { lst[iFirst + 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }
        public static int binarySearchRange(IList<T> lst, object searchNum, int iCompareType, MethodInfo method, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;
            int i = qsort<T>.binarysearch(lst, searchNum, iCompareType, method);
            if (i < 0) return 0;

            if (i < 0) return 0;

            iFirst = i;
            iLast = i;
            while (iFirst != 0)
            {
                object[] paramArray = { iCompareType, lst[iFirst - 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                object[] paramArray = { iCompareType, lst[iFirst + 1] };
                if ((int)method.Invoke(searchNum, paramArray) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }
        public static int binarySearchRange(List<T> lst, object searchNum, int iCompareType, dlgTypedPartCompare comparer, out int iFirst, out int iLast)
        {
            iFirst = -1;
            iLast = -1;

            int i = qsort<T>.binarysearch(lst, searchNum, iCompareType, comparer);
            if (i < 0) return 0;

            iFirst = i;
            iLast = i;

            while (iFirst != 0)
            {
                if (comparer(searchNum, lst[iFirst - 1], iCompareType) != 0)
                    break;
                iFirst--;
            }
            while (iLast != lst.Count - 1)
            {
                if (comparer(searchNum, lst[iLast + 1], iCompareType) != 0)
                    break;
                iLast++;
            }
            return iLast - iFirst + 1;
        }

        private static List<T> makeList(IList<T> lst, int iFirst, int iLast)
        {
            List<T> lstRet = new List<T>();
            for (int iCnt = iFirst; iCnt <= iLast; iCnt++)
                lstRet.Add(lst[iCnt]);
            return lstRet;
        }
        public static List<T> binarySearchListRange(List<T> lst, T item, dlgCompare comparer)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, item, comparer, out iFirst, out iLast);
            if (i == 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, T item, int iCompareType, dlgTypedCompare comparer)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, item, iCompareType, comparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, object searchNum, string szComparer)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, searchNum, szComparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, object searchNum, int iCompareType, string szComparer)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, searchNum, iCompareType, szComparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(IList<T> lst, object searchNum, MethodInfo method)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, searchNum, method, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(IList<T> lst, object searchNum, int iCompareType, MethodInfo method)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, searchNum, iCompareType, method, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, object searchNum, int iCompareType, dlgTypedPartCompare comparer)
        {
            int iFirst;
            int iLast;
            int i = qsort<T>.binarySearchRange(lst, searchNum, iCompareType, comparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }

        public static List<T> binarySearchListRange(List<T> lst, T item, dlgCompare comparer, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, item, comparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, T item, int iCompareType, dlgTypedCompare comparer, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, item, iCompareType, comparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, object searchNum, string szComparer, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, searchNum, szComparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, object searchNum, int iCompareType, string szComparer, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, searchNum, iCompareType, szComparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(IList<T> lst, object searchNum, MethodInfo method, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, searchNum, method, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(IList<T> lst, object searchNum, int iCompareType, MethodInfo method, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, searchNum, iCompareType, method, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }
        public static List<T> binarySearchListRange(List<T> lst, object searchNum, int iCompareType, dlgTypedPartCompare comparer, out int iFirst, out int iLast)
        {
            int i = qsort<T>.binarySearchRange(lst, searchNum, iCompareType, comparer, out iFirst, out iLast);
            if (i < 0) return new List<T>();
            return makeList(lst, iFirst, iLast);
        }


        /// <summary>
        /// Binary search with a Comparable-like interface
        /// </summary>
        /// <param name="numArray"></param>
        /// <param name="searchNum"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static int binarysearchComparable(IList<T> numArray, object searchNum, MethodInfo method)
        {
            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = numArray.Count - 1;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                // call the compare method
                object[] paramArray = { numArray[midNum] };
                int iComp = (int)method.Invoke(searchNum, paramArray);
                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return 0;
        }
        private static int binarysearchComparer(IList<T> numArray, object searchNum, MethodInfo method)
        {
            //method.
            //if (method.IsStatic)
            //    o = method.Invoke(null, oCall);
            //else
            //    o = method.Invoke(clsClass, oCall);

            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = numArray.Count - 1;

            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                // call the compare method
                object[] paramArray = { numArray[midNum], searchNum };
                int iComp = (int)method.Invoke(searchNum, paramArray);
                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    return midNum;
                }
            }
            //no match found
            return 0;
        }

    }


    //foreach (clsMethodInfo cMethodInfo in lstMethodInfo)
    //       {
    //           int iFirst;
    //           int iLast;

    //           int iFound = QSort<clsMethodXMLMember>.binarySearchRange(cMethodXML.lstMembers, null
    //               , new clsMethodXMLMember.genericSort(1, cMethodInfo), out iFirst, out iLast);
    //           if (iFound != 1)
    //               Console.WriteLine(iFound.ToString()+" "+cMethodInfo.FullName);
    //           if (iFound == 1)
    //               cMethodInfo.cMethodXMLMember = cMethodXML.lstMembers[iFirst];
    //       }
    public class testBin<T>
    {
        public delegate int dlgCompare(T i1, T i2);

        public int binaryInsert(IList<T> lst, T newObject, dlgCompare comparer)
        {
            MethodInfo method = comparer.Method;

            int sizeNum = lst.Count;
            if (sizeNum == 0)
            {
                lst.Add(newObject);
                return 0;
            }
            //user entered a numeric value so we start our search
            int lowNum = 0;
            //get the high number in the array
            int highNum = sizeNum - 1;
            int iComp = 0;
            //loop while the low number is less or equal to the high number
            while (lowNum <= highNum)
            {
                //get the middle point in the array
                int midNum = (lowNum + highNum) / 2;

                iComp = comparer(newObject, lst[midNum]);

                //now start checking the values
                if (iComp < 0)
                {
                    //search value is lower than this index of our array
                    //so set the high number equal to the middle number
                    //minus 1
                    highNum = midNum - 1;
                }
                else if (iComp > 0)
                {
                    //search value is higher than this index of our array
                    //so set the low number to the middle number + 1
                    lowNum = midNum + 1;
                }
                else if (iComp == 0)
                {
                    //we found a match! let the user know the
                    //location of the match
                    lst.Insert(midNum, newObject);
                    return midNum;
                }
            }
            //no match found; return (minus) place where it should be placed BEFORE
            lst.Insert(lowNum, newObject);
            return lowNum;
        }


    }

    public class clsTest
    {
        static Random random = new Random();

        private int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        internal static int compareInt(int i2, int i1)
        {
            if (i2 < i1) return -1;
            if (i2 > i1) return 1;
            return 0;
        }
        public void doTest()
        {
            testBin<int> testBin = new testBin<int>();

            List<int> lst = new List<int>();
            for (int iCnt = 0; iCnt < 10000; iCnt++)
            {
                int i = RandomNumber(0, 50);
                lst.Add(i);
            }
            List<int> lstRet = qsort<int>.SortUnique(lst, compareInt);


            lst = new List<int>();
            for (int iCnt = 0; iCnt < 10000; iCnt++)
            {
                int i = RandomNumber(0, 5000);
                int iRet = testBin.binaryInsert(lst, i, compareInt);
                int iPrev = lst[0];
                for (int iTest = 1; iTest < lst.Count; iTest++)
                {
                    if (iPrev > lst[iTest])
                        throw new Exception();
                    iPrev = lst[iTest];
                }
            }
            //binaryInsert
        }

    }
}
