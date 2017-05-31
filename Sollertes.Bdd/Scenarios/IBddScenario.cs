namespace Sollertes.Bdd.Scenarios
{
    public interface IBddScenario
    {
        IBddScenarioDescription GetDescription();
        void Test();
    }
}