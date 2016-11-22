using System.Threading;
using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi
{
    public class ReduxStore<TState, TActionBase> : IReduxStore<TState, TActionBase>
        where TState : IReduxState<TState, TActionBase>, new()
    {
        public delegate TState ReducerDelegate(TState state, object message);

        private readonly SynchronizationContext synchronizationContext;
        private readonly ISafeEventManager<StateChangedMessage<TState>> stateChangedEvent =
            new SafeEventManager<StateChangedMessage<TState>>();


        public ReduxStore(SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
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
            var oldState = State;
            State = State.ApplyAction((TActionBase)action);

            if (!object.ReferenceEquals(oldState, State))
                stateChangedEvent.Invoke(new StateChangedMessage<TState>(oldState, State));
        }
    }
}
