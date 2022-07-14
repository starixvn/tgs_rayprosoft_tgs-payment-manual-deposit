using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tgs.MessageQueue;
using Tgs.Payment.Protos;

namespace Tgs.Payment.Manual.MessageQueue;

public class DepositProcessor : IMessageProcessor
{
    public ValueTask<ReadOnlyMemory<byte>> HandleAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        var request = DepositRequest.Parser.ParseFrom(data.Span);

        var response = new DepositResponse
        {
            Result = request.ApplyInfo
        };

        return ValueTask.FromResult((ReadOnlyMemory<byte>)response.ToByteArray());
    }
}