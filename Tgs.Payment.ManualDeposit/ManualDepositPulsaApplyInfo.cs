namespace Tgs.Payment.ManualDeposit;

public class ManualDepositPulsaApplyInfo : ManualDepositApplyInfo
{
    public string Phone { get; init; } = default!;

    public string? SendNote { get; init; }
}
