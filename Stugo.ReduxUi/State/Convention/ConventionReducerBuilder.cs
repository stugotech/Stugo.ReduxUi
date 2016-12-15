using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Stugo.ReduxUi.State.Convention
{
    /// <summary>
    /// Helper class to allow easy lazy-loading of conventions.
    /// </summary>
    internal static class ConventionReducerBuilder<TReducer, TState, TActionBase>
    {
        public static ReducerCollection<TState, TActionBase> Collection { get; }

        static ConventionReducerBuilder()
        {
            Collection = new ReducerCollection<TState, TActionBase>(
                ConventionReducerBuilder.GetReducers<TReducer, TState, TActionBase>());
        }
    }

    /// <summary>
    /// Helper class to build mutator methods for the given state and action types by matching 
    /// public methods defined on the reducer class.
    /// </summary>
    internal static class ConventionReducerBuilder
    {
        public static Dictionary<Type, ReducerDelegate<TState, TActionBase>> 
            GetReducers<TReducer, TState, TActionBase>()
        {
            var reducers = new Dictionary<Type, ReducerDelegate<TState, TActionBase>>();

            foreach (var method in typeof(TReducer).GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                ReducerDelegate<TState, TActionBase> handler;
                var type = TryBuildReducer(method, out handler);

                if (type != null)
                    reducers.Add(type, handler);
            }

            return reducers;
        }


        internal static Type TryBuildReducer<TState, TActionBase>(
            MethodInfo method, out ReducerDelegate<TState, TActionBase> reducer)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            reducer = null;

            // method has to return the state
            if (method.ReturnType != typeof(TState))
                return null;

            // method has to have two parameters
            var parameters = method.GetParameters();

            if (parameters.Length != 2)
                return null;

            // first parameter has to be initial state
            if (parameters[0].ParameterType != typeof(TState))
                return null;

            var actionType = parameters[1].ParameterType;

            // second parameter has to be action base type or more derived
            if (!typeof(TActionBase).IsAssignableFrom(actionType))
                return null;

            var stateParameter = Expression.Parameter(typeof(TState));
            var actionParameter = Expression.Parameter(typeof(TActionBase));
            Expression actionExpression = actionParameter;

            if (actionType != typeof(TActionBase))
                actionExpression = Expression.Convert(actionParameter, actionType);

            var lambda = Expression.Lambda(
                Expression.Call(null, method, stateParameter, actionExpression),
                stateParameter, actionParameter
            );

            var compiled = (Func<TState, TActionBase, TState>)lambda.Compile();
            reducer = new ReducerDelegate<TState, TActionBase>(compiled);
            return actionType;
        }
    }
}
