using System;
using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi.Middleware
{
    public class HandleEffectsMiddleware<TState, TEffect, TActionBase> : IMiddleware<TState, TEffect, TActionBase>
    {
        public delegate void CallbackDelegate(TEffect effect);

        private readonly CallbackDelegate callback;


        public HandleEffectsMiddleware(CallbackDelegate callback)
        {
            this.callback = callback;
        }


        protected HandleEffectsMiddleware()
        {
            this.callback = OnEffect;
        }


        public ReducerDelegate<TState, TEffect, TActionBase> Chain(
            ReducerDelegate<TState, TEffect, TActionBase> reducer)
        {
            return (initialState, action) =>
            {
                var newState = reducer(initialState, action);

                foreach (var effect in newState.Effects)
                {
                    OnEffect(effect);
                }

                return newState;
            };
        }


        protected virtual void OnEffect(TEffect effect)
        {
            throw new NotImplementedException();
        }
    }
}
