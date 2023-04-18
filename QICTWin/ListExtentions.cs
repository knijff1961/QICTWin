using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class ListExtentions
{
    public delegate int dlgCompare<T>(T i1, T i2);
    class randomize<T>
    {
        public T tObject;
        public int iVal;

        public randomize(T tObject, int iVal)
        {
            this.tObject = tObject;
            this.iVal = iVal;
        }

        public static int Comparer(randomize<T> tObject1, randomize<T> tObject2)
        {
            if (tObject1.iVal < tObject2.iVal) return -1;
            if (tObject1.iVal > tObject2.iVal) return 1;
            return 0;
        }
    }


     public static List<T> myClone<T>(this List<T> theList)
    {
        List<T> lstRet = new List<T>();
        for (int iCnt = 0; iCnt < theList.Count; iCnt++)
            lstRet.Add(theList[iCnt]);
        return lstRet;
    }
    public static string ToTextString<T>(this List<T> theList)
    {
        return ToTextString(theList, "\r\n");
    }
    public static string ToTextString<T>(this List<T> theList, string szSep)
    {
        string szRet = "";
        for (int iCnt = 0; iCnt < theList.Count; iCnt++)
            szRet += szSep + theList[iCnt];
        return szRet.Substring(szSep.Length);
    }

    public static bool IN<T>(this List<T> theList, T myobject)
    {
        for (int iCnt = 0; iCnt < theList.Count; iCnt++)
            if (theList[iCnt].Equals(myobject))
                return true;
        return false;
    }
    public static bool IN<T>(this List<T> theList, T myobject, dlgCompare<T> dlg)
    {
        for (int iCnt = 0; iCnt < theList.Count; iCnt++)
            if (dlg(theList[iCnt], myobject) == 0)
                return true;
        return false;
    }
    public static int INPOS<T>(this List<T> theList, T myobject)
    {
        for (int iCnt = 0; iCnt < theList.Count; iCnt++)
            if (theList[iCnt].Equals(myobject))
                return iCnt;
        return -1;
    }
    public static int INPOS<T>(this List<T> theList, T myobject, dlgCompare<T> dlg)
    {
        for (int iCnt = 0; iCnt < theList.Count; iCnt++)
            if (dlg(theList[iCnt], myobject) == 0)
                return iCnt;
        return -1;
    }
    public static void Sort<T>(this List<T> theList, Test2Lib.qsort<T>.dlgCompare dlg)
    {
        Test2Lib.qsort<T>.Sort(theList, dlg);
    }

    public static T[] myClone<T>(this T[] theList)
    {
        List<T> t = new List<T>(theList).myClone();
        return t.ToArray();
    }
    public static bool IN<T>(this T[] theList, T myobject)
    {
        return new List<T>(theList).IN(myobject);
    }
    public static bool IN<T>(this T[] theList, T myobject, dlgCompare<T> dlg)
    {
        return new List<T>(theList).IN(myobject, dlg);
    }
    public static int INPOS<T>(this T[] theList, T myobject)
    {
        return new List<T>(theList).INPOS(myobject);
    }
    public static int INPOS<T>(this T[] theList, T myobject, dlgCompare<T> dlg)
    {
        return new List<T>(theList).INPOS(myobject, dlg);
    }
    public static void Sort<T>(this T[] theList, Test2Lib.qsort<T>.dlgCompare dlg)
    {
        new List<T>(theList).Sort(dlg);
    }

}
