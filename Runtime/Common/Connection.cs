using System;

namespace Mud.Reactive.Disposables
{
    public class Connection : IDisposable
    {
        public IDisposable Current;
        
        public void Dispose()
        {
            Current?.Dispose();
            Current = null;
        }
    }
}