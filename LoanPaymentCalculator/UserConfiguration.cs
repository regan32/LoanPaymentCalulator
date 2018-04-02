namespace LoanPaymentCalculator
{
    using System;
    using System.Collections.Generic;
    using Guards;

    internal class UserConfiguration : Dictionary<string, string>
    {
        public int Amount => ParseDigit(nameof(Amount).ToLower());
        public double Interest => ParsePercent(nameof(Interest).ToLower());
        public int DownPayment => ParseDigit(nameof(DownPayment).ToLower());
        public int Term => ParseDigit(nameof(Term).ToLower());
        public int TermInMonths => Term * 12;

        public static string HelpString => "Arguments are expected in the following format:" +
                                           $"\r\n amount: {{0 - {int.MaxValue}}}" +
                                           $"\r\n interest: {{5.5% or 0.055}}" +
                                           $"\r\n downpayment: {{0 - {int.MaxValue}}}" +
                                           $"\r\n term: {{0 - {int.MaxValue}}}";

        public static UserConfiguration CreateFromArgs(string[] args)
        {
            Guard.ArgumentNotNull(args, nameof(args));

            var lines = string.Join("", args)
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var configuration = new UserConfiguration();

            foreach (var line in lines)
            {
                var keyValue = line.Split(':');

                if (keyValue.Length == 2)
                {
                    configuration[keyValue[0].Trim().ToLower()] = keyValue[1].Trim();
                }
            }

            return configuration;
        }

        private string GetString(string key)
        {
            string value;
            if (!TryGetValue(key, out value))
            {
                throw new Exception($"Agrument line '{key}: value' was expected. ");
            }

            return value;
        }

        private int ParseDigit(string key)
        {
            string value = GetString(key);

            int digit;
            if (!int.TryParse(value, out digit))
            {
                throw new Exception($"Digit was expeted as value for '{key}: value'. Example: {key}: {{0-{int.MaxValue}}} ");
            }

            return digit;
        }

        private double ParsePercent(string key)
        {
            string value = GetString(key);

            double multiplier = 1;
            if (value.Contains("%"))
            {
                value = value.Replace("%", string.Empty);
                multiplier = 0.01;
            }

            double digit;
            if (!double.TryParse(value.Replace('.', ','), out digit))
            {
                throw new Exception($"Percent or float was expeted as value for '{key}: value'. Example: '{key}: 5.5%' or '{key}: 0.055' ");
            }

            return digit * multiplier;
        }
    }
}