using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.Helpers
{
    public static class SystemExtensions
    {
        /// <summary>
        /// Calculate the number of days based on the duration value and unit.
        /// Example: 1 week = 7 days
        /// </summary>
        /// <param name="referenceDate">Reference object to where the function will be extended.</param>
        /// <param name="durationValue">Duration value</param>
        /// <param name="durationUnit">Duration Unit. Example: Day, Week, Month, Quarter and Year</param>
        /// <returns>Returns the calculated number of days</returns>
        public static int CalculateDaysBasedOnDuration(this DateTime referenceDate, int durationValue, DurationEnum durationUnit)
        {
            var numberOfDays = 0;
            var endDate = DateTime.MinValue;
            var dateDiff = TimeSpan.MinValue;
            var months = 0;

            switch (durationUnit)
            {
                case DurationEnum.Day:
                    numberOfDays = durationValue;
                    break;
                case DurationEnum.Week:
                    numberOfDays = durationValue * 7; // 7 days per weeks
                    break;
                case DurationEnum.Month:
                    months = durationValue;
                    endDate = referenceDate.AddMonths(months);
                    dateDiff = endDate.Subtract(referenceDate);
                    numberOfDays = dateDiff.Days;
                    break;
                case DurationEnum.Quarter:
                    months = durationValue * 3; // 3 Months per quarter
                    endDate = referenceDate.AddMonths(months);
                    dateDiff = endDate.Subtract(referenceDate);
                    numberOfDays = dateDiff.Days;
                    break;
                case DurationEnum.Year:
                    months = durationValue * 12; // 12 Months per year
                    endDate = referenceDate.AddMonths(months);
                    dateDiff = endDate.Subtract(referenceDate);
                    numberOfDays = dateDiff.Days;
                    break;
            }

            return numberOfDays;
        }

        /// <summary>
        /// This calculates the last day of the month.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static IQueryable<TEntity> Includes<TEntity>(this IQueryable<TEntity> query,
                                    params Expression<Func<TEntity, object>>[] includes)
                                    where TEntity : class, IEntity
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public static Expression<Func<TEntity, object>>[] IncludeAlso<TEntity>(this IEnumerable<TEntity> entities,
                                params Expression<Func<TEntity, object>>[] includes)
                                where TEntity : class, IEntity
        {
            return includes;
        }


        /// <summary>
        /// This will be generic function use to retrieve user claim depending on claim type specified as parameter.
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static object GetUserClaim(this IIdentity identity, string claimType)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(claimType);

            if (claim == null)
                return null;

            return claim.Value;
        }

        public static string ToCurrencyFormat(this float number)
        {
            NumberFormatInfo nfi = new CultureInfo("fil-PH", false).NumberFormat;
            nfi.CurrencySymbol = "Php ";
            var amountFormatted = string.Format(nfi, "{0:C}", number);
            return amountFormatted;
        }

        /// <summary>
        /// Convert date and time based on the timezone id
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timezoneId"></param>
        /// <returns></returns>
        public static DateTime ConvertToTimeZone(this DateTime dateTime, string timezoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime.ToUniversalTime(), timezoneId);
        }

        /// <summary>
        /// Convert date and time based on the local timezone id
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timezoneId"></param>
        /// <returns></returns>
        public static DateTime ToLocalTimeZone(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime.ToUniversalTime(), TimeZoneInfo.Local.Id);
        }
    }
}