using Stugo.ReduxUi.State;
using Xunit;

namespace Stugo.ReduxUi.Test.State
{
    public class ReducerCollectionTest
    {
        [Fact]
        public void It_returns_a_mutator_registered_for_the_given_type()
        {
            var collection = new ReducerCollection<string, object>();
            collection.Add<A1>(MutateA1);
            collection.Add<A>(MutateA);

            var mutator = collection[typeof(A1)];

            Assert.NotNull(mutator);
            Assert.Equal("A1:test", mutator("test", null));
        }


        [Fact]
        public void It_returns_a_mutator_registered_for_a_less_derived_type()
        {
            var collection = new ReducerCollection<string, object>();
            collection.Add<A1>(MutateA1);
            collection.Add<A>(MutateA);

            var mutator = collection[typeof(A2)];

            Assert.NotNull(mutator);
            Assert.Equal("A:test", mutator("test", null));
        }


        [Fact]
        public void It_returns_null_if_no_mutator_found()
        {
            var collection = new ReducerCollection<string, object>();
            collection.Add<A1>(MutateA1);
            collection.Add<A>(MutateA);

            var mutator = collection[typeof(C)];

            Assert.Null(mutator);
        }


        private string MutateA1(string state, A1 action)
        {
            return "A1:"+state;
        }

        private string MutateA(string state, A action)
        {
            return "A:"+state;
        }


        class A { }
        class A1 : A { }
        class A2 : A { }
        class C { }
    }
}
