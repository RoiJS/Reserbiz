using System.Collections;
using System.Collections.Generic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests.Comparers
{
    public class ContractComparer
        : IComparer, IComparer<Contract>
    {
        public int Compare(Contract x, Contract y)
        {
            if (x == null || y == null) return -1;
            return x.Id == y.Id ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            var modelX = x as Contract;
            var modelY = y as Contract;
            return Compare(modelX, modelY);
        }
    }
}