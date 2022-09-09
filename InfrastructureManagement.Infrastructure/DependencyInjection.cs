using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InfrastructureManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
                options.UseMySql(configuration.GetConnectionString("MySqlDev"), serverVersion);
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
            services.AddScoped(typeof(ITokenAccountRepository), typeof(TokenAccountRepository));
            services.AddScoped(typeof(IItemRepository), typeof(ItemRepository));
            services.AddScoped(typeof(IItemLogRepository), typeof(ItemLogRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IReportRepository), typeof(ReportRepository));
            return services;
        }
    }
}
