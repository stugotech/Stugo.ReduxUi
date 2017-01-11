using System;
using System.Threading.Tasks;
using Stugo.ReduxUi.Middleware;
using Stugo.ReduxUi.TodoApp.Actions;
using Stugo.ReduxUi.TodoApp.Effects;
using Stugo.ReduxUi.TodoApp.State;

namespace Stugo.ReduxUi.TodoApp.Middleware
{
    class RollDiceMiddleware : HandleEffectsMiddleware<TodoAppStore, object, object>
    {
        private readonly IReduxDispatcher<object> dispatcher;

        public RollDiceMiddleware(IReduxDispatcher<object> dispatcher)
        {
            this.dispatcher = dispatcher;
        }


        protected override void OnEffect(object effect)
        {
            if (effect is RollDiceEffect)
                Roll();
        }


        private async void Roll()
        {
            await Task.Delay(1000);
            var roll = new Random().Next(1, 6);
            dispatcher.Dispatch(new AddTodoAction(new Todo(roll.ToString())));
        }
    }
}
