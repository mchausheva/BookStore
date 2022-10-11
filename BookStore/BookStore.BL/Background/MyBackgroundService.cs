using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace BookStore.BL.Background
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private Timer _timer;
        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start {nameof(MyBackgroundService)} {DateTime.Now.ToString("F", CultureInfo.CreateSpecificCulture("en-US"))}");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            Thread.Sleep(500);
            _logger.LogInformation(
                $"{nameof(MyBackgroundService)} is working  ->  {DateTime.Now.ToString("F", CultureInfo.CreateSpecificCulture("en-US"))}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stop {nameof(MyBackgroundService)} {DateTime.Now.ToString("F", CultureInfo.CreateSpecificCulture("en-US"))}");
            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
            while (!cancellationToken.IsCancellationRequested && await periodicTimer.WaitForNextTickAsync(cancellationToken))
            {
                await Task.Delay(999, cancellationToken);
                _logger.LogInformation($"{nameof(MyBackgroundService)} {DateTime.Now.ToString()}");
            }
        }
    }
}
