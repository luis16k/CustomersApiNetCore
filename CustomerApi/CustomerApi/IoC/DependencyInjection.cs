using Autofac;
using Customer.Domain.Service.Contracts;
using Customer.Domain.Service.Implementations;

namespace Customer.Api.IoC
{
    public class DependencyInjection
    {
        public static void LoadDI(WebApplicationBuilder builder)
        {
            
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            //builder.Services.AddHttpClient<ICustomerService, CustomerService>();

            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            //builder.Services.AddHttpClient<IPurchaseService, PurchaseService>();
        }
    }
}
