using Application.Extensions;
using Infra.Data;
using Infra.Service.Extensions;
using Asp.Versioning;
using WebApi.Swagger;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddInfraService();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
       new HeaderApiVersionReader("x-api-version"),
       new UrlSegmentApiVersionReader()
   );
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1.0", new()
    {
        Title = "TheCurator API",
        Version = "v1.0",
        Description = "Versão 1.0 da API"
    });
    options.OperationFilter<RemoveVersionFromParametersFilter>();
    options.DocumentFilter<RemoveVersionFromPathsFilter>();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();  // ← Adicionar aqui
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
