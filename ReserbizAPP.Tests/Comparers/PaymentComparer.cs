using System.Collections;
using System.Collections.Generic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests.Comparers
{
    public class PaymentComparer
        : IComparer, IComparer<PaymentBreakdown>
    {
        public int Compare(PaymentBreakdown x, PaymentBreakdown y)
        {
            if (x == null || y == null) return -1;
            return x.Id == y.Id ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            var modelX = x as PaymentBreakdown;
            var modelY = y as PaymentBreakdown;
            return Compare(modelX, modelY);
        }
    }
}