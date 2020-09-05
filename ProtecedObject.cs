using System;
using System.Collections.Generic;
using System.Text;

namespace ProtectedVariable
{
    public interface ProtecedObjectCallBack
    {
        void OnValueInvalid();
    }

    public abstract class ProtecedObject<T>
    {
        public T Value
        {
            get
            {
                if (!IsValueValid())
                {
                    if (ProtecedObjectCallBack != null)
                    {
                        ProtecedObjectCallBack.OnValueInvalid();
                    }
                }

                return GetValue();
            }
            set
            {
                SetValue(value);
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

    }
}
