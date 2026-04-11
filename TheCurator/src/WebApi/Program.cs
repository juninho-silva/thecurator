using Application.Extensions;
using Infra.Data;
using Infra.Service.Extensions;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddInfraService();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1.0", new() 
    { 
        Title = "TheCurator API", 
        Version = "v1.0",
        Description = "Versão 1.0 da API"
    });
}); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "TheCurator API v1.0");
});
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
