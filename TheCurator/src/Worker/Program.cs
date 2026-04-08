using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<NewsletterWorker>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IRecommendationService, RecommendationService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var worker = scope.ServiceProvider.GetRequiredService<NewsletterWorker>();

await worker.ExecuteAsync();