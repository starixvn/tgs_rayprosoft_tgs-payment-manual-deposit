using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Tgs.MessageQueue;
using Tgs.Payment.Protos;

namespace Tgs.Payment.Manual.MessageQueue;

public class WithdrawProcessor : IMessageProcessor
{
    public ValueTask<byte[]> HandleAsync(byte[] data, CancellationToken cancellationToken = default)
    {
        var request = WithdrawRequest.Parser.ParseFrom(data);

        var response = new WithdrawResponse
        {
            Result = request.ApplyInfo
        };

        return ValueTask.FromResult(response.ToByteArray());
    }

    public ValueTask<ReadOnlyMemory<byte>> HandleAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        var request = WithdrawRequest.Parser.ParseFrom(data.Span);

        var response = new WithdrawResponse
        {
            Result = request.ApplyInfo
        };

        return ValueTask.FromResult((ReadOnlyMemory<byte>)response.ToByteArray());
    }
}