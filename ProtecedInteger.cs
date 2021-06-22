using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ProtectedVariable
{
    public class ProtecedInteger : ProtecedObject<int>
    {
        private static int? factor1Instance;

        public ProtecedInteger(int value) : base(value)
        {
            this.Value = value;
        }

        private static int Factor1
        {
            get
            {
                if (factor1Instance == null)
                {
                    string stringData = DateTime.Now.Millisecond.ToString();
                    factor1Instance = Convert.ToInt32(DateTime.Now.Millisecond.ToString().Substring(0, Math.Min(2, stringData.Length)));
                }
                return (int)factor1Instance;
            }
        }

        private BigInteger Factor2 { get; set; }

        public static ProtecedInteger operator -(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value - b.Value);

        public static bool operator !=(ProtecedInteger a, ProtecedInteger b) => a.Value != b.Value;

        public static ProtecedInteger operator %(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value % b.Value);

        public static ProtecedInteger operator &(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value & b.Value);

        public static ProtecedInteger operator *(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value * b.Value);

        public static ProtecedInteger operator /(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value / b.Value);

        public static ProtecedInteger operator ^(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value ^ b.Value);

        public static ProtecedInteger operator |(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value | b.Value);

        public static ProtecedInteger operator +(ProtecedInteger a, ProtecedInteger b) => new ProtecedInteger(a.Value + b.Value);

        public static bool operator <(ProtecedInteger a, ProtecedInteger b) => a.Value < b.Value;

        public static ProtecedInteger operator <<(ProtecedInteger a, int b) => new ProtecedInteger(a.Value << b);

        public static bool operator <=(ProtecedInteger a, ProtecedInteger b) => a.Value <= b.Value;

        public static bool operator ==(ProtecedInteger a, ProtecedInteger b) => a.Value == b.Value;

        public static bool operator >(ProtecedInteger a, ProtecedInteger b) => a.Value > b.Value;

        public static bool operator >=(ProtecedInteger a, ProtecedInteger b) => a.Value >= b.Value;

        public static ProtecedInteger operator >>(ProtecedInteger a, int b) => new ProtecedInteger(a.Value >> b);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected override int GetValue()
        {
            return (int)(Factor2 / Factor1);
        }

        protected override bool IsValueValid()
        {
            return _value == (int)(Factor2 / Factor1);
        }

        protected override void SetValue(int value)
        {
            Factor2 = (BigInteger)value * Factor1;
        }
    }
}