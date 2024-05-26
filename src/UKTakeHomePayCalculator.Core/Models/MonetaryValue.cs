namespace UKTakeHomePayCalculator.Core.Models
{
    public enum MonetaryValueInfinity
    {
        None,
        PositiveInfinity,
        NegativeInfinity,
    }

    public struct MonetaryValue : IComparable<MonetaryValue>
    {
        private readonly decimal _value = 0.0m;
        private readonly MonetaryFrequency _frequency = MonetaryFrequency.Week;
        public MonetaryFrequency Frequency => _frequency;
        private decimal BaseValue => Math.Round(_value / (int)_frequency, 8);
        private MonetaryValueInfinity _infinity = MonetaryValueInfinity.None;
        private const int COMPARISON_DIGITS = 2;

        public static MonetaryValue Zero => new MonetaryValue(0.0m, MonetaryFrequency.Week);

        public static MonetaryValue PositiveInfinity
            => new MonetaryValue(0.0m, MonetaryFrequency.Week) { _infinity = MonetaryValueInfinity.PositiveInfinity };

        public static MonetaryValue NegativeInfinity
            => new MonetaryValue(0.0m, MonetaryFrequency.Week) { _infinity = MonetaryValueInfinity.NegativeInfinity };
        
        public MonetaryValue(decimal value, MonetaryFrequency frequency)
        {
            _value = value;
            _frequency = frequency;
        }

        public static MonetaryValue operator +(MonetaryValue value1, MonetaryValue value2)
        {
            if (IsSameInfinity(value1, value2))
                return value1;

            if (IsOppositeInfinity(value1, value2))
                return Zero;

            if (value1._infinity != MonetaryValueInfinity.None)
                return value1;

            if (value2._infinity != MonetaryValueInfinity.None)
                return value2;

            return new MonetaryValue((value1.BaseValue + value2.BaseValue) * (int)value1._frequency, value1._frequency);

        }

        public static MonetaryValue operator *(MonetaryValue value1, decimal value2)
        {
            if (value1._infinity == MonetaryValueInfinity.PositiveInfinity)
                if (value2 > 0)
                    return value1;
                else
                    return NegativeInfinity;

            if (value1._infinity == MonetaryValueInfinity.NegativeInfinity)
                if (value2 > 0)
                    return value1;
                else
                    return PositiveInfinity;

            if (value2 == 0)
                return Zero;

            return new MonetaryValue(value1._value * value2, value1._frequency);
        }

        public static MonetaryValue operator -(MonetaryValue value1, MonetaryValue value2)
        {
            return value1 + (value2 * -1);
        }

        public static MonetaryValue operator -(MonetaryValue value1)
        {
            return value1 * -1;
        }

        public static MonetaryValue operator /(MonetaryValue value1, decimal value2)
        {
            if (value1._infinity == MonetaryValueInfinity.PositiveInfinity)
                if (value2 > 0)
                    return value1;
                else
                    return NegativeInfinity;

            if (value1._infinity == MonetaryValueInfinity.NegativeInfinity)
                if (value2 > 0)
                    return value1;
                else
                    return PositiveInfinity;

            if (value2 == 0)
                throw new DivideByZeroException("Cannot divide a MonetaryValue by zero.");

            return new MonetaryValue(value1._value / value2, value1._frequency);
        }

        public static bool operator <(MonetaryValue value1, MonetaryValue value2)
        {
            if (IsSameInfinity(value1, value2))
                return false;

            if (value1._infinity == MonetaryValueInfinity.NegativeInfinity || 
                value2._infinity == MonetaryValueInfinity.PositiveInfinity)
                return true;

            if (value1._infinity == MonetaryValueInfinity.PositiveInfinity || 
                value2._infinity == MonetaryValueInfinity.NegativeInfinity)
                return false;

            return Math.Round(value1.BaseValue, COMPARISON_DIGITS) < Math.Round(value2.BaseValue, COMPARISON_DIGITS);
        }

        public static bool operator >(MonetaryValue value1, MonetaryValue value2)
        {
            if (IsSameInfinity(value1, value2))
                return false;

            if (value1._infinity == MonetaryValueInfinity.PositiveInfinity || 
                value2._infinity == MonetaryValueInfinity.NegativeInfinity)
                return true;

            if (value1._infinity == MonetaryValueInfinity.NegativeInfinity || 
                value2._infinity == MonetaryValueInfinity.PositiveInfinity)
                return false;

            return Math.Round(value1.BaseValue, COMPARISON_DIGITS) > Math.Round(value2.BaseValue, COMPARISON_DIGITS);
        }

        public static bool operator <=(MonetaryValue value1, MonetaryValue value2)
        {
            return value1 < value2 || value1 == value2;
        }

        public static bool operator >=(MonetaryValue value1, MonetaryValue value2)
        {
            return value1 > value2 || value1 == value2;
        }

        public static bool operator ==(MonetaryValue value1, MonetaryValue value2)
        {
            return IsEqualTo(value1, value2);
        }

        public static bool operator !=(MonetaryValue value1, MonetaryValue value2)
        {
            return !IsEqualTo(value1, value2);
        }

        private static bool IsSameInfinity(MonetaryValue value1, MonetaryValue value2)
        {
            if (value1._infinity == MonetaryValueInfinity.None || value2._infinity == MonetaryValueInfinity.None)
                return false;

            return value1._infinity == value2._infinity;
        }

        private static bool IsOppositeInfinity(MonetaryValue value1, MonetaryValue value2)
        {
            if (value1._infinity == MonetaryValueInfinity.None || value2._infinity == MonetaryValueInfinity.None)
                return false;

            return value1._infinity != value2._infinity;
        }

        private static bool IsEqualTo(MonetaryValue value1, MonetaryValue value2)
        {
            if (IsSameInfinity(value1, value2))
                return true;

            if (IsOppositeInfinity(value1, value2))
                return false;

            if (value1._infinity != MonetaryValueInfinity.None || value2._infinity != MonetaryValueInfinity.None)
                return false;

            return Math.Round(value1.BaseValue, COMPARISON_DIGITS) == Math.Round(value2.BaseValue, COMPARISON_DIGITS);
        }

        public MonetaryValue ConvertTo(MonetaryFrequency newFrequency)
        {
            if (_infinity == MonetaryValueInfinity.PositiveInfinity || _infinity == MonetaryValueInfinity.NegativeInfinity)
                return this;

            return new MonetaryValue(_value * (int)newFrequency / (int)_frequency, newFrequency);
        }

        public override bool Equals(object? value)
        {
            if (value is null)
                return false;

            if (value is not MonetaryValue monetaryValue)
                return false;

            return IsEqualTo(this, monetaryValue);
        }

        public override int GetHashCode()
        {
            int initialHash = 23;

            unchecked
            {
                initialHash = initialHash * 23 + _value.GetHashCode();
                initialHash = initialHash * 23 + _frequency.GetHashCode();
            }

            return initialHash;
        }

        public override string ToString()
        {
            if (_infinity == MonetaryValueInfinity.PositiveInfinity)
                return "+Infinity";

            if (_infinity == MonetaryValueInfinity.NegativeInfinity)
                return "-Infinity";

            if (_value == 0m)
                return "0.00";

            return $"{Math.Round(_value, 2):n2}/{_frequency}";
        }

        public int CompareTo(MonetaryValue other)
        {
            if (this < other) return -1;
            else if (this > other) return 1;
            else return 0;
        }
    }
}
