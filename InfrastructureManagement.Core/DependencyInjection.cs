using InfrastructureManagement.Core.Interfaces.IServices;
using InfrastructureManagement.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(ITokenAccountService), typeof(TokenAccountService));
            services.AddScoped(typeof(IItemService), typeof(ItemService));
            services.AddScoped(typeof(IItemLogService), typeof(ItemLogService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(IReportService), typeof(ReportService));
            services.AddScoped(typeof(IRelationshipService), typeof(RelationshipService));
            return services;
        }
    }
}
