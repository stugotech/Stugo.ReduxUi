using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Actions
{
    class SelectTodoAction
    {
        public SelectTodoAction(Todo selectedTodo)
        {
            SelectedTodo = selectedTodo;
        }

        public Todo SelectedTodo { get; }
    }
}
