using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Actions
{
    class UpdateTodoAction
    {
        public UpdateTodoAction(Todo todo, string text)
        {
            Todo = todo;
            Text = text;
        }

        public Todo Todo { get; }
        public string Text { get; }
    }
}
