using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    public class MultidimensionalSquareArray
    {
        public int[] iOneDimenionalArray;
        public int iBounds;
        public int iSize;

        public MultidimensionalSquareArray()
        {
        }
        public MultidimensionalSquareArray(int iBounds, int iSize)
        {
            setBoundaries(iBounds, iSize);
        }
        public void setBoundaries(int iBounds, int iSize)
        {
            this.iBounds = iBounds;
            this.iSize = iSize;
            iOneDimenionalArray = makeMultidimensionalArray(iBounds, iSize);
        }
        public void setBoundaries(int iBounds, int iSize, object valArray)
        {
            this.iBounds = iBounds;
            this.iSize = iSize;
            iOneDimenionalArray = makeMultidimensionalArray(iBounds, iSize);
        }
        public MultidimensionalSquareArray(int[][] iVals)
        {
            //this.iBounds = iBounds;
            //this.iSize = iSize;
            //iOneDimenionalArray = makeMultidimensionalArray(iBounds, iSize);
        }
        public MultidimensionalSquareArray(int[,] iVals)
        {
            if (iVals.GetLength(0) != iVals.GetLength(1))
                throw new Exception("dimensions of square array are not equal");
            this.iBounds = 2;
            this.iSize = iVals.GetLength(0);

            iOneDimenionalArray = makeMultidimensionalArray(iBounds, iSize);
            int iArrayPos = 0;
            for(int iRowCnt=0;iRowCnt<iSize;iRowCnt++)
                for (int iColCnt = 0; iColCnt < iSize; iColCnt++)
                {
                    iOneDimenionalArray[iArrayPos++] = iVals[iRowCnt, iColCnt];
                }
               
        }

        public int get(params int[] iIndexes)
        {
            return iOneDimenionalArray[getSquareIndex(iSize, iIndexes)];
        }
        public void set(int iVal, params int[] iIndexes)
        {
            iOneDimenionalArray[getSquareIndex(iSize, iIndexes)] = iVal;
        }

        public static MultidimensionalSquareArray operator +(MultidimensionalSquareArray a1, MultidimensionalSquareArray a2)
        {
            if (a1.iSize == a2.iSize && a1.iBounds == a2.iBounds)
            {
                MultidimensionalSquareArray a3 = new MultidimensionalSquareArray(a1.iBounds, a1.iSize);
                for (int iCnt = 0; iCnt < a3.iOneDimenionalArray.Length; iCnt++)
                    a3.iOneDimenionalArray[iCnt] = a1.iOneDimenionalArray[iCnt] + a2.iOneDimenionalArray[iCnt];
                return a3;
            }
             else
                throw new Exception("Cannot add arrays with different dimensions");
        }
        public static MultidimensionalSquareArray operator -(MultidimensionalSquareArray a1, MultidimensionalSquareArray a2)
        {
            if (a1.iSize == a2.iSize && a1.iBounds == a2.iBounds)
            {
                MultidimensionalSquareArray a3 = new MultidimensionalSquareArray(a1.iBounds, a1.iSize);
                for (int iCnt = 0; iCnt < a3.iOneDimenionalArray.Length; iCnt++)
                    a3.iOneDimenionalArray[iCnt] = a1.iOneDimenionalArray[iCnt] - a2.iOneDimenionalArray[iCnt];
                return a3;
            }
            else
                throw new Exception("Cannot add arrays with different dimensions");
        }
        public static MultidimensionalSquareArray operator *(MultidimensionalSquareArray a1, MultidimensionalSquareArray a2)
        {
            if (a1.iSize == a2.iSize && a1.iBounds == a2.iBounds)
            {
                MultidimensionalSquareArray a3 = new MultidimensionalSquareArray(a1.iBounds, a1.iSize);
                for (int iCnt = 0; iCnt < a3.iOneDimenionalArray.Length; iCnt++)
                    a3.iOneDimenionalArray[iCnt] = a1.iOneDimenionalArray[iCnt] * a2.iOneDimenionalArray[iCnt];
                return a3;
            }
            else
                throw new Exception("Cannot add arrays with different dimensions");
        }
        public static MultidimensionalSquareArray operator /(MultidimensionalSquareArray a1, MultidimensionalSquareArray a2)
        {
            if (a1.iSize == a2.iSize && a1.iBounds == a2.iBounds)
            {
                MultidimensionalSquareArray a3 = new MultidimensionalSquareArray(a1.iBounds, a1.iSize);
                for (int iCnt = 0; iCnt < a3.iOneDimenionalArray.Length; iCnt++)
                    a3.iOneDimenionalArray[iCnt] = a1.iOneDimenionalArray[iCnt] / a2.iOneDimenionalArray[iCnt];
                return a3;
            }
            else
                throw new Exception("Cannot add arrays with different dimensions");
        }

        public void swapRow(int iFristRow, int iSecondRow)
        {
            iFristRow *= iSize;
            iSecondRow *= iSize;

            for (int iCnt = 0; iCnt < iSize; iCnt++)
            {
                int iTmp = iOneDimenionalArray[iFristRow];
                iOneDimenionalArray[iFristRow++] = iOneDimenionalArray[iSecondRow];
                iOneDimenionalArray[iSecondRow++] = iTmp;
            }
        }
        public void swapCol(int iFristCol, int iSecondCol)
        {
            //iFristCol *= iSize;
            //iSecondCol *= iSize;

            for (int iCnt = 0; iCnt < iSize; iCnt++)
            {
                int iTmp = iOneDimenionalArray[iFristCol];
                iOneDimenionalArray[iFristCol] = iOneDimenionalArray[iSecondCol];
                iOneDimenionalArray[iSecondCol] = iTmp;
                iFristCol += iSize;
                iSecondCol += iSize;
            }
        }

        public void getArrayIndex(int iRow, int iCol)
        {
        }
        /// <summary>
        /// Make a mapping for a multidimensional array to a one dimenional array.
        /// The one dimensional array can then be used as a multiple dimensional array usinge the underlying routines
        /// </summary>
        /// <param name="iBounds"></param>
        /// <param name="iSize"></param>
        /// <returns></returns>
        public static int[] makeMultidimensionalArray(int iBounds, int iSize)
        {
            int d = (int)Math.Pow(iSize, iBounds);
            int[] iRet = new int[d];
            return iRet;
        }

        /// <summary>
        ///  returns the index of an item in an "square" array (e.g. multidimensional array with same sizes)
        ///  The returned index is a number to be found in a one dimensional array
        /// </summary>
        /// <param name="iBounds">The square size of the array</param>
        /// <param name="iBoundaries">Indexes within the "multidimensional" array; the length is used to determine the number of dimensions</param>
        /// <returns>The index mapped to the single dimensional array</returns>
        public static int getSquareIndex(int iBounds, params int[] iBoundaries)
        {
            int iRet = 0;

            for (int j = 0; j < iBoundaries.Length; j++)
            {
                iRet *= iBounds;
                iRet += iBoundaries[j];
            }

            return iRet;
        }

         public static void Test()
        {
            int[,] v1 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            int[,] v2 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            MultidimensionalSquareArray a1 = new MultidimensionalSquareArray(v1);
            MultidimensionalSquareArray a2 = new MultidimensionalSquareArray(v2);
            MultidimensionalSquareArray a3 = a1 + a2;
            a3 += a2;
            a1.swapCol(0, 1);
            a1.swapRow(0, 1);
        }
    }
}
