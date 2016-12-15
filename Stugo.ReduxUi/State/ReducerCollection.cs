using System;
using System.Collections.Generic;

namespace Stugo.ReduxUi.State
{
    /// <summary>
    /// Represents a collection of state mutators that can be indexed by action type.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TActionBase">The base type of actions which mutate state.</typeparam>
    internal class ReducerCollection<TState, TActionBase>
    {
        private readonly Dictionary<Type, ReducerDelegate<TState, TActionBase>> reducers;


        /// <summary>
        /// Create a new instance with given reducers.
        /// </summary>
        public ReducerCollection(Dictionary<Type, ReducerDelegate<TState, TActionBase>> reducers)
        {
            this.reducers = reducers;
        }


        /// <summary>
        /// Create a new empty collection.
        /// </summary>
        public ReducerCollection() 
            : this(new Dictionary<Type, ReducerDelegate<TState, TActionBase>>())
        {
        }


        /// <summary>
        /// Try to get a reducer which handles the given action type, or a less derived type. 
        /// Returns null if reducer mutator exists for given type.
        /// </summary>
        public ReducerDelegate<TState, TActionBase> this[Type actionType]
        {
            get
            {
                ReducerDelegate<TState, TActionBase> value;
                return TryGetReducer(actionType, out value) ? value : null;
            }
            set
            {
                reducers[actionType] = value;
            }
        }


        /// <summary>
        /// Add a reducer for the given action type.
        /// </summary>
        public void Add(Type actionType, ReducerDelegate<TState, TActionBase> reducer)
        {
            reducers.Add(actionType, reducer);
        }


        /// <summary>
        /// Add a reducer for the given action type.
        /// </summary>
        public void Add<TAction>(ReducerDelegate<TState, TAction> reducer)
            where TAction: TActionBase
        {
            reducers.Add(typeof(TAction), (state, action) => reducer(state, (TAction)action));
        }


        /// <summary>
        /// Try to get a reducer for the given action type, or a less derived type.
        /// </summary>
        public bool TryGetReducer(Type actionType, out ReducerDelegate<TState, TActionBase> reducer)
        {
            Type resolvedType;
            return TryGetReducer(actionType, out reducer, out resolvedType);
        }


        /// <summary>
        /// Try to get a reducer for the given action type, or a less derived type.
        /// </summary>
        public bool TryGetReducer(Type actionType, 
            out ReducerDelegate<TState, TActionBase> reducer, out Type resolvedType)
        {
            foreach (var type in TypeAnalayser.GetAllTypes(actionType))
            {
                if (reducers.TryGetValue(type, out reducer))
                {
                    resolvedType = type;
                    return true;
                }
            }

            resolvedType = null;
            reducer = null;
            return false;
        }
    }
}
