namespace Stugo.ReduxUi.State
{
    public delegate ReducerDelegate<TState, TEffect, TActionBase> MiddlewareDelegate<TState, TEffect, TActionBase>(
        ReducerDelegate<TState, TEffect, TActionBase> reducer);
}
