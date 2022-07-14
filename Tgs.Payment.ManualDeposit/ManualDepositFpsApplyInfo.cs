namespace Tgs.Payment.ManualDeposit;

public class ManualDepositFpsApplyInfo : ManualDepositApplyInfo
{
    public string? QrCodeFileUrl { get; init; }
    public string? Email { get; init; }

    public string? FpsId { get; init; }

    public string? Phone { get; init; }

    public string? SendBankAccount { get; init; }

    public string? SendBankName { get; init; }
}