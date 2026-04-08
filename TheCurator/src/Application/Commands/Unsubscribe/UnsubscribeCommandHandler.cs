using Infra.Data;

namespace Application.Commands.Unsubscribe
{
    public sealed class UnsubscribeCommandHandler : IUnsubscribeCommandHandler
    {
        private readonly ISubscriberRepository _repository;
        
        public UnsubscribeCommandHandler(ISubscriberRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UnsubscribeCommand request, CancellationToken cancellationToken)
        {
            var subscriber = await _repository.FindByTokenAsync(request.Token);
            if (subscriber is null) return false;

            await _repository.DeactivateAsync(subscriber.Id);
            return true;
        }
    }
}
