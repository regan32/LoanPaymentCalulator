namespace LoanPaymentCalculator
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings(){Converters = new List<JsonConverter>(){new JsonPaymentConverter()}};

            if (args.Length == 0)
            {
                Console.WriteLine(UserConfiguration.HelpString);

                return;
            }

            try
            {
                var configuration = UserConfiguration.CreateFromArgs(args);
                var payment = Calculator.Calulate(configuration.Amount, configuration.Interest, configuration.DownPayment, configuration.TermInMonths);

                Console.WriteLine();
                Console.Write(JsonConvert.SerializeObject(payment).ToFormattedJson());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
