using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Api_BigchainDb.Queue
{
    public class DefaultBackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Tuple<object, Func<object, CancellationToken, ValueTask>>> _queue;

        public DefaultBackgroundTaskQueue(int capacity)
        {
            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<Tuple<object, Func<object, CancellationToken, ValueTask>>>(options);
        }

        public async ValueTask QueueBackgroundWorkItemAsync(
            Tuple<object, Func<object, CancellationToken, ValueTask>> workItem)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await _queue.Writer.WriteAsync(workItem);
        }

        public async ValueTask<Tuple<object, Func<object, CancellationToken, ValueTask>>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            Tuple<object, Func<object, CancellationToken, ValueTask>>? workItem =
                await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }
    }
}
