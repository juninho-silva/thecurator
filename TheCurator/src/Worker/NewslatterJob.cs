public class NewsletterWorker
{
    private readonly ILogger<NewsletterWorker> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRecommendationService _recommendationService;
    private readonly IEmailService _emailService;

    public NewsletterWorker(
        ILogger<NewsletterWorker> logger,
        IUserRepository userRepository,
        IRecommendationService recommendationService,
        IEmailService emailService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _recommendationService = recommendationService;
        _emailService = emailService;
    }

    public async Task ExecuteAsync()
    {
        var executionId = Guid.NewGuid();
        var today = DateTime.UtcNow.DayOfWeek;

        _logger.LogInformation("Execução {ExecutionId} iniciada para {Day}", executionId, today);

        var users = await _userRepository.GetUsersByDayAsync(today);

        _logger.LogInformation("{Count} usuários encontrados", users.Count);

        foreach (var user in users)
        {
            try
            {
                var recommendation = await _recommendationService.GetRecommendationAsync(user);

                if (recommendation == null)
                {
                    _logger.LogWarning("Sem recomendação para usuário {UserId}", user.Id);
                    continue;
                }

                await _emailService.SendAsync(user.Email, recommendation);

                _logger.LogInformation("Email enviado para usuário {UserId}", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar usuário {UserId}", user.Id);
            }
        }

        _logger.LogInformation("Execução {ExecutionId} finalizada", executionId);
    }
}