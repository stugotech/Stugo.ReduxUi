using System;
using System.Collections.Generic;

namespace Stugo.ReduxUi.State
{
    /// <summary>
    /// Represents a collection of state mutators that can be indexed by action type.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <typeparam name="TActionBase">The base type of actions which mutate state.</typeparam>
    internal class StateMutatorCollection<TState, TActionBase>
    {
        private readonly Dictionary<Type, StateMutatorDelegate<TState, TActionBase>> mutators;


        /// <summary>
        /// Create a new instance with given mutators.
        /// </summary>
        public StateMutatorCollection(Dictionary<Type, StateMutatorDelegate<TState, TActionBase>> mutators)
        {
            this.mutators = mutators;
        }


        /// <summary>
        /// Create a new empty collection.
        /// </summary>
        public StateMutatorCollection() 
            : this(new Dictionary<Type, StateMutatorDelegate<TState, TActionBase>>())
        {
        }


        /// <summary>
        /// Try to get a mutator which handles the given action type, or a less derived type. 
        /// Returns null if no mutator exists for given type.
        /// </summary>
        public StateMutatorDelegate<TState, TActionBase> this[Type actionType]
        {
            get
            {
                StateMutatorDelegate<TState, TActionBase> value;
                return TryGetMutator(actionType, out value) ? value : null;
            }
            set
            {
                mutators[actionType] = value;
            }
        }


        /// <summary>
        /// Add a mutator for the given action type.
        /// </summary>
        public void Add(Type actionType, StateMutatorDelegate<TState, TActionBase> mutator)
        {
            mutators.Add(actionType, mutator);
        }


        /// <summary>
        /// Add a mutator for the given action type.
        /// </summary>
        public void Add<TAction>(StateMutatorDelegate<TState, TAction> mutator)
            where TAction: TActionBase
        {
            mutators.Add(typeof(TAction), (state, action) => mutator(state, (TAction)action));
        }


        /// <summary>
        /// Try to get a mutator for the given action type, or a less derived type.
        /// </summary>
        public bool TryGetMutator(Type actionType, out StateMutatorDelegate<TState, TActionBase> mutator)
        {
            Type resolvedType;
            return TryGetMutator(actionType, out mutator, out resolvedType);
        }


        /// <summary>
        /// Try to get a mutator for the given action type, or a less derived type.
        /// </summary>
        public bool TryGetMutator(Type actionType, 
            out StateMutatorDelegate<TState, TActionBase> mutator, out Type resolvedType)
        {
            foreach (var type in TypeAnalayser.GetAllTypes(actionType))
            {
                if (mutators.TryGetValue(type, out mutator))
                {
                    resolvedType = type;
                    return true;
                }
            }

            resolvedType = null;
            mutator = null;
            return false;
        }
    }
}
