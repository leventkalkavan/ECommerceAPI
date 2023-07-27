using ECommerceAPI.Application.Validations.Product;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Services.Local;
using ECommerceAPI.Infrastructure.Storage.Azure;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var httpClientHandler = new HttpClientHandler();
httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.WithOrigins("https://localhost:4200","http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddControllers().AddFluentValidation(configuration =>
    configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
     .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true);
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage<LocalStorage>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();