using System.Collections;
using System.Collections.Generic;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.State.Convention;
using Stugo.ReduxUi.TodoApp.Actions;

namespace Stugo.ReduxUi.TodoApp.State
{
    class TodoCollection : IEnumerable<Todo>, IReduxState<TodoCollection, object>
    {
        private readonly IStateMutator<TodoCollection, object> convention;
        private readonly Todo[] items;


        public TodoCollection()
            : this(new Todo[0])
        {
        }


        public int Length => items.Length;
        

        public TodoCollection(Todo[] todos)
        {
            this.convention = new ConventionStateMutator<TodoCollection, object>();
            items = todos;
        }


        public TodoCollection AddTodo(AddTodoAction action)
        {
            return new TodoCollection(items.Append(action.Todo));
        }


        public TodoCollection UpdateTodo(UpdateTodoAction action)
        {
            return new TodoCollection(items.Update(x => x == action.Todo ? new Todo(action.Text) : x));
        }


        public TodoCollection DeleteTodo(DeleteTodoAction action)
        {
            return new TodoCollection(items.Remove(action.Todo));
        }


        public TodoCollection ApplyAction(object action)
        {
            return convention.ApplyAction(this, action);
        }


        public IEnumerator<Todo> GetEnumerator()
        {
            return ((IEnumerable<Todo>)items).GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
