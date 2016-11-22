using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TodoAppStore store;


        public MainWindow()
        {
            store = new TodoAppStore(SynchronizationContext.Current);
            store.StateChanged.AddHandler(this, x => x.OnStoreStateChanged);
            Redux.SetStore(this, store);
            InitializeComponent();
        }


        private void OnStoreStateChanged(StateChangedMessage<TodoAppRoot> state)
        {
            this.DataContext = state.NewState;
        }


        private void TodosListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            store.Dispatch(new SelectTodoAction((Todo)todosListBox.SelectedItem));
        }


        private void TodosListBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && store.State.SelectedTodo != null)
            {
                store.Dispatch(new DeleteTodoAction(store.State.SelectedTodo));
            }
        }
    }
}
