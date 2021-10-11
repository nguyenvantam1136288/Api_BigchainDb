using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api_BigchainDb.Queue
{
    public sealed class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<QueuedHostedService> _logger;

        public QueuedHostedService(
            IBackgroundTaskQueue taskQueue,
            ILogger<QueuedHostedService> logger) =>
            (_taskQueue, _logger) = (taskQueue, logger);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(QueuedHostedService)} is running.{Environment.NewLine}" +
                $"{Environment.NewLine}Tap W to add a work item to the " +
                $"background queue.{Environment.NewLine}");

            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Tuple<object, Func<object, CancellationToken, ValueTask>>? workItem =
                        await _taskQueue.DequeueAsync(stoppingToken);
                    await workItem.Item2(workItem.Item1, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if stoppingToken was signaled
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing task work item.");
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(QueuedHostedService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
