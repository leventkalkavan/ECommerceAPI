using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Application.File;
using ECommerceAPI.Application.InvoiceFile;
using ECommerceAPI.Application.ProductImageFile;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.CustomerRepository;
using ECommerceAPI.Application.Repositories.OrderRepository;
using ECommerceAPI.Application.Repositories.ProductRepository;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.CustomerRepository;
using ECommerceAPI.Persistence.Repositories.FileRepositories;
using ECommerceAPI.Persistence.Repositories.InvoiceFileRepositories;
using ECommerceAPI.Persistence.Repositories.OrderRepository;
using ECommerceAPI.Persistence.Repositories.ProductImagesRepositories;
using ECommerceAPI.Persistence.Repositories.ProductRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence;

public static class ServiceRegistation
{
    public static void AddPersistenceServices(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddDbContext<ECommerceAPIDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString));
        services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository,OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository,OrderWriteRepository>();
        services.AddScoped<IProductReadRepository,ProductReadRepository>();
        services.AddScoped<IProductWriteRepository,ProductWriteRepository>();
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IProductImageFileReadRepository, ProductImageReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageWriteRepository>();

    }
}