using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Tgs.Payment.ManualDeposit;
using Tgs.Payment.ManualDeposit.Converters;

namespace ManualDeposit.UnitTests;

public class ConverterTests
{
    [Test]
    public void Deserialize_Deposit_Bank_ApplyInfo_Data_Should_Success()
    {
        var json = "{\"sendBankAccount\":\"sendBankAccount\",\"sendName\":\"sendName\",\"sendNote\":\"sendNote\",\"ReceiveBankAccount\":\"brandPaymentInfo.BankAccount\",\"ReceiveAccountName\":\"brandPaymentInfo.AccountName\",\"bankType\":\"Bank\"}";

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
        ClassicAssert.AreEqual("Bank", actual.BankType);
        ClassicAssert.AreEqual("sendNote", actual.SendNote);
        ClassicAssert.AreEqual("sendName", actual.SendName);
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

        ClassicAssert.AreEqual("ThirdParty", actual.BankType);
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

        ClassicAssert.AreEqual("FPS", actual.BankType);
        ClassicAssert.AreEqual("88888888", actual.Phone);
        ClassicAssert.AreEqual("123456789", actual.FpsId);
        ClassicAssert.AreEqual("https://www.thirdParty/qrCode", actual.QrCodeFileUrl);
        ClassicAssert.AreEqual("fps@test.com", actual.Email);
        ClassicAssert.AreEqual("1688888888", actual.SendBankAccount);
        ClassicAssert.AreEqual("bankName", actual.SendBankName);
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

        ClassicAssert.AreEqual("Usdt", actual.BankType);
        ClassicAssert.AreEqual("TCKCX5Khy9V96qvD5T5K8c6bzWXzMkawbw", actual.Address);
        ClassicAssert.AreEqual("https://www.test.com/qrCode", actual.QrCode);
        ClassicAssert.AreEqual(168m, actual.Rate);
        ClassicAssert.AreEqual(1680m, actual.SendAmount);
    }

    [Test]
    public void Deserialize_Deposit_EWallet_ApplyInfo_Data_Should_Success()
    {
        var json = "{\"qrCode\":\"https://www.test.com/qrCode\",\"eMoneyName\":\"TestEMoneyName\",\"phone\":\"11223344\",\"sendNote\":\"testPlayerNote\",\"sendPhone\":\"111666888\",\"sendEMoneyName\":\"testPlayerEMoneyName\",\"bankType\":\"EWallet\"}";

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

        ClassicAssert.AreEqual("EWallet", actual.BankType);
        ClassicAssert.AreEqual("https://www.test.com/qrCode", actual.QrCode);
        ClassicAssert.AreEqual("TestEMoneyName", actual.EMoneyName);
        ClassicAssert.AreEqual("testPlayerNote", actual.SendNote);
        ClassicAssert.AreEqual("11223344", actual.Phone);
        ClassicAssert.AreEqual("testPlayerEMoneyName", actual.SendEMoneyName);
        ClassicAssert.AreEqual("111666888", actual.SendPhone);
    }

    [Test]
    public void Deserialize_Deposit_Pulsa_ApplyInfo_Data_Should_Success()
    {
        var json = "{\"phone\":\"11223344\",\"sendNote\":\"testPlayerNote\",\"sendName\":\"testRealName\",\"sendPhone\":\"222333555\",\"bankType\":\"Pulsa\"}";

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

        ClassicAssert.AreEqual("Pulsa", actual.BankType);
        ClassicAssert.AreEqual("11223344", actual.Phone);
        ClassicAssert.AreEqual("testPlayerNote", actual.SendNote);
        ClassicAssert.AreEqual("testRealName", actual.SendName);
        ClassicAssert.AreEqual("222333555", actual.SendPhone);
    }

    [Test]
    public void Deserialize_Deposit_Coinsph_ApplyInfo_Data_Should_Success()
    {
        var json = "{\"qrCode\":\"https://www.test.com/qrCode\",\"name\":\"TestName\",\"phone\":\"11223344\",\"sendPhone\":\"111666888\",\"sendName\":\"testPlayerName\",\"bankType\":\"Coinsph\"}";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Converters = {
                new ManualDepositApplyInfoJsonConverter()
            }
        };

        var actual = JsonSerializer.Deserialize<ManualDepositCoinsphApplyInfo>(json, options);

        ClassicAssert.AreEqual("Coinsph", actual.BankType);
        ClassicAssert.AreEqual("https://www.test.com/qrCode", actual.QrCode);
        ClassicAssert.AreEqual("TestName", actual.Name);
        ClassicAssert.AreEqual("11223344", actual.Phone);
        ClassicAssert.AreEqual("testPlayerName", actual.SendName);
        ClassicAssert.AreEqual("111666888", actual.SendPhone);
    }
}
