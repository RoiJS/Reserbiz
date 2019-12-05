namespace ReserbizAPP.LIB.Interfaces
{
    public interface IValidateDurationRangeResult
    {
        bool result {get; set;}
        int minValue {get; set;}
        int maxValue {get; set;}
    }
}