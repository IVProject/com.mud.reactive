namespace Mud.Reactive
{
    public interface IObserver: IObservable
    {
        void Update();
    }

    public interface IObserver<T>: IObservable<T>
    {
        void Update(T value);
    }
}