using System.Collections;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Models
{
    public class ContractDurationBeforeContractEnds : IContractDurationBeforeContractEnds
    {
        public double DurationValue { get; set; }

        public DurationEnum DurationUnit { get; set; }
    }
}