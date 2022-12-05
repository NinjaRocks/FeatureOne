using FeatureOn;
using FeatureOn.Toggle.Conditions;
using FeatureOn.Toggles;

namespace FeatureIt.Test
{
    [TestFixture]
    public sealed class FeatureTest
    {
        private Dictionary<string, string> claims;

        [SetUp]
        public void Setup()
        {
            claims = new Dictionary<string, string>();
        }


        [Test]
        public void TestFeatureForCorrectNameAndToggleCondition()
        {
            var feature = new Feature("feat-01", 
                    new FeatureToggle(FeatureOn.Toggle.ToggleOperator.All,
                        new[] { new RegexCondition { Claim = "email", Expression = "1234@ioHub.com" } }));

            Assert.IsInstanceOf<RegexCondition>(feature.Toggle.Conditions[0]);
            Assert.That(feature.Name.Value, Is.EqualTo("feat-01"));
        }

        [TestCase(true, true, true)]
        [TestCase(false, true, false)]
        [TestCase(true, false, false)]
        [TestCase(false, false, false)]
       
        public void TestFeatureForAllOperatorWhenEvaluatingMultileToggleCondition(bool condition1, bool condition2, bool expected)
        {
            claims.Clear();
            claims.Add("email", "kl12.sha123@ninja.com");
            claims.Add("User_role", "administration");

            var feature = new Feature("feat-01",
                    new FeatureToggle(FeatureOn.Toggle.ToggleOperator.All,
                        new[] { 
                            new SimpleCondition { IsEnabled = condition1},
                            new SimpleCondition { IsEnabled = condition2 }
                        }));

            var isEnabled = feature.IsEnabled(claims);
                        
            Assert.That(feature.Name.Value, Is.EqualTo("feat-01"));
            Assert.That(isEnabled, Is.EqualTo(expected));
        }

        [TestCase(true, true, true)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, false, false)]
        public void TestFeatureForAnyOperatorWhenEvaluatingMultileToggleCondition(bool condition1, bool condition2, bool expected)
        {
            claims.Clear();
            claims.Add("email", "kl12.sha123@ninja.com");
            claims.Add("User_role", "administration");

            var feature = new Feature("feat-01",
                    new FeatureToggle(FeatureOn.Toggle.ToggleOperator.Any,
                        new[] {
                            new SimpleCondition { IsEnabled = condition1},
                            new SimpleCondition { IsEnabled = condition2 }
                        }));

            var isEnabled = feature.IsEnabled(claims);

            Assert.That(feature.Name.Value, Is.EqualTo("feat-01"));
            Assert.That(isEnabled, Is.EqualTo(expected));
        }
    }
}
