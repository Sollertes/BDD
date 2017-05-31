using System;
using System.Linq.Expressions;

namespace Sollertes.Bdd.Scenarios
{
    public interface IGivenContinuationBuilder<TFixture> : IWhenBuilder<TFixture>
    {
        IGivenContinuationBuilder<TFixture> And(Expression<Action<TFixture>> givenAction);
        IGivenContinuationBuilder<TFixture> And(Action<TFixture> givenAction, string name);
    }
}