using System.Threading;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp
{
    class TodoAppStore : ReduxStore<TodoAppRoot, object>
    {
        public TodoAppStore(SynchronizationContext synchronizationContext)
            : base(synchronizationContext)
        {
        }
    }
}
