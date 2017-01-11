namespace Stugo.ReduxUi.State
{
    public interface IReducerResult<out TState, out TEffect>
    {
        TState State { get; }
        TEffect[] Effects { get; }
    }
}
