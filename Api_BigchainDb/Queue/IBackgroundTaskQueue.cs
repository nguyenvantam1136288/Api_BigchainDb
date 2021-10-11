using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api_BigchainDb.Queue
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(
             Tuple<object, Func<object, CancellationToken, ValueTask>> workItem);

        ValueTask<Tuple<object, Func<object, CancellationToken, ValueTask>>> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
