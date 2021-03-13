namespace ReserbizAPP.LIB.Helpers
{
    public static class SystemConstants
    {
        public static class DurationUnitRangeValues
        {
            public const int DAY_MAX_VALUE = 365;
            public const int WEEK_MAX_VALUE = 52;
            public const int MONTH_MAX_VALUE = 12;
            public const int QUARTER_MAX_VALUE = 4;
            public const int YEAR_MAX_VALUE = 1;
        }

        public static class AverageDaysPerDurationUnit
        {
            public const double AVERAGE_DAYS_PER_MONTH_VALUE = 30.44;
            public const double AVERAGE_DAYS_PER_LEAP_YEAR_VALUE = 366;
            public const double AVERAGE_DAYS_PER_REGULAR_YEAR_VALUE = 365;
            public const double AVERAGE_DAYS_PER_YEAR_VALUE = 365.0;
        }
    }
}