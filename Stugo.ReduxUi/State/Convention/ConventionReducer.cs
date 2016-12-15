namespace Stugo.ReduxUi.State.Convention
{
    public static class ConventionReducer<TReducer>
    {
        public static TState Reduce<TState, TActionBase>(TState initialState, TActionBase action)
        {
            var reducers = ConventionReducerBuilder<TReducer, TState, TActionBase>.Collection;
            ReducerDelegate<TState, TActionBase> reducer;

            if (reducers.TryGetReducer(action.GetType(), out reducer))
                return reducer(initialState, action);
            else
                return initialState;
        }
    }
}
