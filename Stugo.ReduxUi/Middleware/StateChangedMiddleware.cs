using System;
using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi.Middleware
{
    public class StateChangedMiddleware<TState, TEffect, TActionBase> : IMiddleware<TState, TEffect, TActionBase>
    {
        public delegate void CallbackDelegate(TState oldState, TState newState);

        private readonly CallbackDelegate callback;


        public StateChangedMiddleware(CallbackDelegate callback)
        {
            this.callback = callback;
        }


        protected StateChangedMiddleware()
        {
            this.callback = OnStateChanged;
        }


        public ReducerDelegate<TState, TEffect, TActionBase> Chain(
            ReducerDelegate<TState, TEffect, TActionBase> reducer)
        {
            return (initialState, action) =>
            {
                var newState = reducer(initialState, action);

                if (!ReferenceEquals(initialState, newState.State))
                    callback(initialState, newState.State);

                return newState;
            };
        }


        protected virtual void OnStateChanged(TState initialState, TState newState)
        {
            throw new NotImplementedException();
        }
    }
}
