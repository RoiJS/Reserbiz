using System;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Helpers
{
    public static class ContractHelper
    {
        /// <summary>
        /// This calculates and translates the days to Year, months and days format.
        /// Note: In some scenarios, this does not give the exact output since
        ///       it based its calculation to the average days per month (approximately 30.44 days).
        /// </summary>
        /// <param name="contractDurationBeforeContractEnds"></param>
        /// <param name="remainingDays"></param>
        public static void CalculateDurationValueBeforeContractEnds(ref List<IContractDurationBeforeContractEnds> contractDurationBeforeContractEnds, double remainingDays)
        {
            var durationValue = new ContractDurationBeforeContractEnds();
            var newRemainingDays = 0.0;
            var durationUnit = GetDurationUnit(remainingDays);

            durationValue.DurationValue = durationUnit == DurationEnum.Day ? remainingDays : Math.Floor(remainingDays / GetAverageDaysValuePerDurationUnit(durationUnit));
            durationValue.DurationUnitText = durationUnit.ToString();

            // Check if current value is not zero
            if (durationValue.DurationValue != 0)
            {
                contractDurationBeforeContractEnds.Add(durationValue);
            }

            if (durationUnit != DurationEnum.Day)
            {
                newRemainingDays = Math.Round(remainingDays - (durationValue.DurationValue * GetAverageDaysValuePerDurationUnit(durationUnit)));

                CalculateDurationValueBeforeContractEnds(ref contractDurationBeforeContractEnds, newRemainingDays);
            }
        }

        private static double GetAverageDaysValuePerDurationUnit(DurationEnum duration)
        {
            var value = 0.0;

            switch (duration)
            {
                case DurationEnum.Month:
                    value = SystemConstants.AverageDaysPerDurationUnit.AVERAGE_DAYS_PER_MONTH_VALUE;
                    break;
                case DurationEnum.Year:
                    value = SystemConstants.AverageDaysPerDurationUnit.AVERAGE_DAYS_PER_YEAR_VALUE;
                    break;
            }

            return value;
        }

        private static DurationEnum GetDurationUnit(double remainingDays)
        {
            var durationUnit = DurationEnum.None;

            if (remainingDays >= SystemConstants.AverageDaysPerDurationUnit.AVERAGE_DAYS_PER_YEAR_VALUE)
            {
                durationUnit = DurationEnum.Year;
            }
            else if (remainingDays >= SystemConstants.AverageDaysPerDurationUnit.AVERAGE_DAYS_PER_MONTH_VALUE && remainingDays < SystemConstants.AverageDaysPerDurationUnit.AVERAGE_DAYS_PER_YEAR_VALUE)
            {
                durationUnit = DurationEnum.Month;
            }
            else if (remainingDays >= 0 && remainingDays < SystemConstants.AverageDaysPerDurationUnit.AVERAGE_DAYS_PER_MONTH_VALUE)
            {
                durationUnit = DurationEnum.Day;
            }

            return durationUnit;
        }
    }
}