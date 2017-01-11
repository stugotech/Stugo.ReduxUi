using System;
using Moq;
using Stugo.ReduxUi.Middleware;
using Stugo.ReduxUi.State;
using Xunit;

namespace Stugo.ReduxUi.Test
{
    public class StateChangedMiddlewareTest
    {
        [Fact]
        public void It_calls_callback_if_state_changes()
        {
            var initialState = new object();
            var newState = new object();

            var callbackMock = new Mock<StateChangedMiddleware<object, object, object>.CallbackDelegate>();
            var middleware = new StateChangedMiddleware<object, object, object>(callbackMock.Object);

            ReducerDelegate<object, object, object> reducer =
                (state, action) => newState.WithEffects(new object[0]);

            var chain = middleware.Chain(reducer);
            chain(initialState, new object());

            callbackMock.Verify(x => x(
                    It.Is<object>(p => ReferenceEquals(p, initialState)),
                    It.Is<object>(p => ReferenceEquals(p, newState))
                ), Times.Once);
        }


        [Fact]
        public void It_doesnt_call_callback_if_state_doesnt_change()
        {
            var initialState = new object();

            var callbackMock = new Mock<StateChangedMiddleware<object, object, object>.CallbackDelegate>();
            var middleware = new StateChangedMiddleware<object, object, object>(callbackMock.Object);

            ReducerDelegate<object, object, object> reducer =
                (state, action) => state.WithEffects(new object[0]);

            var chain = middleware.Chain(reducer);
            chain(initialState, new object());

            callbackMock.Verify(x => x(
                    It.IsAny<object>(),
                    It.IsAny<object>()
                ), Times.Never());
        }



        [Fact]
        public void It_calls_OnStateChanged_if_state_changes_and_no_callback_specified()
        {
            var initialState = new object();
            var newState = new object();

            var middleware = new TestMiddleware();

            ReducerDelegate<object, object, object> reducer =
                (state, action) => newState.WithEffects(new object[0]);

            var chain = middleware.Chain(reducer);
            chain(initialState, new object());

            Assert.Equal(1, middleware.OnStateChangedCallCount);
        }



        [Fact]
        public void It_throws_NotImplementedException_if_no_callback_implented()
        {
            var initialState = new object();
            var newState = new object();

            var middleware = new TestMiddlewareNoCallback();

            ReducerDelegate<object, object, object> reducer =
                (state, action) => newState.WithEffects(new object[0]);

            var chain = middleware.Chain(reducer);
            Assert.Throws<NotImplementedException>(() => chain(initialState, new object()));
        }



        class TestMiddleware : StateChangedMiddleware<object, object, object>
        {
            public int OnStateChangedCallCount { get; private set; }


            protected override void OnStateChanged(object initialState, object newState)
            {
                ++OnStateChangedCallCount;
            }
        }


        class TestMiddlewareNoCallback : StateChangedMiddleware<object, object, object> { }
    }
}
