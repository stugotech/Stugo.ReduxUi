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
        public MainWindow()
        {
            InitializeComponent();
        }


        private void TodosListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Redux.GetStore(this).Dispatcher.Dispatch(new SelectTodoAction((Todo)todosListBox.SelectedItem));
        }


        private void TodosListBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            var store = Redux.GetStore(this);

            if (e.Key == Key.Delete && store.State.SelectedTodo != null)
            {
                store.Dispatcher.Dispatch(new DeleteTodoAction(store.State.SelectedTodo));
            }
        }
    }
}
