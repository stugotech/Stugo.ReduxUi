using System.Windows;
using Stugo.ReduxUi.Middleware;

namespace Stugo.ReduxUi.TodoApp.Middleware
{
    class UpdateUiMiddleware<TState, TEffect, TActionBase> : StateChangedMiddleware<TState, TEffect, TActionBase>
    {
        private readonly FrameworkElement element;


        public UpdateUiMiddleware(FrameworkElement element)
        {
            this.element = element;
        }


        protected override void OnStateChanged(TState initialState, TState newState)
        {
            element.DataContext = newState;
        }
    }
}
