using MediatR;

namespace Application.Commands.Unsubscribe
{
    public class UnsubscribeCommand : IRequest<bool>
    {
        public UnsubscribeCommand(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
