namespace Stugo.ReduxUi.State
{
    public interface IStateMutator<TState, in TAction>
    {
        TState ApplyAction(TState initialState, TAction action);
    }
}
