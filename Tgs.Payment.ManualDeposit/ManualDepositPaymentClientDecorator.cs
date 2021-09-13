using System;
using System.Threading.Tasks;
using Tgs.Payment.Clients;

namespace TGS.Payment.ManualDeposit
{
    public class ManualDepositPaymentClientDecorator : IPaymentClient
    {
        private readonly IPaymentClient _paymentClient;

        public ManualDepositPaymentClientDecorator(IPaymentClient paymentClient)
        {
            _paymentClient = paymentClient ?? throw new ArgumentNullException(nameof(paymentClient));
        }

        public ValueTask ApproveDepositAsync(Guid formId, Guid brandId)
            => _paymentClient.ApproveDepositAsync(formId, brandId);

        public ValueTask<string> AskAsync(string subject, string requestInfo)
            => _paymentClient.AskAsync(subject, requestInfo);

        public ValueTask<string> DepositAsync(string subject, decimal amount, string? applyInfo)
            => _paymentClient.DepositAsync(subject, amount, applyInfo);

        public ValueTask EmitDepositFormCreatedEventAsync(Guid formId, Guid brandId)
            => _paymentClient.EmitDepositFormCreatedEventAsync(formId, brandId);

        public ValueTask EmitDepositFormStatusChangedEventAsync(Guid formId, Guid brandId)
            => _paymentClient.EmitDepositFormStatusChangedEventAsync(formId, brandId);

        public ValueTask EmitWithdrawFormCreatedEventAsync(Guid fromId, Guid brandId)
            => _paymentClient.EmitWithdrawFormCreatedEventAsync(fromId, brandId);

        public ValueTask EmitWithdrawFormStatusChangedEventAsync(Guid formId, Guid brandId)
            => _paymentClient.EmitWithdrawFormStatusChangedEventAsync(formId, brandId);

        public ValueTask<string> WithdrawAsync(string subject, decimal amount, string? applyInfo)
            => _paymentClient.WithdrawAsync(subject, amount, applyInfo);
    }
}
