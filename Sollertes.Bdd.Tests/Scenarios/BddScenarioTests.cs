﻿using System;
using Moq;
using Shouldly;
using Sollertes.Bdd.Scenarios;
using Xunit;

namespace Sollertes.Bdd.Tests.Scenarios
{
    public class BddScenarioTests
    {
        [Fact]
        public void AllGivenActionsAreInvokedOnce()
        {
            var fixtureMock = new Mock<IFixture>();

            TestScenarion(fixtureMock.Object);

            fixtureMock.Verify(f => f.FirstFact(), Times.Once);
            fixtureMock.Verify(f => f.SecondFact(), Times.Once);
        }

        [Fact]
        public void GivenActionsAreNotRequired()
        {
            var fixtureMock = new Mock<IFixture>();
            BddScenario
                .Given(fixtureMock.Object)
                .GivenNoAction()
                .When(f => f.SomethingIsDone())
                .Then(f => f.Result1IsAsExpected())
                .Test();
        }

        [Fact]
        public void WhenActionIsInvokedOnce()
        {
            var fixtureMock = new Mock<IFixture>();

            TestScenarion(fixtureMock.Object);

            fixtureMock.Verify(f => f.SomethingIsDone(), Times.Once);
        }

        [Fact]
        public void CreateSutIsInvokedOnceForISutCreator()
        {
            var fixtureMock = new Mock<IFixture>();

            TestScenarion(fixtureMock.Object);

            fixtureMock.Verify(f => f.CreateSut(), Times.Once);
        }

        [Fact]
        public void AllThenActionsAreInvokedOnce()
        {
            var fixtureMock = new Mock<IFixture>();

            TestScenarion(fixtureMock.Object);

            fixtureMock.Verify(f => f.Result1IsAsExpected(), Times.Once);
            fixtureMock.Verify(f => f.Result2IsAsExpected(), Times.Once);
        }

        [Fact]
        public void AllThenActionsAreInvokedEvenIfExceptionWasThrown()
        {
            var fixtureMock = new Mock<IFixture>();
            fixtureMock.Setup(f => f.SomethingIsDone()).Throws<Exception>();

            TestScenarion(fixtureMock.Object);

            fixtureMock.Verify(f => f.Result1IsAsExpected(), Times.Once);
            fixtureMock.Verify(f => f.Result2IsAsExpected(), Times.Once);
        }

        [Fact]
        public void SecondThenActionIsInvokedEvenIfFirstAssertFailed()
        {
            var fixtureMock = new Mock<IFixture>();
            fixtureMock.Setup(f => f.Result1IsAsExpected()).Throws<Exception>();

            void Test() => TestScenarion(fixtureMock.Object);

            Assert.Throws<AggregateAssertException>((Action)Test);
            fixtureMock.Verify(f => f.Result1IsAsExpected(), Times.Once);
            fixtureMock.Verify(f => f.Result2IsAsExpected(), Times.Once);
        }

        [Fact]
        public void DefaultTitleIsEqualToHumanizedMethodName()
        {
            var fixtureMock = new Mock<IFixture>();
            IBddScenario scenario = BddScenario
                .Given(fixtureMock.Object)
                .Given(f => f.FirstFact())
                    .And(f => f.SecondFact())
                .When(f => f.SomethingIsDone())
                .Then(f => f.Result1IsAsExpected())
                    .And(f => f.Result2IsAsExpected())
                .Create();
            
            scenario.GetDescription().Title.ShouldBe("Default title is equal to humanized method name");
        }

        [Fact]
        public void CustomTitleIsCorrect()
        {
            var fixtureMock = new Mock<IFixture>();
            const string title = "Custom title";
            IBddScenario scenario = CreateScenario(fixtureMock.Object, title);

            scenario.GetDescription().Title.ShouldBe(title);
        }

        [Fact]
        public void GivenSectionTextIsCorrect()
        {
            var fixtureMock = new Mock<IFixture>();
            IBddScenario scenario = CreateScenario(fixtureMock.Object);

            scenario.GetDescription().Given.ShouldBe($"Given first fact{Environment.NewLine}\tAnd second fact");
        }

        [Fact]
        public void WhenSectionTextIsCorrect()
        {
            var fixtureMock = new Mock<IFixture>();
            IBddScenario scenario = CreateScenario(fixtureMock.Object);

            scenario.GetDescription().When.ShouldBe("When something is done");
        }

        [Fact]
        public void ThenSectionTextIsCorrect()
        {
            var fixtureMock = new Mock<IFixture>();
            IBddScenario scenario = CreateScenario(fixtureMock.Object);

            scenario.GetDescription().Then.ShouldBe($"Then result 1 is as expected{Environment.NewLine}\tAnd result 2 is as expected");
        }

        [Fact]
        public void DescriptionIsCorrectlyComposedFromSections()
        {
            var fixtureMock = new Mock<IFixture>();
            IBddScenario scenario = CreateScenario(fixtureMock.Object);

            IBddScenarioDescription description = scenario.GetDescription();
            string newLine = Environment.NewLine;
            description.ToString().ShouldBe($"{description.Title}{newLine}{description.Given}{newLine}{description.When}{newLine}{description.Then}{newLine}");
        }

        private static void TestScenarion(IFixture fixture)
        {
            CreateScenario(fixture).Test();
        }

        private static IBddScenario CreateScenario(IFixture fixture, string title = null)
        {
            return BddScenario
                .Title(title)
                .Given(fixture)
                .Given(f => f.FirstFact())
                    .And(f => f.SecondFact())
                .When(f => f.SomethingIsDone())
                .Then(f => f.Result1IsAsExpected())
                    .And(f => f.Result2IsAsExpected())
                .Create();
        }

        // ReSharper disable once MemberCanBePrivate.Global - required by Moq
        public interface IFixture : ISutCreator
        {
            void FirstFact();
            void SecondFact();

            void SomethingIsDone();

            void Result1IsAsExpected();
            void Result2IsAsExpected();
        }
    }
}