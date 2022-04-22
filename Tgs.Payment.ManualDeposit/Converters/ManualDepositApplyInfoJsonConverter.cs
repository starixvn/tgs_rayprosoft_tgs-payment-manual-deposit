using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tgs.Payment.ManualDeposit.Converters
{
    public class ManualDepositApplyInfoJsonConverter : JsonConverter<ManualDepositApplyInfo>
    {
        public override ManualDepositApplyInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonDoc = JsonDocument.ParseValue(ref reader);

            var bankType = jsonDoc.RootElement.GetProperty("bankType").GetString();

            using var ms = new MemoryStream();

            var writer = new Utf8JsonWriter(ms, new JsonWriterOptions { Indented = false });
            jsonDoc.WriteTo(writer);
            writer.Flush();

            var json = Encoding.UTF8.GetString(ms.ToArray());

            return bankType switch
            {
                "Bank" => JsonSerializer.Deserialize<ManualDepositBankApplyInfo>(json, options),
                "ThirdParty" => JsonSerializer.Deserialize<ManualDepositThirdPartyApplyInfo>(json, options),
                "Usdt" => JsonSerializer.Deserialize<ManualDepositUsdtApplyInfo>(json, options),
                "FPS" => JsonSerializer.Deserialize<ManualDepositFpsApplyInfo>(json, options),
                _ => throw new NotImplementedException()
            };
        }

        public override void Write(Utf8JsonWriter writer, ManualDepositApplyInfo value, JsonSerializerOptions options)
        {
            var json = value switch
            {
                ManualDepositBankApplyInfo bankApplyInfo => JsonSerializer.Serialize(bankApplyInfo, options),
                ManualDepositThirdPartyApplyInfo thirdPartyApplyInfo => JsonSerializer.Serialize(thirdPartyApplyInfo, options),
                ManualDepositUsdtApplyInfo usdtApplyInfo => JsonSerializer.Serialize(usdtApplyInfo, options),
                ManualDepositFpsApplyInfo fpsApplyInfo => JsonSerializer.Serialize(fpsApplyInfo, options),
                _ => throw new NotImplementedException()
            };

            JsonDocument.Parse(json).WriteTo(writer);
        }
    }
}