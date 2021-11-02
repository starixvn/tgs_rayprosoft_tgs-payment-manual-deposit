using Tgs.Payment.ManualDeposit;

namespace TGS.Payment.ManualDeposit
{
    public record ManualDepositBankApplyInfo(        
        string SendBankAccount,
        string ReceiveBankAccount,
        string ReceiveAccountName)
        : ManualDepositApplyInfo;
}