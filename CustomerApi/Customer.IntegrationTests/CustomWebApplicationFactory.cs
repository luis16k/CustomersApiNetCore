using Customer.Domain.Models;
using Customer.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = builder.Build();
        host.Start();

        var serviceProvider = host.Services;

        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<DbContextApi>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<DbContextApi>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                string inMemoryCollectionName = Guid.NewGuid().ToString();

                services.AddDbContext<DbContextApi>(options =>
                {
                    options.UseInMemoryDatabase(inMemoryCollectionName);
                });

            });
    }

    /*protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>{               
        });
    }*/
}
