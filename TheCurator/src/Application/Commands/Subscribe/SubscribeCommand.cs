using MediatR;

namespace Application.Commands.Subscribe
{
    public class SubscribeCommand : IRequest<bool>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public SubscribeCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
