using System.Windows.Controls;
using System.Windows.Input;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Views
{
    /// <summary>
    /// Interaction logic for EditTodoView.xaml
    /// </summary>
    public partial class EditTodoView : UserControl
    {
        private TodoAppStore _store;
        private TodoAppStore Store => _store ?? (_store = this.GetTodoAppStore());


        public EditTodoView()
        {
            InitializeComponent();
        }


        private void TodoTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Store.State.SelectedTodo == null)
                {
                    var action = new AddTodoAction(new Todo(todoTextBox.Text));
                    Store.Dispatch(action);
                }
                else
                {
                    var action = new UpdateTodoAction(Store.State.SelectedTodo, todoTextBox.Text);
                    Store.Dispatch(action);
                }
            }
        }
    }
}
