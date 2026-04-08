using Infra.Service.Email;
using Infra.Service.Tmdb;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace Infra.Service.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfraService(this IServiceCollection services)
        {
            services.AddResendClient();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITmdbMovieService, TmdbMovieService>();
        }

        private static void AddResendClient(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddHttpClient<ResendClient>();
            services.Configure<ResendClientOptions>(options =>
            {
                options.ApiToken = Environment.GetEnvironmentVariable("RESEND_API_KEY") ?? "re_GpseHTT9_L7CsqQBBCEq9fH2pGx9cJGJ5";
            });
            services.AddTransient<IResend, ResendClient>();
        }
    }
}
