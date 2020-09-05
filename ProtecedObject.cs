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
        private object Factor1 { get; set; }
        private object Factor2 { get; set; }
        public T Value
        {
            get
            {
                if (IsValueValid())
                {
                    if (ProtecedObjectCallBack != null)
                    {
                        ProtecedObjectCallBack.OnValueInvalid();
                    }
                }
                IsValueValid();
                return default;
            }
            set
            {

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

        public abstract void RefreshValue();

    }
}
