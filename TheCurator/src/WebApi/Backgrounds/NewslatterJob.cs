using Application.Commands.NewslatterJob;
using MediatR;

namespace WebApi.Backgrounds
{
    public class NewslatterJob : BackgroundService
    {
        private readonly IMediator _mediator;

        public NewslatterJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                if (now.Hour == 9)
                {
                    await _mediator.Send(new NewslatterJobCommand(), stoppingToken);
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}