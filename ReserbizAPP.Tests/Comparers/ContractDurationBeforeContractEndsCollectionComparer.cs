using System.Collections;
using System.Collections.Generic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests.Comparers
{
    public class ContractDurationBeforeContractEndsCollectionComparer 
        : IComparer, IComparer<ContractDurationBeforeContractEnds>
    {
        public int Compare(ContractDurationBeforeContractEnds x, ContractDurationBeforeContractEnds y)
        {
            if (x == null || y == null) return -1;
            return x.DurationValue == y.DurationValue && x.DurationUnitText == y.DurationUnitText ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            var modelX = x as ContractDurationBeforeContractEnds;
            var modelY = y as ContractDurationBeforeContractEnds;
            return Compare(modelX, modelY);
        }
    }
}