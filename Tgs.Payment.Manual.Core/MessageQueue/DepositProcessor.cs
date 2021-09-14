using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tgs.MessageQueue;
using Tgs.Payment.Protos;

namespace Tgs.Payment.Manual.MessageQueue
{
    public class DepositProcessor : IMessageProcessor
    {
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
