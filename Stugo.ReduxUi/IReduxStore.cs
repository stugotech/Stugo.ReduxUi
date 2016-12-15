using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi
{
    public interface IReduxStore<TState, in TAction> : IReduxDispatcher<TAction>, INotifyStateChanged<TState>
    {
        TState State { get; }
    }
}
