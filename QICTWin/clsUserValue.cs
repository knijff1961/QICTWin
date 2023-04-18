using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    public class clsUserValue
    {
        int iLegalPos;
        string szName;
        int iUnusedCount;

        public int LegalPos
        {
            get
            {
                return iLegalPos;
            }
            set
            {
                iLegalPos = value;
            }
        }
        public int UnusedCount
        {
            get
            {
                return iUnusedCount;
            }
            set
            {
                iUnusedCount = value;
            }
        }
        public string Name
        {
            get
            {
                return szName;
            }
        }

        public clsUserValue(string szName, int iLegalStartPos)
        {
            iLegalPos = iLegalStartPos;
            this.szName = szName.Trim();
        }
    }
}
