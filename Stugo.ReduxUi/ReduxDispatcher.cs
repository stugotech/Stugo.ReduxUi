using System;
using System.Threading;

namespace Stugo.ReduxUi
{
    public class ReduxDispatcher<TActionBase> : IReduxDispatcher<TActionBase>
    {
        private readonly SynchronizationContext synchronizationContext;
        private readonly DispatchDelegate<TActionBase> callback;


        public ReduxDispatcher(SynchronizationContext synchronizationContext,
            DispatchDelegate<TActionBase> callback)
        {
            this.synchronizationContext = synchronizationContext;
            this.callback = callback;
        }


        public void Dispatch(TActionBase action)
        {
            synchronizationContext.Post(DoDispatch, action);
        }


        private void DoDispatch(object action)
        {
            callback((TActionBase)action);
        }
    }
}
