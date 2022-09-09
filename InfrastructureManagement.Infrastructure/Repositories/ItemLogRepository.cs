using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class ItemLogRepository : BaseRepository<ItemLog>, IItemLogRepository
    {
        public ItemLogRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
