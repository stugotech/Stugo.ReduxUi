namespace Stugo.ReduxUi.State
{
    public delegate TState StateMutatorDelegate<TState, in TActionBase>(TState initialState, TActionBase action);
}
