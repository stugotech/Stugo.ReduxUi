namespace Stugo.ReduxUi.State
{
    public interface INotifyStateChanged<TState>
    {
        ISafeEvent<StateChangedMessage<TState>> StateChanged { get; }
    }
}
