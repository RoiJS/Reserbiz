using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractDurationBeforeContractEnds
    {
        double DurationValue { get; set; }

        DurationEnum DurationUnit { get; set; }
    }
}