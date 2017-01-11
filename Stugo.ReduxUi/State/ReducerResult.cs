using System;
using System.Linq;

namespace Stugo.ReduxUi.State
{
    public class ReducerResult<TState, TEffect> : IReducerResult<TState, TEffect>
    {
        public ReducerResult(TState state)
        {
            State = state;
            Effects = new TEffect[0];
        }


        public ReducerResult(TState state, params TEffect[] effects)
        {
            State = state;
            Effects = effects;
        }


        public ReducerResult(TState state, params TEffect[][] effects)
        {
            State = state;
            Effects = effects.SelectMany(x => x).ToArray();
        }


        public TState State { get; }
        public TEffect[] Effects { get; }
    }
}
