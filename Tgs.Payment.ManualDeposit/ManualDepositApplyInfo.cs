namespace Tgs.Payment.ManualDeposit
{
    public abstract record ManualDepositApplyInfo
    {
        public string BankType { get; init; } = default!;
    };
}