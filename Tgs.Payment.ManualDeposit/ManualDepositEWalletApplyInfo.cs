namespace Tgs.Payment.ManualDeposit
{
    public class ManualDepositEWalletApplyInfo : ManualDepositApplyInfo
    {
        public string EMoneyName { get; init; } = default!;

        public string? QrCode { get; init; }

        public string Phone { get; init; } = default!;

        public string? SendNote { get; init; }
    }
}
