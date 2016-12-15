using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Reducers
{
    static class TodoAppReducer
    {
        public static TodoAppRoot Reduce(this TodoAppRoot initialState, object action)
        {
            var items = initialState.TodoItems.Reduce(action);
            Todo selectedItem = null;

            if (action is SelectTodoAction)
                selectedItem = ((SelectTodoAction)action).SelectedTodo;

            return new TodoAppRoot(items, selectedItem);
        }
    }
}
