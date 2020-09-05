using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ProtectedVariable
{
    public interface ProtecedObjectCallBack
    {
        void OnValueInvalid(dynamic eventInfo);
    }

    public abstract class ProtecedObject<T>
    {
        protected T _value;

        public T Value
        {
            get
            {
                CheckIntegrity();

                _value = GetValue();
                return _value;
            }
            set
            {
                CheckIntegrity();

                SetValue(value);
                _value = value;
            }
        }

        private ProtecedObjectCallBack ProtecedObjectCallBack { get; set; }

        public ProtecedObject(ProtecedObjectCallBack ProtecedObjectCallBack = null)
        {
            this.ProtecedObjectCallBack = ProtecedObjectCallBack;
        }

        public ProtecedObject(T Value, ProtecedObjectCallBack ProtecedObjectCallBack = null) : this(ProtecedObjectCallBack)
        {
            this.Value = Value;
        }

        protected abstract bool IsValueValid();

        protected abstract T GetValue();

        protected abstract void SetValue(T value);

        protected void CheckIntegrity()
        {
            T protectedValue = GetValue();

            if (!IsValueValid())
            {
                if (ProtecedObjectCallBack != null)
                {
                    dynamic eventInfo = new ExpandoObject();
                    eventInfo.InjectedValue = _value;
                    eventInfo.OriginalValue = protectedValue;

                    ProtecedObjectCallBack.OnValueInvalid(eventInfo);
                }
            }
        }

    }
}
