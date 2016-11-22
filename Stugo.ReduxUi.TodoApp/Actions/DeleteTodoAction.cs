using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Actions
{
    class DeleteTodoAction
    {
        public DeleteTodoAction(Todo todo)
        {
            Todo = todo;
        }

        public Todo Todo { get; }
    }
}
