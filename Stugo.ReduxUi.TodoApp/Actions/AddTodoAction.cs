using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Actions
{
    class AddTodoAction
    {
        public AddTodoAction(Todo todo)
        {
            Todo = todo;
        }

        public Todo Todo { get; }
    }
}
