
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Helpers;

public static class MonetaryValueExtensions
    {
        public static MonetaryValue Week(this decimal value)
        {
            return new MonetaryValue(value, MonetaryFrequency.Week);
        }

        public static MonetaryValue Month(this decimal value)
        {
            return new MonetaryValue(value, MonetaryFrequency.Month);
        }

        public static MonetaryValue Year(this decimal value)
        {
            return new MonetaryValue(value, MonetaryFrequency.Year);
        }

        public static MonetaryValue Every(this decimal value, MonetaryFrequency frequency)
        {
            return new MonetaryValue(value, frequency);
        }
    }
