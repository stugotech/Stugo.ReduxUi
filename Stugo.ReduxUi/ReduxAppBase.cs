using System;
using System.Linq;
using System.Threading;
using Stugo.ReduxUi.Middleware;
using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi
{
    public abstract class ReduxAppBase<TState, TEffect, TActionBase>
    {
        private readonly ReducerDelegate<TState, TEffect, TActionBase> reducer;


        protected ReduxAppBase(SynchronizationContext synchronizationContext,
            ReducerDelegate<TState, TEffect, TActionBase> reducer,
            TState initialState,
            params MiddlewareDelegate<TState, TEffect, TActionBase>[] middleware)
        {
            State = initialState;
            Dispatcher = new ReduxDispatcher<TActionBase>(synchronizationContext, DispatchCallback);
            this.reducer = middleware.Chain()(reducer);
        }


        public TState State { get; private set; }
        public IReduxDispatcher<TActionBase> Dispatcher { get; }


        public void Dispatch(TActionBase action)
        {
            Dispatcher.Dispatch(action);
        }


        private void DispatchCallback(TActionBase action)
        {
            State = reducer(State, action).State;
        }
    }
}
