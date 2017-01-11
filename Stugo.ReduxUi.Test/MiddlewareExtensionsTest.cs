using Stugo.ReduxUi.Middleware;
using Stugo.ReduxUi.State;
using Xunit;

namespace Stugo.ReduxUi.Test
{
    public class MiddlewareExtensionsTest
    {
        [Fact]
        public void Chain_calls_first_first()
        {
            MiddlewareDelegate<string, object, object> first =
                reduce => (state, a) => (reduce(state, a).State + "1").WithEffects(new object[0]);

            MiddlewareDelegate<string, object, object> second =
                reduce => (state, a) => (reduce(state, a).State + "2").WithEffects(new object[0]);

            var chain = first.Chain(second)((s, a) => "".WithEffects(new object[0]));
            var result = chain(null, null);

            Assert.Equal("12", result.State);
        }


        [Fact]
        public void Chain_array_calls_middleware_in_order()
        {
            MiddlewareDelegate<string, object, object> first =
                reduce => (state, a) => (reduce(state, a).State + "1").WithEffects(new object[0]);

            MiddlewareDelegate<string, object, object> second =
                reduce => (state, a) => (reduce(state, a).State + "2").WithEffects(new object[0]);

            MiddlewareDelegate<string, object, object> third =
                reduce => (state, a) => (reduce(state, a).State + "3").WithEffects(new object[0]);

            MiddlewareDelegate<string, object, object> fourth =
                reduce => (state, a) => (reduce(state, a).State + "4").WithEffects(new object[0]);

            MiddlewareDelegate<string, object, object> fifth =
                reduce => (state, a) => (reduce(state, a).State + "5").WithEffects(new object[0]);

            var chain = new [] {first, second, third, fourth, fifth}
                .Chain()((s, a) => "".WithEffects(new object[0]));

            var result = chain(null, null);

            Assert.Equal("12345", result.State);
        }


        [Fact]
        public void Chain_empty_array_returns_identity()
        {
            var middleware = new MiddlewareDelegate<string, object, object>[0];
            ReducerDelegate<string, object, object> reducer = (s, a) => s.WithEffects(new object[0]);

            var result = middleware.Chain()(reducer);

            Assert.Same(reducer, result);
        }
    }
}
