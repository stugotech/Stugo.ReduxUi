namespace Stugo.ReduxUi.TodoApp.State
{
    class TodoAppRoot
    {
        public TodoAppRoot()
            : this(new TodoCollection(), null)
        {
        }


        public TodoAppRoot(TodoCollection todoItems, Todo selectedTodo)
        {
            TodoItems = todoItems;
            SelectedTodo = selectedTodo;
        }

        
        public TodoCollection TodoItems { get; }
        public Todo SelectedTodo { get; }
    }
}
