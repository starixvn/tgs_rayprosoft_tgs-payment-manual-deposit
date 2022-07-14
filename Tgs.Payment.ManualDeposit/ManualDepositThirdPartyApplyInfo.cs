namespace Tgs.Payment.ManualDeposit;

public class ManualDepositThirdPartyApplyInfo : ManualDepositApplyInfo
{
    public string QrCode { get; init; } = default!;
}