namespace TGS.Payment.ManualDeposit
{
    public record ManualDepositApplyInfo(
        string BankType,
        string SendBankAccount,
        string ReceiveBankAccount,
        string ReceiveAccountName);
}