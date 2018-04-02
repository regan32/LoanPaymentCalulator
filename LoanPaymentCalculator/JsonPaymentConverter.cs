namespace LoanPaymentCalculator
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public sealed class JsonPaymentConverter : JsonConverter<Payment>
    {
        public override void WriteJson(JsonWriter writer, Payment value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var obj = new JObject
            {
                new JProperty("monthly payment", value.MonthlyPaiment),
                new JProperty("total interest", value.TotalInterest),
                new JProperty("total payment", value.TotalPayment)
            };

            obj.WriteTo(writer);
        }

        public override Payment ReadJson(JsonReader reader, Type objectType, Payment existingValue, bool hasExistingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}