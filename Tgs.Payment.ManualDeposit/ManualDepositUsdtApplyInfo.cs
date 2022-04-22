namespace Tgs.Payment.ManualDeposit
{
    public class ManualDepositUsdtApplyInfo : ManualDepositApplyInfo
    {
        public string Address { get; init; } = default!;

        public string? QrCode { get; init; }
    }
}
