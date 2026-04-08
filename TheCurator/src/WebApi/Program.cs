using Application.Extensions;
using Infra.Data;
using Infra.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfraData(builder.Configuration);
builder.Services.AddInfraService();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
    

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
