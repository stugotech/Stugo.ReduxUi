using System;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Reducers
{
    static class TodoCollectionReducer
    {
        public static TodoCollection Reduce(this TodoCollection initialState, object action)
        {
            if (action is AddTodoAction)
                return initialState.Add((AddTodoAction)action);

            if (action is UpdateTodoAction)
                return initialState.Update((UpdateTodoAction)action);

            if (action is DeleteTodoAction)
                return initialState.Delete((DeleteTodoAction)action);

            return initialState;
        }


        public static TodoCollection Add(this TodoCollection collection, AddTodoAction action)
        {
            return new TodoCollection(collection.ToArray().Append(action.Todo));
        }


        public static TodoCollection Update(this TodoCollection collection, UpdateTodoAction action)
        {
            return new TodoCollection(
                collection.ToArray().Update(x => x == action.Todo ? new Todo(action.Text) : x)
            );
        }


        public static TodoCollection Delete(this TodoCollection collection, DeleteTodoAction action)
        {
            return new TodoCollection(collection.ToArray().Remove(action.Todo));
        }
    }
}
