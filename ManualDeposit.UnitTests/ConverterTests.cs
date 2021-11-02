using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using Tgs.Payment.ManualDeposit;
using Tgs.Payment.ManualDeposit.Converters;

namespace ManualDeposit.UnitTests
{
    public class ConverterTests
    {    
        [Test]
        public void Deserialize_Deposit_Bank_ApplyInfo_Data_Should_Success()
        {            
            var json = "{\"sendBankAccount\":\"sendBankAccount\",\"ReceiveBankAccount\":\"brandPaymentInfo.BankAccount\",\"ReceiveAccountName\":\"brandPaymentInfo.AccountName\",\"bankType\":\"Bank\"}";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters = {
                    new ManualDepositApplyInfoJsonConverter()
                }
            };

            var actual = JsonSerializer.Deserialize<ManualDepositBankApplyInfo>(json, options);            
        }
        [Test]
        public void Deserialize_Deposit_Third_Party_ApplyInfo_Data_Should_Success()
        {            
            var json = "{\"qrCode\":\"https://www.thirdParty/qrCode\",\"bankType\":\"ThirdParty\"}";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters = {
                    new ManualDepositApplyInfoJsonConverter()
                }
            };            

            var actual = JsonSerializer.Deserialize<ManualDepositThirdPartyApplyInfo>(json, options);

            Assert.AreEqual("ThirdParty", actual.BankType);
        }
    }
}