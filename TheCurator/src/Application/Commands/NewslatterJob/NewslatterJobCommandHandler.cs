using Infra.Data;
using Infra.Service.Email;
using Infra.Service.Email.Models;
using Infra.Service.Tmdb;
using Microsoft.Extensions.Logging;

namespace Application.Commands.NewslatterJob
{
    public class NewslatterJobCommandHandler : INewslatterJobCommandHandler
    {
        private readonly ITmdbMovieService _service;
        private readonly ISubscriberRepository _repository;
        private readonly IEmailService _emailService;
        private readonly ILogger<NewslatterJobCommandHandler> _logger;

        public NewslatterJobCommandHandler(
            ITmdbMovieService movies,
            ISubscriberRepository repository,
            IEmailService emailService,
            ILogger<NewslatterJobCommandHandler> logger)
        {
            _logger = logger;
            _service = movies;
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<bool> Handle(NewslatterJobCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando envio da newsletter...");

            try
            {
                var movieList = await _service.GetTrendingMoviesAsync(count: 3);
                var seriesList = await _service.GetTrendingSeriesAsync(count: 2);
                var activeList = await _repository.GetActiveAsync();

                foreach (var subscriber in activeList)
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    try
                    {
                        var contract = new ResendEmailRequest(
                            Name: subscriber.Name,
                            Email: subscriber.Email,
                            UnsubscribeToken: subscriber.UnsubscribeToken,
                            Movies: movieList,
                            Series: seriesList
                        );

                        await _emailService.SendNewsletterAsync(contract);
                        _logger.LogInformation("Email enviado para {Email}", subscriber.Email);
                        await Task.Delay(200, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Falha ao enviar para {Email}", subscriber.Email);
                    }
                }

                _logger.LogInformation("Newsletter enviada para {Count} subscribers.", activeList.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro geral no envio da newsletter.");
            }

            return true;
        }
    }
}
