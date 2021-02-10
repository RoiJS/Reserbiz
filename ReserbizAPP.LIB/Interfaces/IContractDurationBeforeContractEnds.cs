using ReserbizAPP.LIB.Enums;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractDurationBeforeContractEnds
    {
        double DurationValue { get; set; }

        string DurationUnitText { get; set; }
    }
}