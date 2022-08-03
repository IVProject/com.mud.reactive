using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mud.Reactive.Disposables
{
    public class Connections : List<IDisposable>, IDisposable
    {
        public Connections(int capacity): base(capacity) {}

        public Connections(IDisposable disposable) =>
            Add(disposable);
        
        public void Dispose()
        {
            foreach (var connection in this)
                connection.Dispose();
            Clear();
        }

        public static Connections operator +(Connections connections, IDisposable connection)
        {
            if (connection == null)
                return connections;
            connections?.Add(connection);
            return connections;
        }
        
        public static Connections operator +(Connections connections, Action disposeAction)
        {
            if (disposeAction == null)
                return connections;
            connections?.Add(new AnonymousDisposable(disposeAction));
            return connections;
        }
    }
}