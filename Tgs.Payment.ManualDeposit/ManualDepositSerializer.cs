using System.Text.Json;
using Tgs.Payment.ManualDeposit.Converters;

namespace TGS.Payment.ManualDeposit
{
    public class ManualDepositSerializer
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            Converters = {
                new ManualDepositApplyInfoJsonConverter()
            }
        };

        public string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, _jsonSerializerOptions);
        }

        public T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        }
    }
}
