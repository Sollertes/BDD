using Shouldly;
using Sollertes.Bdd.Scenarios;

namespace Sollertes.Bdd.Shouldly
{
    public class ShouldlyThenBuilder<TFixture>
    {
        private readonly IThenContinuationBuilder<TFixture> _thenContinuationBuilder;

        public ShouldlyThenBuilder(IThenBuilder<TFixture> thenBuilder)
        {
            _thenContinuationBuilder = thenBuilder.GetContinuationBuilder();
        }

        public ShouldlyThenBuilder(IThenContinuationBuilder<TFixture> thenContinuationBuilder)
        {
            _thenContinuationBuilder = thenContinuationBuilder;
        }

        public IThenContinuationBuilder<TFixture> Throws<TException>()
        {
            return _thenContinuationBuilder.And((f, e) => e.ShouldBeAssignableTo<TException>());
        }

        public IThenContinuationBuilder<TFixture> ThrowsExactly<TException>()
        {
            return _thenContinuationBuilder.And((f, e) => e.ShouldBeOfType<TException>());
        }
    }
}