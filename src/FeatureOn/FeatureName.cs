﻿using System.Text.RegularExpressions;

namespace Ninja.FeatureIt
{
    public class FeatureName
    {
        public FeatureName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("feature name can't be empty or null");
            if (!Regex.IsMatch(name, @"^\w+([\w\-]+)?$", RegexOptions.None, Constants.DefaultRegExTimeout))
                throw new ArgumentException($"invalid feature name '{name}'");

            Value = name;
        }
        public string Value { get; }
       
        public static implicit operator string(FeatureName s) => s?.Value;
    }
}