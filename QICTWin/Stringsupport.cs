using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Static support class for String manipulation
/// </summary>
public static class Stringsupport
{
    /// <summary>
    /// Counts the number of occurences of a string in a line
    /// </summary>
    /// <param name="szLine">Line containing 0, 1 or more occurrences of a string to search for</param>
    /// <param name="szFind">The string to be found in the line</param>
    /// <returns>Number of occurences of the search string in the line</returns>
    public static int countChars(string szLine, string szFind)
    {
        int iRet = 0;
        int iCnt = 0;
        while (iCnt < szLine.Length)
        {
            if (szLine.IndexOf(szFind, iCnt, StringComparison.Ordinal) >= 0)
            {
                iRet++;
                iCnt = szLine.IndexOf(szFind, iCnt, StringComparison.Ordinal) + szFind.Length;
            }
            else
            {
                break;
            }
        }
        return iRet;
    }
    /// <summary>
    /// Counts the number of occurences of a character in a line
    /// </summary>
    /// <param name="szLine">Line containing 0, 1 or more occurrences of a character to search for</param>
    /// <param name="cFind">The character to be found in the line</param>
    /// <returns>Number of occurences of the search string in the line</returns>
    public static int countChars(string szLine, char cFind)
    {
        int iRet = 0;
        int iCnt = 0;
        while (iCnt < szLine.Length)
        {
            if (szLine.IndexOf(cFind, iCnt) >= 0)
            {
                iRet++;
                iCnt = szLine.IndexOf(cFind, iCnt) + 1;
            }
            else
            {
                break;
            }
        }
        return iRet;
    }

    /// <summary>
    /// Represents a pseudo-random number generator,
    /// a device that produces a sequence of numbers that meet certain statistical requirements for randomness
    /// </summary>
    public static Random random = new Random();
    /// <summary>
    /// Generates a pseudo random number between two integer boundaries
    /// </summary>
    /// <param name="min">The inclusive lower bound of the random number returned.</param>
    /// <param name="max"> The exclusive upper bound of the random number returned.
    /// maxValue must be greater than or equal to minValue</param>
    /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue;
    /// that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
    public static int RandomNumber(int min, int max)
    {
        return random.Next(min, max);
    }

    /// <summary>
    /// Generates a random string with the given length
    /// </summary>
    /// <param name="size">Size of the string</param>
    /// <param name="lowerCase">If true, generate lowercase string</param>
    /// <returns>Random string</returns>
    public static string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    /// <summary>
    /// Splits a line into sub strings and put the substrings in a list. The split string is szSep
    /// </summary>
    /// <param name="szLine">The line to be split</param>
    /// <param name="szSep">The seperator of the sub strings; seperator is NOT included in the splitted strings</param>
    /// <returns>A list with all sub strings</returns>
    public static List<string> decodeLine(string szLine, string szSep)
    {
        return decodeLine(szLine, szSep, false);
    }
    /// <summary>
    /// Splits a line into sub strings and put the substrings in a list and optionally removes the beginning and ending spaces.
    /// The split string is szSep
    /// </summary>
    /// <param name="szLine">The line to be split</param>
    /// <param name="szSep">The seperator of the sub strings; seperator is NOT included in the splitted strings</param>
    /// <param name="bTrim">Indicates if beginning and ending spaces have to be removed from the sub string</param>
    /// <returns> list with all sub strings</returns>
    public static List<string> decodeLine(string szLine, string szSep, bool bTrim)
    {
        int iPos;
        string szTmp;
        List<string> lstRet = new List<string>();

        try
        {
            iPos = szLine.IndexOf(szSep);
            while (iPos >= 0)
            {
                szTmp = szLine.Substring(0, iPos);
                if (bTrim)
                    szTmp = szTmp.Trim();
                lstRet.Add(szTmp);

                szLine = szLine.Substring(iPos + szSep.Length);
                iPos = szLine.IndexOf(szSep);
            }
            if (bTrim)
                szLine = szLine.Trim();
            lstRet.Add(szLine);
        }
        catch
        {
            throw new Exception();
        }
        return lstRet;
    }

    /// <summary>
    /// Gets the string part of a Line preceding to the szSep string
    /// </summary>
    /// <param name="szLine">The string to be examined</param>
    /// <param name="szSep">The seperator string; indicates where the preceding string ends.
    /// NOT included in the returning string</param>
    /// <returns>The string preceding the sepecrator string</returns>
    /// <remarks>If szSep is not found, an empty string is returned.</remarks>
    public static string getPreString(string szLine, string szSep)
    {
        int iPos = szLine.IndexOf(szSep);
        if (iPos < 0) return "";
        return szLine.Substring(0, iPos);
    }
    /// <summary>
    /// Gets the string part of a Line following to the szSep string
    /// </summary>
    /// <param name="szLine">The string to be examined</param>
    /// <param name="szSep">The seperator string; indicates where the following string starts.
    /// NOT included in the returning string</param>
    /// <returns>If szSep is not found, an empty string is returned.</returns>
    public static string getPostString(string szLine, string szSep)
    {
        int iPos = szLine.IndexOf(szSep);
        if (iPos < 0) return "";
        return szLine.Substring(iPos + szSep.Length);
    }
    /// <summary>
    /// Gets the string part of a Line preceeding to the szSepPre string and following to the szSepPost string
    /// </summary>
    /// <param name="szLine">The string to be examined</param>
    /// <param name="szSepPre">The first seperator string; indicates where the returning string starts.
    /// NOT included in the returning string</param>
    /// <param name="szSepPost">The second seperator string; indicates where the returning string ends.
    /// NOT included in the returning string</param>
    /// <returns>If szSepPre and/or szSepPost is not found, an empty string is returned.</returns>
    public static string getPrePostString(string szLine, string szSepPre, string szSepPost)
    {
        if (szLine.Length == 0) return szLine;
        string szRet = getPostString(szLine, szSepPre);
        if (szRet.Length == 0) return szRet;
        return getPreString(szRet, szSepPost);
    }
    /// <summary>
    /// This function add spaces to a string until a specified length is reached
    /// </summary>
    /// <param name="szString">The source string</param>
    /// <param name="iFinalLength">The length of the string, spaces are added until this length has been reached</param>
    /// <returns>The string of length iFinalLength</returns>
    /// <remarks>If iFinalLength &gt; length of szString, szString is returned</remarks>
    public static string addSpacesToString(string szString, int iFinalLength)
    {
        string szRet = szString;
        while (szRet.Length < iFinalLength)
            szRet += " ";
        return szRet;
    }
    /// <summary>
    /// This function add characters to a string until a specified length is reached
    /// </summary>
    /// <param name="szString">The source string</param>
    /// <param name="c">The character to be added</param>
    /// <param name="iFinalLength">The length of the string, spaces are added until this length has been reached</param>
    /// <returns>The string of length iFinalLength</returns>
    /// <remarks>If iFinalLength &gt; length of szString, szString is returned</remarks>
    public static string addCharsToString(string szString, char c, int iFinalLength)
    {
        string szRet = szString;
        while (szRet.Length < iFinalLength)
            szRet += c;
        return szRet;
    }
    /// <summary>
    /// Cuts the last iNrOfChars from a string
    /// </summary>
    /// <param name="szString">The string to be shortened</param>
    /// <param name="iNrOfChars">Nr of charcters to be removed from the end of the string</param>
    /// <returns>The shortened string</returns>
    public static string cutCharsFromString(string szString, int iNrOfChars)
    {
        string szRet = szString;
        if (iNrOfChars < 1) return szRet;
        if (iNrOfChars >= szRet.Length) return "";
        return szRet.Substring(0, szRet.Length - iNrOfChars);
    }

    /// <summary>
    /// Generate a string with spaces
    /// </summary>
    /// <param name="iNr">Number of spaces</param>
    /// <returns>A string filled with spaces</returns>
    public static string space(int iNr)
    {
        return genChars(' ', iNr);
    }
    /// <summary>
    /// Generate a string with a number of same characters
    /// </summary>
    /// <param name="c">character to fill the string with</param>
    /// <param name="iNr">Number of characters</param>
    /// <returns>A string with the number of specified characters</returns>
    public static string genChars(char c, int iNr)
    {
        string szRet = "";
        while (iNr > 0)
        {
            szRet += c;
            iNr--;
        }
        return szRet;
    }

}
