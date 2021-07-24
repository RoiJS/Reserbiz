using System.Collections;
using System.Collections.Generic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests.Comparers
{
    public class SpaceComparer
        : IComparer, IComparer<Space>
    {
        public int Compare(Space x, Space y)
        {
            if (x == null || y == null) return -1;
            return x.Id == y.Id ? 0 : -1;
        }

        public int Compare(object x, object y)
        {
            var modelX = x as Space;
            var modelY = y as Space;
            return Compare(modelX, modelY);
        }
    }
}