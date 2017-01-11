using System.Linq;
using Stugo.ReduxUi.State;

namespace Stugo.ReduxUi.Middleware
{
    public static class MiddlewareExtensions
    {
        public static MiddlewareDelegate<TState, TEffect, TActionBase> Chain<TState, TEffect, TActionBase>(
            this MiddlewareDelegate<TState, TEffect, TActionBase> first,
            MiddlewareDelegate<TState, TEffect, TActionBase> second)
        {
            return reduce => second(first(reduce));
        }


        public static MiddlewareDelegate<TState, TEffect, TActionBase> Chain<TState, TEffect, TActionBase>(
            this MiddlewareDelegate<TState, TEffect, TActionBase>[] middleware)
        {
            if (middleware.Length == 0)
                return reducer => reducer;
            else
                return middleware.Skip(1).Aggregate(middleware.First(), Chain);
        }
    }
}
