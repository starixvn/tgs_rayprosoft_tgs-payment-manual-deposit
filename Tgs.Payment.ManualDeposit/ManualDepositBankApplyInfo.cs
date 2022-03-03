namespace Tgs.Payment.ManualDeposit
{
    public class ManualDepositBankApplyInfo : ManualDepositApplyInfo
    {
        public string SendBankAccount { get; init; } = default!;

        public string? SendBankName { get; init; }
        public string ReceiveBankAccount { get; init; } = default!;
        public string ReceiveAccountName { get; init; } = default!;
    }
}