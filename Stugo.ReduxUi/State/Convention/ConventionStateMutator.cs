using System;

namespace Stugo.ReduxUi.State.Convention
{
    public class ConventionStateMutator<TState, TActionBase> : IStateMutator<TState, TActionBase>
    {
        private readonly bool handleBaseActionType;


        public ConventionStateMutator(bool handleBaseActionType = false)
        {
            this.handleBaseActionType = handleBaseActionType;
        }


        public TState ApplyAction(TState initialState, TActionBase action)
        {
            StateMutatorDelegate<TState, TActionBase> handler;
            var collection = ConventionMutatorBuilder<TState, TActionBase>.Collection;

            Type resolvedType;
            var canHandle = collection.TryGetMutator(action.GetType(), out handler, out resolvedType)
                && (handleBaseActionType || resolvedType != typeof(TActionBase));

            return canHandle ? handler(initialState, action) : initialState;
        }
    }
}
