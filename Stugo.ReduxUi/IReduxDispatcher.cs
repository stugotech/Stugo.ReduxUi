namespace Stugo.ReduxUi
{
    public interface IReduxDispatcher<in TAction>
    {
        void Dispatch(TAction action);
    }
}
