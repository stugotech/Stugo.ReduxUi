namespace Stugo.ReduxUi.State
{
    public interface IReduxState<out TState, in TActionBase>
    {
        TState ApplyAction(TActionBase action);
    }
}
