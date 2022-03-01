namespace Tgs.Payment.ManualDeposit
{
    public class ManualDepositFPSApplyInfo : ManualDepositApplyInfo
    {
        public string QrCodeFileUrl { get; init; } = default!;
        public string Email { get; init; } = default!;

        public string FPSId { get; init; } = default!;

        public string Phone { get; init; } = default!;
    }
}