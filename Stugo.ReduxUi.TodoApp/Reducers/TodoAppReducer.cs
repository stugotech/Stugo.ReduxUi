using Stugo.ReduxUi.State;
using Stugo.ReduxUi.State.Convention;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Reducers
{
    class TodoAppReducer
    {
        public static TodoAppRoot Reduce(TodoAppRoot initialState, object action)
        {
            initialState = new TodoAppRoot(
                TodoCollectionReducer.Reduce(initialState.TodoItems, action), 
                initialState.SelectedTodo
            );

            return ConventionReducer<Reducers>.Reduce(initialState, action);
        }


        public class Reducers
        {
            public static TodoAppRoot Reduce(TodoAppRoot initialState, SelectTodoAction action)
            {
                return new TodoAppRoot(initialState.TodoItems, action.SelectedTodo);
            }


            public static TodoAppRoot Reduce(TodoAppRoot initialState, object action)
            {
                // clear selected item for all other actions
                return new TodoAppRoot(initialState.TodoItems, null);
            }
        }
    }
}
