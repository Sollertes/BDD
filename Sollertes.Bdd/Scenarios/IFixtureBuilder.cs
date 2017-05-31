namespace Sollertes.Bdd.Scenarios
{
    public interface IFixtureBuilder
    {
        IGivenBuilder<TFixture> Given<TFixture>() where TFixture : class, new();
        IGivenBuilder<TFixture> Given<TFixture>(TFixture fixture);
    }
}