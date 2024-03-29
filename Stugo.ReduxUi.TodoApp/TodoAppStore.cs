﻿using System.Threading;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.Reducers;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp
{
    class TodoAppStore : ReduxStore<TodoAppRoot, object>
    {
        public TodoAppStore(SynchronizationContext synchronizationContext)
            : base(synchronizationContext, TodoAppReducer.Reduce)
        {
        }
    }
}
