using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Stugo.ReduxUi.State.Convention
{
    /// <summary>
    /// Helper class to allow easy lazy-loading of conventions.
    /// </summary>
    internal static class ConventionMutatorBuilder<TState, TActionBase>
    {
        public static StateMutatorCollection<TState, TActionBase> Collection { get; }

        static ConventionMutatorBuilder()
        {
            Collection = new StateMutatorCollection<TState, TActionBase>(
                ConventionMutatorBuilder.GetMutators<TState, TActionBase>());
        }
    }


    /// <summary>
    /// Helper class to build mutator methods for the given state and action types by matching 
    /// public methods defined on the state class.
    /// </summary>
    internal static class ConventionMutatorBuilder
    {
        public static Dictionary<Type, StateMutatorDelegate<TState, TActionBase>> GetMutators<TState, TActionBase>()
        {
            var mutators = new Dictionary<Type, StateMutatorDelegate<TState, TActionBase>>();

            foreach (var method in typeof(TState).GetMethods())
            {
                StateMutatorDelegate<TState, TActionBase> handler;
                var type = TryBuildMutator(method, out handler);

                if (type != null)
                    mutators.Add(type, handler);
            }

            return mutators;
        }


        internal static Type TryBuildMutator<TState, TActionBase>(MethodInfo method, out StateMutatorDelegate<TState, TActionBase> mutator)
        {
            mutator = null;

            if (method.ReturnType != typeof(TState))
                return null;

            var parameters = method.GetParameters();

            if (parameters.Length != 1)
                return null;

            var actionType = parameters[0].ParameterType;

            if (!typeof(TActionBase).IsAssignableFrom(actionType))
                return null;

            var stateParameter = Expression.Parameter(typeof(TState));
            var actionParameter = Expression.Parameter(typeof(TActionBase));
            Expression actionExpression = actionParameter;

            if (actionType != typeof(TActionBase))
                actionExpression = Expression.Convert(actionParameter, actionType);

            var lambda = Expression.Lambda(
                Expression.Call(stateParameter, method, actionExpression),
                stateParameter, actionParameter
            );

            var compiled = (Func<TState, TActionBase, TState>)lambda.Compile();
            mutator = new StateMutatorDelegate<TState, TActionBase>(compiled);
            return actionType;
        }
    }
}
