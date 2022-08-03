using System;

namespace Mud.Reactive
{
    public interface IObservable
    {
        IDisposable Subscribe(Action action);
    }
    public interface IObservable<out T> : IObservable
    {
        IDisposable Subscribe(Action<T> action);
    }
}