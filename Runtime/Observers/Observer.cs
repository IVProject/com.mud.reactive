using System;
using System.Collections.Generic;
using Mud.Reactive.Disposables;

namespace Mud.Reactive
{
    public class Observer : IObserver
    {
        private List<Action> _subscribers = new List<Action>();

        public int CountSubscribers() =>
            _subscribers.Count;
        
        public bool Unsubscribe(Action action)
        {
           return _subscribers.Remove(action);
        }

        public IDisposable Subscribe(Action action)
        {
            _subscribers.Add(action);
            return new ObserverDisposable(this, action);
        }

        public void Update()
        {
            lock (_subscribers)
            {
                for (var i = 0; i < _subscribers.Count; i++)
                    _subscribers[i]();
                
            }
        }

        public static bool operator -(Observer observer, Action action) =>
            observer.Unsubscribe(action);
        public static IDisposable operator +(Observer observer, Action action) =>
            observer.Subscribe(action);
    }
    
    public class Observer<T> : IObserver<T>
    {
        private List<Action<T>> _subscribers = new List<Action<T>>();

        public int CountSubscribers() =>
            _subscribers.Count;
        
        public bool Unsubscribe(Action action)
        {
            Action<T> wrapper = _ => action();
            return _subscribers.Remove(wrapper);
        }
        
        public bool Unsubscribe(Action<T> action)
        {
            return _subscribers.Remove(action);
        }
        
        public IDisposable Subscribe(Action action)
        {
            Action<T> wrapper = _ => action();
            _subscribers.Add(wrapper);
            return new ObserverDisposable<T>(this, wrapper);
        }
        
        public IDisposable Subscribe(Action<T> action)
        {
            _subscribers.Add(action);
            return new ObserverDisposable<T>(this, action);
        }

        public void Update(T value)
        {
            lock (_subscribers)
            {
                for (var i = 0; i < _subscribers.Count; i++)
                    _subscribers[i](value);
            }
        }

        public static bool operator -(Observer<T> observer, Action<T> action) =>
            observer.Unsubscribe(action);
        public static bool operator -(Observer<T> observer, Action action) =>
            observer.Unsubscribe(action);
        public static IDisposable operator +(Observer<T> observer, Action<T> action) =>
            observer.Subscribe(action);
        public static IDisposable operator +(Observer<T> observer, Action action) =>
            observer.Subscribe(action);
    }
}