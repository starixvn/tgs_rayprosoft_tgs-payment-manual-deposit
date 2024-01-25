using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tgs.Payment.Protos;
using Valhalla.MessageQueue;

namespace Tgs.Payment.Manual.MessageQueue;

public class DepositProcessor : IMessageProcessor<ReadOnlyMemory<byte>, ReadOnlyMemory<byte>>
{
    public ValueTask<ReadOnlyMemory<byte>> HandleAsync(
        string subject,
        ReadOnlyMemory<byte> data,
        IEnumerable<MessageHeaderValue>? headerValues,
        CancellationToken cancellationToken = default)
    {
        var request = DepositRequest.Parser.ParseFrom(data.Span);

        var response = new DepositResponse
        {
            Result = request.ApplyInfo
        };

        return ValueTask.FromResult((ReadOnlyMemory<byte>)response.ToByteArray());
    }
}
