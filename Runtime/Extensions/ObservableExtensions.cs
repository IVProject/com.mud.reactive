using System;
using Mud.Reactive.Disposables;

namespace Mud.Reactive
{
    public static partial class ObservableExtensions
    {
        public static IObservable<T> Filter<T>(this IObservable<T> self, Func<T, bool> filter)
        {
            return new AnonymousObserver<T>(reaction =>
            {
                return self.Subscribe(value =>
                {
                    if (filter(value)) reaction(value);
                });
            });
        }
        
        public static IObservable Filter(this IObservable self, Func<bool> filter)
        {
            return new AnonymousObserver(reaction =>
            {
                return self.Subscribe(() =>
                {
                    if (filter()) reaction();
                });
            });
        }

        public static IObservable<T> Single<T>(this IObservable<T> self)
        {
            return new AnonymousObserver<T>(reaction =>
            {
                var disp = new Connection();
                disp.Current = self.Subscribe(value =>
                {
                    reaction(value);
                    disp.Dispose();
                });
                return disp;
            });
        }
        
        public static IObservable Single(this IObservable self)
        {
            return new AnonymousObserver(reaction =>
            {
                var disp = new Connection();
                disp.Current = self.Subscribe(() =>
                {
                    reaction();
                    disp.Dispose();
                });
                return disp;
            });
        }
        
        public static IObservable<T> When<T>(this IObservable<T> self, Func<T, bool> predicate) =>
            self.Filter(predicate);
        
        public static IObservable<T> Skip<T>(this IObservable<T> self, Func<T, bool> predicate) =>
            self.Filter(value => predicate(value) == false);

        public static IObservable<int> Range(this IObservable<int> self, int min, int max) =>
            self.Filter(value => value < min && value > max);
        
        public static IObservable<float> Range(this IObservable<float> self, float min, float max) =>
            self.Filter(value => value < min && value > max);

        public static IObservable<bool> WhenTrue(this IObservable<bool> self) =>
            self.Filter(value => value == true);

        public static IObservable Concat<T>(this IObservable self, IObservable target)
        {
            return null;
        }
    }
}