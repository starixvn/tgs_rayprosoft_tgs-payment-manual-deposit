namespace Tgs.Payment.ManualDeposit
{
    public record ManualDepositThirdPartyApplyInfo(
        string BankType,
        string QrCode);
}