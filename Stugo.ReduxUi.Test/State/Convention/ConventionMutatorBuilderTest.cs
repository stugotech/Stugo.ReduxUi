using System;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.State.Convention;
using Xunit;

namespace Stugo.ReduxUi.Test.State.Convention
{
    public class ConventionMutatorBuilderTest
    {
        [Fact]
        public void TryBuildMutator_creates_a_mutator_function()
        {
            var state = new StringState("hello ");
            var method = ((Func<AppendAction, StringState>)state.Append).Method;
            StateMutatorDelegate<StringState, IAction> mutator;

            var actionType = ConventionMutatorBuilder.TryBuildMutator(method, out mutator);

            Assert.Equal(typeof(AppendAction), actionType);

            var result = mutator(state, new AppendAction("world"));
            Assert.Equal("hello world", result.Text);
        }


        [Fact]
        public void GetMutators_returns_all_valid_mutators()
        {
            var mutators = ConventionMutatorBuilder.GetMutators<StringState, IAction>();

            Assert.Equal(2, mutators.Count);
            Assert.True(mutators.ContainsKey(typeof(AppendAction)));
            Assert.True(mutators.ContainsKey(typeof(SurroundAction)));

            var state = new StringState("hello");

            Assert.Equal("hello world", mutators[typeof(AppendAction)](state, new AppendAction(" world")).Text);
            Assert.Equal("..hello..", mutators[typeof(SurroundAction)](state, new SurroundAction("..")).Text);
        }


        interface IAction { }

        class AppendAction : IAction
        {
            public AppendAction(string appendText)
            {
                AppendText = appendText;
            }

            public string AppendText { get; }
        }


        class SurroundAction : IAction
        {
            public SurroundAction(string surroundText)
            {
                SurroundText = surroundText;
            }


            public string SurroundText { get; }
        }


        class StringState
        {
            public StringState(string text)
            {
                Text = text;
            }


            public string Text { get; }


            public StringState Append(AppendAction action)
            {
                return new StringState(Text + action.AppendText);
            }


            public StringState Surround(SurroundAction action)
            {
                return new StringState(action.SurroundText + Text + action.SurroundText);
            }


            public string GetText(AppendAction action)
            {
                return action.AppendText;
            }

            public StringState Append(string a, string b)
            {
                return new StringState(Text + a + b);
            }

            public StringState Append(int a)
            {
                return new StringState(Text + a);
            }
        }
    }
}
