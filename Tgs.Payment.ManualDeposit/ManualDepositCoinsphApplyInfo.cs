namespace Tgs.Payment.ManualDeposit;

public class ManualDepositCoinsphApplyInfo : ManualDepositApplyInfo
{
    public string Phone { get; init; } = default!;

    public string Name { get; init; } = default!;

    public string? QrCode { get; init; }

    public string? SendName { get; init; }

    public string? SendPhone { get; init; }

    public string? Address { get; init; }
}
