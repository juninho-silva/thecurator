using MediatR;

namespace Application.Commands.NewslatterJob
{
    public interface INewslatterJobCommandHandler : IRequestHandler<NewslatterJobCommand, bool>
    {
    }
}
