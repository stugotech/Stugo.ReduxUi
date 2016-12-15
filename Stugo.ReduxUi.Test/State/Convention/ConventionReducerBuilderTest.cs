using System;
using Stugo.ReduxUi.State;
using Stugo.ReduxUi.State.Convention;
using Xunit;

namespace Stugo.ReduxUi.Test.State.Convention
{
    public class ConventionReducerBuilderTest
    {
        [Fact]
        public void TryBuildMutator_creates_a_mutator_function()
        {
            var state = new StringState("hello ");
            var method = ((ReducerDelegate<StringState, AppendAction>)StringStateReducer.Append).Method;
            ReducerDelegate<StringState, IAction> mutator;

            var actionType = ConventionReducerBuilder.TryBuildReducer(method, out mutator);

            Assert.Equal(typeof(AppendAction), actionType);

            var result = mutator(state, new AppendAction("world"));
            Assert.Equal("hello world", result.Text);
        }


        [Fact]
        public void GetMutators_returns_all_valid_mutators()
        {
            var mutators = ConventionReducerBuilder.GetReducers<StringStateReducer, StringState, IAction>();

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
        }


        class StringStateReducer
        {
            public static StringState Append(StringState initialState, AppendAction action)
            {
                return new StringState(initialState.Text + action.AppendText);
            }


            public static StringState Surround(StringState initialState, SurroundAction action)
            {
                return new StringState(action.SurroundText + initialState.Text + action.SurroundText);
            }


            public static object WrongReturnType(StringState initialState, SurroundAction action)
            {
                return null;
            }


            public static StringState WrongActionType(StringState initialState, object action)
            {
                return null;
            }


            public static StringState WrongStateType(object initialState, SurroundAction action)
            {
                return null;
            }


            public static StringState SomethingElse()
            {
                return null;
            }
        }
    }
}
