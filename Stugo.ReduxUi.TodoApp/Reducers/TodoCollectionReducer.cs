using Stugo.ReduxUi.State;
using Stugo.ReduxUi.State.Convention;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Reducers
{
    static class TodoCollectionReducer
    {
        public static TodoCollection Reduce(TodoCollection initialState, object action)
        {
            return ConventionReducer<Reducers>.Reduce(initialState, action);
        }


        public class Reducers
        {
            public static TodoCollection AddTodo(TodoCollection initialState, AddTodoAction action)
            {
                return new TodoCollection(initialState.ToArray().Append(action.Todo));
            }


            public static TodoCollection UpdateTodo(TodoCollection initialState, UpdateTodoAction action)
            {
                return new TodoCollection(initialState.ToArray().Update(x => x == action.Todo ? new Todo(action.Text) : x));
            }


            public static TodoCollection DeleteTodo(TodoCollection initialState, DeleteTodoAction action)
            {
                return new TodoCollection(initialState.ToArray().Remove(action.Todo));
            }
        }
    }
}
