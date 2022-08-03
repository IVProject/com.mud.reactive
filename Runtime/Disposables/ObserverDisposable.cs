using System;

namespace Mud.Reactive.Disposables
{
    public sealed class ObserverDisposable: IDisposable
    {
        private Observer _observer;
        private Action _action;

        public ObserverDisposable(Observer observer, Action action)
        {
            _observer = observer;
            _action = action;
        }

        public void Dispose()
        {
            _observer.Unsubscribe(_action);
            _observer = null;
            _action = null;
        }
    }


    public sealed class ObserverDisposable<T>: IDisposable
    {
        private Observer<T> _observer;
        private Action<T> _action;

        public ObserverDisposable(Observer<T> observer, Action<T> action)
        {
            _observer = observer;
            _action = action;
        }

        public void Dispose()
        {
            _observer.Unsubscribe(_action);
            _observer = null;
            _action = null;
        }
    }
}