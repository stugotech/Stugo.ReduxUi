namespace Stugo.ReduxUi.State
{
    public static class ReducerExtensions
    {
        public static IReducerResult<TState, TEffects> WithEffects<TState, TEffects>(
            this TState state, params TEffects[] effects)
        {
            return new ReducerResult<TState, TEffects>(state, effects);
        }


        public static IReducerResult<TState, TEffects> WithEffects<TState, TEffects>(
            this TState state, params TEffects[][] effects)
        {
            return new ReducerResult<TState, TEffects>(state, effects);
        }
    }
}
