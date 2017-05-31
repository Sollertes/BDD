using Sollertes.Bdd.Scenarios;

namespace Sollertes.Bdd.Shouldly
{
    public static class ThenBuilderExtensions
    {
        public static ShouldlyThenBuilder<TFixture> Then<TFixture>(this IThenBuilder<TFixture> thenBuilder)
        {
            return new ShouldlyThenBuilder<TFixture>(thenBuilder);
        }

        public static ShouldlyThenBuilder<TFixture> And<TFixture>(this IThenContinuationBuilder<TFixture> thenBuilder)
        {
            return new ShouldlyThenBuilder<TFixture>(thenBuilder);
        }
    }
}
