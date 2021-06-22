using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ProtectedVariable
{
    public abstract class ProtecedObject<T> : IObservable<object>
    {
        protected T _value;
        protected List<IObserver<object>> observers;

        public ProtecedObject()
        {
            observers = new List<IObserver<object>>();
        }

        public ProtecedObject(T value) : this()
        {
            this.Value = value;
        }

        public T Value
        {
            get
            {
                CheckIntegrity();

                _value = GetValue();
                observers.ForEach(observer => { observer.OnCompleted(); });
                return _value;
            }
            set
            {
                CheckIntegrity();

                SetValue(value);
                _value = value;
                observers.ForEach(observer => { observer.OnNext(_value); });
            }
        }

        public override bool Equals(object obj)
        {
            return this.Value != null ? this.Value.Equals(obj) : obj == null;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IDisposable Subscribe(IObserver<object> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        public override string ToString()
        {
            return $@"{this.Value}";
        }

        protected void CheckIntegrity()
        {
            T protectedValue = GetValue();

            if (!IsValueValid())
            {
                observers.ForEach(observer =>
                {
                    Exception ex = new AccessViolationException();
                    ex.Data.Add("InjectedValue", _value);
                    ex.Data.Add("OriginalValue", protectedValue);
                    observer.OnError(ex);
                });
            }
        }

        protected abstract T GetValue();

        protected abstract bool IsValueValid();

        protected abstract void SetValue(T value);

        private class Unsubscriber : IDisposable
        {
            private IObserver<object> _observer;
            private List<IObserver<object>> _observers;

            public Unsubscriber(List<IObserver<object>> observers, IObserver<object> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}