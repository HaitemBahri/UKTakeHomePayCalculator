namespace UKTakeHomePayCalculator.Core.Models
{
    public readonly struct MonetaryFrequency
    {
        #region fields
        private readonly MonetaryFrequencyEnum _monetaryFrequencyEnum;
        private readonly int _count;
        #endregion

        #region Static
        public static MonetaryFrequency Week => new MonetaryFrequency(MonetaryFrequencyEnum.Week);
        public static MonetaryFrequency Weeks(int weekCount)
        {
            if(weekCount is < 1 or > 4)
                throw new ArgumentOutOfRangeException(nameof(weekCount), "_value should be between 1 and 4.");

            return new MonetaryFrequency(MonetaryFrequencyEnum.Week, weekCount);
        }
        public static MonetaryFrequency Month => new MonetaryFrequency(MonetaryFrequencyEnum.Month);
        public static MonetaryFrequency Months(int monthCount)
        {
            if (monthCount is < 1 or > 12)
                throw new ArgumentOutOfRangeException(nameof(monthCount), "_value should be between 1 and 12.");

            return new MonetaryFrequency(MonetaryFrequencyEnum.Month, monthCount);
        }
        public static MonetaryFrequency Year => new MonetaryFrequency(MonetaryFrequencyEnum.Year);
        #endregion

        #region Constructors

        private MonetaryFrequency(MonetaryFrequencyEnum monetaryFrequencyEnum, int count = 1)
        {
            _monetaryFrequencyEnum = monetaryFrequencyEnum;
            _count = count;
        }
        #endregion

        #region Operators
        public static explicit operator int(MonetaryFrequency monetaryFrequency)
        {
            return (int)monetaryFrequency._monetaryFrequencyEnum * monetaryFrequency._count;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            var returnString = _count == 1 ? _monetaryFrequencyEnum.ToString() : $"{_count} {_monetaryFrequencyEnum}s";

            return returnString;
        }
        #endregion
    }

    public enum MonetaryFrequencyEnum
    {
        Week = 7,
        Month = 30,
        Year = 365,
    }
}
