using Api_BigchainDb.Models;
using Api_BigchainDb.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api_BigchainDb.Queue
{
    public class MonitorLoop
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<MonitorLoop> _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly UserService _userService;

        public MonitorLoop(
            UserService userService,
            IBackgroundTaskQueue taskQueue,
            ILogger<MonitorLoop> logger,
            IHostApplicationLifetime applicationLifetime)
        {
            _userService = userService;
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;
        }

        public void StartMonitorLoop(object value)
        {
            _logger.LogInformation($"{nameof(MonitorAsync)} loop is starting.");

            // Run a console user input loop in a background thread
            Task.Run(async () => await MonitorAsync(value));
        }

        private async ValueTask MonitorAsync(object value)
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var tuple = new Tuple<object, Func<object, CancellationToken, ValueTask>>(value, BuildWorkItemAsync);
                await _taskQueue.QueueBackgroundWorkItemAsync(tuple);
            }
        }

        private async ValueTask BuildWorkItemAsync(object value, CancellationToken token)
        {
            #region
            // Simulate three 5-second tasks to complete
            // for each enqueued work item
            //System.Diagnostics.Trace.WriteLine("object class:" + value.GetType().FullName);

            //int delayLoop = 0;
            //var guid = Guid.NewGuid();

            //_logger.LogInformation("Queued work item {Guid} is starting.", guid);
            #endregion

            var listUser = Constant.Chunk((List<User>)value, 1000);
            for(int i=0; i < listUser.Count(); i++ )
            {
                await _userService.CreateListAsync(listUser[i]);
            }

            #region
            //while (!token.IsCancellationRequested && delayLoop < 3)
            //{
            //    try
            //    {
            //        await Task.Delay(TimeSpan.FromSeconds(5), token);
            //    }
            //    catch (OperationCanceledException)
            //    {
            //        // Prevent throwing if the Delay is cancelled
            //    }

            //    ++delayLoop;

            //    _logger.LogInformation("Queued work item {Guid} is running. {DelayLoop}/3", guid, delayLoop);
            //}

            //string format = delayLoop switch
            //{
            //    3 => "Queued Background Task {Guid} is complete.",
            //    _ => "Queued Background Task {Guid} was cancelled."
            //};
            #endregion
            _logger.LogInformation("complete");
        }
    }
}
