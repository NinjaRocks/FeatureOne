﻿using FeatureOn.Toggle.Conditions;

namespace FeatureOn.Test.Toggles
{
    [TestFixture]
    public sealed class SimpleConditionTest
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Evaluate_returns_IsEnabled(bool isEnabled)
        {
            var toggle = new SimpleCondition { IsEnabled= isEnabled };
            Assert.That(toggle.Evaluate(null), Is.EqualTo(isEnabled));
        }
    }
}
