using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    public class CVector
    {
        double[] cVec;
        public CVector()
        {
        }
        public CVector(double[] dVec)
        {
            cVec = new double[dVec.Length];
            for (int iCnt = 0; iCnt < dVec.Length; iCnt++)
                cVec[iCnt] = dVec[iCnt];
        }
        public CVector(CVector dVec)
        {
            cVec = new double[dVec.R];
            for (int iCnt = 0; iCnt < cVec.Length; iCnt++)
                cVec[iCnt] = dVec[iCnt];
        }
        public double this[int iRow]
        {
            get
            {
                return cVec[iRow];
            }
            set
            {
                cVec[iRow] = value;
            }
        }

        public static CVector operator +(CVector v1, CVector v2)
        {
            if (v1.R != v2.R)
                throw new Exception("Vectors have different size");

            double[] cVec = new double[v1.R];
            for (int iCnt = 0; iCnt < v1.R; iCnt++)
                cVec[iCnt]= v1[iCnt] + v2[iCnt];
            return new CVector(cVec);
        }

        public int R
        {
            get
            {
                return cVec.Length;
            }
        }

        public double Length
        {
            get
            {
                double d = 0;
                for (int iCnt = 0; iCnt < cVec.Length; iCnt++)
                    d += cVec[iCnt] * cVec[iCnt];
                return Math.Sqrt(d);
            }
        }

        public double distance(CVector v)
        {
            if (this.R != v.R)
                throw new Exception("Vectors have different size");
            double d = 0;
            for (int iCnt = 0; iCnt < cVec.Length; iCnt++)
                d += (cVec[iCnt] - v[iCnt]) * (cVec[iCnt] - v[iCnt]);


            return Math.Sqrt(d);
        }
        public double inproduct(CVector v)
        {
            if (this.R != v.R)
                throw new Exception("Vectors have different size");
            double d = 0;
            for (int iCnt = 0; iCnt < cVec.Length; iCnt++)
                d += cVec[iCnt] * v[iCnt];

            return Math.Sqrt(d);
        }

        public static CVector operator *(CVector v1, double d)
        {
            double[] cVec = new double[v1.R];
            for (int iCnt = 0; iCnt < v1.R; iCnt++)
                cVec[iCnt] = d * v1[iCnt];
            return new CVector(cVec);
        }
        public static CVector operator *(double d, CVector v1)
        {
            double[] cVec = new double[v1.R];
            for (int iCnt = 0; iCnt < v1.R; iCnt++)
                cVec[iCnt] = d * v1[iCnt];
            return new CVector(cVec);
        }

        public static void testc()
        {
            CVector v1 = new CVector(new double[] { 1, 2, 3 });
            CVector v2 = new CVector(new double[] { 4, 5, 6 });

            CVector v3 = v1 + v2;
            v1 += v2;
            v1 *= 3;
            v1 = 3 * v1;

        }
    }
}
