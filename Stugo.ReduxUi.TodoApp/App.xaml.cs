using System.Threading;
using System.Windows;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.TodoApp.Reducers;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ReducerDelegate<TodoAppRoot, object, object> reducer = TodoAppReducer.Reduce;

            var window = new MainWindow();
            var store = new TodoAppStore(SynchronizationContext.Current, window);

            Redux.SetStore(window, store);
            window.Show();
            base.OnStartup(e);
        }


        private static ReducerDelegate<TodoAppRoot, object, object> RegisterMiddleware(
            ReducerDelegate<TodoAppRoot, object, object> reducer)
        {
            return reducer;
        }
    }
}
