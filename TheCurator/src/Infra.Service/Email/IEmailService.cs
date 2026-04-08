using Infra.Service.Email.Models;
namespace Infra.Service.Email
{
    public interface IEmailService
    {
        Task SendNewsletterAsync(ResendEmailRequest request);
    }
}
