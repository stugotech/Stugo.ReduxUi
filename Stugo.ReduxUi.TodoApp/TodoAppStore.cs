using System.Threading;
using System.Windows;
using Stugo.ReduxUi.TodoApp.Middleware;
using Stugo.ReduxUi.TodoApp.Reducers;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp
{
    class TodoAppStore : ReduxAppBase<TodoAppRoot, object, object>
    {
        public TodoAppStore(SynchronizationContext synchronizationContext, FrameworkElement rootElement)
            : base(synchronizationContext, TodoAppReducer.Reduce, new TodoAppRoot(),
                  new UpdateUiMiddleware<TodoAppRoot, object, object>(rootElement).Chain,
                  new RollDiceMiddleware(Dispatcher).Chain)
        {
            rootElement.DataContext = State;
        }
    }
}
