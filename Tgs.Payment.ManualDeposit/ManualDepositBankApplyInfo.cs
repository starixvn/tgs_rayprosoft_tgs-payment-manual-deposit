namespace Tgs.Payment.ManualDeposit
{
    public record ManualDepositBankApplyInfo(
        string SendBankAccount,
        string ReceiveBankAccount,
        string ReceiveAccountName)
        : ManualDepositApplyInfo;
}