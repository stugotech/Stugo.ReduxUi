namespace Stugo.ReduxUi.State
{
    public delegate IReducerResult<TState, TEffect> ReducerDelegate<TState, out TEffect, in TActionBase>(
        TState initialState, TActionBase action);
}
