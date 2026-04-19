using Infra.Data;

namespace Application.Commands.Subscribe
{
    public sealed class SubscribeCommandHandler : ISubscribeCommandHandler
    {
        private readonly ISubscriberRepository _repository;
        
        public SubscribeCommandHandler(ISubscriberRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SubscribeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.FindByEmailAsync(request.Email);

            if (existing is not null)
            {
                if (existing.Active) return true;

                await _repository.ReactivateAsync(existing.Id, request.Name);
                return false;
            }

            var subscriber = new Subscriber
            {
                Id = Guid.NewGuid(),
                Active = true,
                Name = request.Name,
                Email = request.Email,
                UnsubscribeToken = Guid.NewGuid().ToString("N"),
                GenreMovies = [.. request.GenresMovie.Select(g => g.ToString())],
                GenreTVs = [.. request.GenresSeries.Select(g => g.ToString())],
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _repository.InsertAsync(subscriber);
            return false;
        }
    }
}
