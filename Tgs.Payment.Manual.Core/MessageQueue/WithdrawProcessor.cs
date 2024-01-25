using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tgs.Payment.Protos;
using Valhalla.MessageQueue;

namespace Tgs.Payment.Manual.MessageQueue;

public class WithdrawProcessor : IMessageProcessor<ReadOnlyMemory<byte>, ReadOnlyMemory<byte>>
{
    public ValueTask<ReadOnlyMemory<byte>> HandleAsync(
        string subject,
        ReadOnlyMemory<byte> data,
        IEnumerable<MessageHeaderValue>? headerValues,
        CancellationToken cancellationToken = default)
    {
        var request = WithdrawRequest.Parser.ParseFrom(data.Span);

        var response = new WithdrawResponse
        {
            Result = request.ApplyInfo
        };

        return ValueTask.FromResult((ReadOnlyMemory<byte>)response.ToByteArray());
    }
}
