using System;

namespace Mud.Reactive
{
    class AnonymousObserver : IObservable
    {
        private readonly Func<Action, IDisposable> listen;

        public AnonymousObserver(Func<Action, IDisposable> subscribe) =>
            listen = subscribe;
        
        public IDisposable Subscribe(Action action) =>
            listen(action);
    }

    class AnonymousObserver<T> : IObservable<T>
    {
        private readonly Func<Action<T>, IDisposable> listen;

        public AnonymousObserver(Func<Action<T>, IDisposable> subscribe) =>
            listen = subscribe;

        public IDisposable Subscribe(Action<T> action)=>
            listen(action);

        public IDisposable Subscribe(Action action) =>
            listen(_ => action());
    }
}