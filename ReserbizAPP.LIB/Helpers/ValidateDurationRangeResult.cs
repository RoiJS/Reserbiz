using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Helpers
{
    public class ValidateDurationRangeResult : IValidateDurationRangeResult
    {
        public bool result { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }

        public ValidateDurationRangeResult()
        {
            result = true;
            minValue = 1;
            maxValue = 1;
        }
    }
}