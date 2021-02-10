using System.Collections;
using System.Collections.Generic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests.Comparers
{
    public class AccountStatementComparer
        : IComparer, IComparer<AccountStatement>
    {
        public int Compare(AccountStatement x, AccountStatement y)
        {
            if (x == null || y == null) return -1;
            return x.Id == y.Id ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            var modelX = x as AccountStatement;
            var modelY = y as AccountStatement;
            return Compare(modelX, modelY);
        }
    }
}