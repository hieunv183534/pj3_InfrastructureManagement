using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Services
{
    public class ItemLogService : BaseService<ItemLog>, IItemLogService
    {
        public ItemLogService(IBaseRepository<ItemLog> baseRepository) : base(baseRepository)
        {
        }
    }
}
