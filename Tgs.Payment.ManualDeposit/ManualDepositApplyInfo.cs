namespace TGS.Payment.ManualDeposit
{
    public record ManualDepositApplyInfo(
        string SendBankAccount,
        string ReceiveBankAccount,
        string ReceiveAccountName);
}
