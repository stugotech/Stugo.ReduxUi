using System;

namespace Stugo.ReduxUi.TodoApp.State
{
    class Todo
    {
        public Todo(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
        }

        public Guid Id { get; }
        public string Text { get; }
    }
}
