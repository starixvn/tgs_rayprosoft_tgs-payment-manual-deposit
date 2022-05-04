namespace Tgs.Payment.ManualDeposit
{
    public class ManualDepositUsdtApplyInfo : ManualDepositApplyInfo
    {
        public decimal SendAmount { get; init; }

        public string Address { get; init; } = default!;

        public string? QrCode { get; init; }

        public decimal Rate { get; init; } = 1;
    }
}
