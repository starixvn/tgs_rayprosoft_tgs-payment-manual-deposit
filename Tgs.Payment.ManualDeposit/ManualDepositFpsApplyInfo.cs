namespace Tgs.Payment.ManualDeposit
{
    public class ManualDepositFpsApplyInfo : ManualDepositApplyInfo
    {
        public string QrCodeFileUrl { get; init; } = default!;
        public string Email { get; init; } = default!;

        public string FpsId { get; init; } = default!;

        public string Phone { get; init; } = default!;
    }
}