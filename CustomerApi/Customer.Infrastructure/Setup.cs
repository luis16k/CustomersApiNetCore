using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure
{
    public static class Setup
    {
        public static void AddDBContext(this IServiceCollection services, string DBName)
        {
            services.AddDbContext<DbContextApi>(opt => opt.UseInMemoryDatabase(DBName));            
        }
    }
}