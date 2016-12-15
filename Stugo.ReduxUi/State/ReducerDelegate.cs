namespace Stugo.ReduxUi.State
{
    public delegate TState ReducerDelegate<TState, in TActionBase>(TState initialState, TActionBase action);
}
