namespace Tgs.Payment.ManualDeposit
{
    public record ManualDepositThirdPartyApplyInfo(string QrCode)
        : ManualDepositApplyInfo;
}