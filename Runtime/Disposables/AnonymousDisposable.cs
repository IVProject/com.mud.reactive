using System;

namespace Mud.Reactive.Disposables
{
    public class AnonymousDisposable : IDisposable
    {
        private Action _dispose;

        public AnonymousDisposable(Action dispose)
        {
            _dispose = dispose;
        }

        public void Dispose()
        {
            if (_dispose != null)
            {
                _dispose();
                _dispose = null;
            }
        }
    }
}