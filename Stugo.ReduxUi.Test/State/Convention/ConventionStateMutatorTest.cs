using Moq;
using Stugo.ReduxUi.State.Convention;
using Xunit;

namespace Stugo.ReduxUi.Test.State.Convention
{
    public class ConventionStateMutatorTest
    {
        [Fact]
        public void ApplyAction_calls_matching_method()
        {
            var mutator = new ConventionStateMutator<ITestState, object>();
            var stateMock = new Mock<ITestState>(MockBehavior.Strict);
            stateMock.Setup(x => x.Mutate(It.Is<string>(a => a == "hello"))).Returns(() => null);

            mutator.ApplyAction(stateMock.Object, "hello");

            stateMock.Verify(x => x.Mutate(It.Is<string>(a => a == "hello")), Times.Once);
        }


        [Fact]
        public void ApplyAction_does_not_call_method_with_base_type_with_handleBaseActionType_false()
        {
            var mutator = new ConventionStateMutator<ITestState, object>();
            var stateMock = new Mock<ITestState>(MockBehavior.Strict);
            mutator.ApplyAction(stateMock.Object, new object());
        }


        [Fact]
        public void ApplyAction_calls_method_with_base_type_with_handleBaseActionType_true()
        {
            var mutator = new ConventionStateMutator<ITestState, object>(true);
            var action = new object();
            var stateMock = new Mock<ITestState>(MockBehavior.Strict);
            stateMock.Setup(x => x.Mutate(It.Is<object>(a => object.ReferenceEquals(a, action)))).Returns(() => null);

            mutator.ApplyAction(stateMock.Object, action);

            stateMock.Verify(x => x.Mutate(It.Is<object>(a => object.ReferenceEquals(a, action))), Times.Once);
        }


        public interface ITestState
        {
            ITestState Mutate(string action);
            ITestState Mutate(object action);
        }
    }
}
