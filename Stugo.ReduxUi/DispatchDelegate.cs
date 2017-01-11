namespace Stugo.ReduxUi
{
    public delegate void DispatchDelegate<in TActionBase>(TActionBase action);
}
