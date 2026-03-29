using MediatR;

namespace Application.Commands.Subscribe
{
    public interface ISubscribeCommandHandler : IRequestHandler<SubscribeCommand, bool>
    {
    }
}
