using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi.Middleware
{
    public interface IMiddleware<TState, TEffect, TActionBase>
    {
        ReducerDelegate<TState, TEffect, TActionBase> Chain(
            ReducerDelegate<TState, TEffect, TActionBase> reducer);
    }
}
