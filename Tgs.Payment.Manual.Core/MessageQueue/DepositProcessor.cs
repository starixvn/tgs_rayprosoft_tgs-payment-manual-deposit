using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tgs.MessageQueue;
using Tgs.Payment.Protos;
using TGS.Payment.ManualDeposit;

namespace Tgs.Payment.Manual.MessageQueue
{
	public class DepositProcessor : IMessageProcessor
	{
		private readonly ManualDepositSerializer _manualDepositSerializer;

		public DepositProcessor(ManualDepositSerializer manualDepositSerializer)
		{
			_manualDepositSerializer = manualDepositSerializer ?? throw new ArgumentNullException(nameof(manualDepositSerializer));
		}

		public ValueTask<byte[]> HandleAsync(byte[] data, CancellationToken cancellationToken = default)
		{
			var request = DepositRequest.Parser.ParseFrom(data);

			var response = new DepositResponse
			{
				Result = request.ApplyInfo
			};

			return ValueTask.FromResult(response.ToByteArray());
		}
	}
}
