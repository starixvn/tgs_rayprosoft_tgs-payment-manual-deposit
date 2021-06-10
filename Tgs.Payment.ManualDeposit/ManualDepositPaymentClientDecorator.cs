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

		public ValueTask<string> AskAsync(string subject, string requestInfo)
			=> _paymentClient.AskAsync(subject, requestInfo);

		public async ValueTask<string> DepositAsync(string subject, decimal amount, string? applyInfo)
		{
			if (subject == "tgs.payment.manual.deposit")
				return applyInfo ?? string.Empty;
			else
				return await _paymentClient.DepositAsync(subject, amount, applyInfo).ConfigureAwait(false);
		}

		public ValueTask<string> WithdrawAsync(string subject, decimal amount, string? applyInfo)
			=> _paymentClient.WithdrawAsync(subject, amount, applyInfo);
	}
}