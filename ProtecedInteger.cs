using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ProtectedVariable
{

    public class ProtecedInteger : ProtecedObject<int>
    {

        private static int? factor1Instance;

        public ProtecedInteger(int Value, ProtecedObjectCallBack ProtecedObjectCallBack = null) : base(Value, ProtecedObjectCallBack)
        {
            this.Value = Value;
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
