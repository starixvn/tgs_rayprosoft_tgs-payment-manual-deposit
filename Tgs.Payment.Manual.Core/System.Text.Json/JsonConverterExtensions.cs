using System.Collections.Generic;
using System.Text.Json.Serialization;
using Tgs.Payment.ManualDeposit.Converters;

namespace System.Text.Json
{
    public static class JsonConverterExtensions
    {
        public static void AddManualDepositConverters(this IList<JsonConverter> converters)
        {
            converters.Add(new ManualDepositApplyInfoJsonConverter());
        }
    }
}