using System.Collections.Generic;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.Effects;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Reducers
{
    static class TodoAppReducer
    {
        public static IReducerResult<TodoAppRoot, object> Reduce(this TodoAppRoot initialState, object action)
        {
            var effects = new List<object>();
            var items = initialState.TodoItems.Reduce(action);
            Todo selectedItem = null;

            if (action is SelectTodoAction)
                selectedItem = ((SelectTodoAction)action).SelectedTodo;
            else if (action is RollDiceAction)
                effects.Add(new RollDiceEffect());

            return new TodoAppRoot(items, selectedItem).WithEffects(effects.ToArray());
        }
    }
}
