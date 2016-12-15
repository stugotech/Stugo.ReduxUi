using System.Threading;
using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi
{
    public class ReduxStore<TState, TActionBase> : IReduxStore<TState, TActionBase>
        where TState : new()
    {
        private readonly SynchronizationContext synchronizationContext;
        private readonly ReducerDelegate<TState, TActionBase> reducer;
        private readonly ISafeEventManager<StateChangedMessage<TState>> stateChangedEvent =
            new SafeEventManager<StateChangedMessage<TState>>();


        public ReduxStore(SynchronizationContext synchronizationContext,
            ReducerDelegate<TState, TActionBase> reducer)
        {
            this.synchronizationContext = synchronizationContext;
            this.reducer = reducer;
            State = new TState();
        }


        public ISafeEvent<StateChangedMessage<TState>> StateChanged => stateChangedEvent;


        public TState State { get; private set; }



        public void Dispatch(TActionBase action)
        {
            synchronizationContext.Post(DoDispatch, action);
        }


        private void DoDispatch(object action)
        {
            var newState = reducer(State, (TActionBase)action);

            if (!ReferenceEquals(newState, State))
            {
                var message = new StateChangedMessage<TState>(State, newState);
                State = newState;
                stateChangedEvent.Invoke(message);
            }
        }
    }
}
