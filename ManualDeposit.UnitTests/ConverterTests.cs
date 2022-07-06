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
            var json = "{\"sendBankAccount\":\"sendBankAccount\",\"sendNote\":\"sendNote\",\"ReceiveBankAccount\":\"brandPaymentInfo.BankAccount\",\"ReceiveAccountName\":\"brandPaymentInfo.AccountName\",\"bankType\":\"Bank\"}";

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
            Assert.AreEqual("Bank", actual.BankType);
            Assert.AreEqual("sendNote", actual.SendNote);
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

        [Test]
        public void Deserialize_Deposit_FPS_ApplyInfo_Data_Should_Success()
        {
            var json = "{\"qrCodeFileUrl\":\"https://www.thirdParty/qrCode\",\"bankType\":\"FPS\",\"phone\":\"88888888\",\"fpsId\":\"123456789\",\"email\":\"fps@test.com\",\"sendBankAccount\":\"1688888888\",\"sendBankName\":\"bankName\"}";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters = {
                    new ManualDepositApplyInfoJsonConverter()
                }
            };

            var actual = JsonSerializer.Deserialize<ManualDepositFpsApplyInfo>(json, options);

            Assert.AreEqual("FPS", actual.BankType);
            Assert.AreEqual("88888888", actual.Phone);
            Assert.AreEqual("123456789", actual.FpsId);
            Assert.AreEqual("https://www.thirdParty/qrCode", actual.QrCodeFileUrl);
            Assert.AreEqual("fps@test.com", actual.Email);
            Assert.AreEqual("1688888888", actual.SendBankAccount);
            Assert.AreEqual("bankName", actual.SendBankName);
        }

        [Test]
        public void Deserialize_Deposit_Usdt_ApplyInfo_Data_Should_Success()
        {
            var json = "{\"qrCode\":\"https://www.test.com/qrCode\",\"bankType\":\"Usdt\",\"address\":\"TCKCX5Khy9V96qvD5T5K8c6bzWXzMkawbw\",\"rate\":168,\"sendAmount\":1680}";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters = {
                    new ManualDepositApplyInfoJsonConverter()
                }
            };

            var actual = JsonSerializer.Deserialize<ManualDepositUsdtApplyInfo>(json, options);

            Assert.AreEqual("Usdt", actual.BankType);
            Assert.AreEqual("TCKCX5Khy9V96qvD5T5K8c6bzWXzMkawbw", actual.Address);
            Assert.AreEqual("https://www.test.com/qrCode", actual.QrCode);
            Assert.AreEqual(168m, actual.Rate);
            Assert.AreEqual(1680m, actual.SendAmount);
        }

        [Test]
        public void Deserialize_Deposit_EWallet_ApplyInfo_Data_Should_Success()
        {
            var json = "{\"qrCode\":\"https://www.test.com/qrCode\",\"eMoneyName\":\"TestEMoneyName\",\"phone\":\"11223344\",\"sendNote\":\"testPlayerNote\",\"bankType\":\"EWallet\"}";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters = {
                    new ManualDepositApplyInfoJsonConverter()
                }
            };

            var actual = JsonSerializer.Deserialize<ManualDepositEWalletApplyInfo>(json, options);

            Assert.AreEqual("EWallet", actual.BankType);
            Assert.AreEqual("https://www.test.com/qrCode", actual.QrCode);
            Assert.AreEqual("TestEMoneyName", actual.EMoneyName);
            Assert.AreEqual("testPlayerNote", actual.SendNote);
            Assert.AreEqual("11223344", actual.Phone);
        }

        [Test]
        public void Deserialize_Deposit_Pulsa_ApplyInfo_Data_Should_Success()
        {
            var json = "{\"phone\":\"11223344\",\"sendNote\":\"testPlayerNote\",\"bankType\":\"Pulsa\"}";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                Converters = {
                    new ManualDepositApplyInfoJsonConverter()
                }
            };

            var actual = JsonSerializer.Deserialize<ManualDepositPulsaApplyInfo>(json, options);

            Assert.AreEqual("Pulsa", actual.BankType);
            Assert.AreEqual("11223344", actual.Phone);
            Assert.AreEqual("testPlayerNote", actual.SendNote);
        }
    }
}
