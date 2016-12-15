using System.Collections;
using System.Collections.Generic;

namespace Stugo.ReduxUi.TodoApp.State
{
    class TodoCollection : IEnumerable<Todo>
    {
        private readonly Todo[] items;


        public TodoCollection()
            : this(new Todo[0])
        {
        }


        public int Length => items.Length;
        

        public TodoCollection(Todo[] todos)
        {
            items = todos;
        }


        public Todo[] ToArray()
        {
            return items;
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
