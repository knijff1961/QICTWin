using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class CNum
{
    public static bool bitTst(int iNr, int iBit)
    {
        int iPos = 1;
        iPos <<= iBit;

        if ((iNr & iPos) != 0) return true;
        return false;
    }

    public static int bitSet(int iNr, int iBit)
    {
        int iPos = 1;
        iPos <<= iBit;
        return (iNr | iPos);
    }

    public static int bitClr(int iNr, int iBit)
    {
        int iPos = 1;
        iPos <<= iBit;
        iPos ^= 1;
        return (iNr & iPos);
    }

}
