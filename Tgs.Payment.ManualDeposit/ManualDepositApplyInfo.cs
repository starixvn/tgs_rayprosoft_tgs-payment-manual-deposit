namespace Tgs.Payment.ManualDeposit
{
    public record ManualDepositApplyInfo
    {
        public string BankType { get; set; } = default!;
    };
}