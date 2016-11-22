using Stugo.ReduxUi.State;
using Stugo.ReduxUi.State.Convention;
using Stugo.ReduxUi.TodoApp.Actions;

namespace Stugo.ReduxUi.TodoApp.State
{
    class TodoAppRoot : IReduxState<TodoAppRoot, object>
    {
        private readonly IStateMutator<TodoAppRoot, object> mutator;


        public TodoAppRoot()
            : this(new TodoCollection(), null)
        {
        }


        public TodoAppRoot(TodoCollection todoItems, Todo selectedTodo)
        {
            mutator = new ConventionStateMutator<TodoAppRoot, object>();
            TodoItems = todoItems;
            SelectedTodo = selectedTodo;
        }

        
        public TodoCollection TodoItems { get; }
        public Todo SelectedTodo { get; }


        public TodoAppRoot SelectTodo(SelectTodoAction action)
        {
            return new TodoAppRoot(TodoItems.ApplyAction(action), action.SelectedTodo);
        }


        public TodoAppRoot ApplyAction(object action)
        {
            var state = mutator.ApplyAction(this, action);
            // forward mutation to TodoCollection if not processed locally
            return state == this ? new TodoAppRoot(TodoItems.ApplyAction(action), null) : state;
        }
    }
}
