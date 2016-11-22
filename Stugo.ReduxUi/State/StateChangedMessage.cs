namespace Stugo.ReduxUi.State
{
    public class StateChangedMessage<TState>
    {
        public StateChangedMessage(TState oldState, TState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        public TState OldState { get; }
        public TState NewState { get; }
    }
}
