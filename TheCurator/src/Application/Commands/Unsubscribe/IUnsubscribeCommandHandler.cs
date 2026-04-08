using MediatR;

namespace Application.Commands.Unsubscribe
{
    public interface IUnsubscribeCommandHandler : IRequestHandler<UnsubscribeCommand, bool>
    {
    }
}
