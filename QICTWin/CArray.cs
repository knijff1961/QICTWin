using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    public class CArray<T>
    {
        protected T[,] oArrayBody;

        public CArray(int iRows, int iCols)
        {
            oArrayBody = new T[iRows, iCols];
        }
        public CArray(T[,] iVals)
        {
            oArrayBody = new T[iVals.GetLength(0), iVals.GetLength(1)];
            for (int iRowCnt = 0; iRowCnt < iVals.GetLength(0); iRowCnt++)
                for (int iColCnt = 0; iColCnt < iVals.GetLength(1); iColCnt++)
                {
                    oArrayBody[iRowCnt, iColCnt] = iVals[iRowCnt, iColCnt];
                }
        }
        public CArray(CArray<T> a1)
        {
            oArrayBody = new T[a1.Rows, a1.Cols];
            for (int iRowCnt = 0; iRowCnt < a1.Rows; iRowCnt++)
                for (int iColCnt = 0; iColCnt < a1.Cols; iColCnt++)
                {
                    oArrayBody[iRowCnt, iColCnt] = a1[iRowCnt, iColCnt];
                }
        }

        public T this[int iRow, int iCol]
        {
            get
            {
                return oArrayBody[iRow, iCol];
                //return _rst[szName];
            }
            set
            {
                oArrayBody[iRow, iCol] = value;
                //return _rst[szName];
            }
        }
        public int Rows
        {
            get
            {
                return oArrayBody.GetLength(0);
            }
        }
        public int Cols
        {
            get
            {
                return oArrayBody.GetLength(1);
            }
        }
        #region operators
        public static CArray<T> operator +(CArray<T> a1, CArray<T> a2)
        {
            if (a1.Rows == a2.Rows && a1.Cols == a2.Cols)
            {
                CArray<T> a3 = new CArray<T>(a1.Rows, a1.Cols);
                for (int iRowCnt = 0; iRowCnt < a1.Rows; iRowCnt++)
                    for (int iColCnt = 0; iColCnt < a2.Cols; iColCnt++)
                    {
                        if (a3[iRowCnt, iColCnt] is int)
                        {
                            object oa1 = a1[iRowCnt, iColCnt];
                            object oa2 = a2[iRowCnt, iColCnt];
                            object oa3 = (int)oa1 + (int)oa2;
                            a3[iRowCnt, iColCnt] = (T)oa3;
                        }
                        else if (a3[iRowCnt, iColCnt] is string)
                        {
                            object oa1 = a1[iRowCnt, iColCnt];
                            object oa2 = a2[iRowCnt, iColCnt];
                            object oa3 = (string)oa1 + (string)oa2;
                            a3[iRowCnt, iColCnt] = (T)oa3;
                        }
                        else
                            throw new Exception("Cannot add arrays with different dimensions");
                    }

                return a3;
            }
            else
                throw new Exception("Cannot add arrays with different dimensions");
        }

        #endregion
    }
}
