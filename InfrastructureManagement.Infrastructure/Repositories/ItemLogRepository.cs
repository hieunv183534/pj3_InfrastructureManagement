using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using Newtonsoft.Json.Linq;
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

        public List<ItemLog> GetLogOfItem(Guid itemId)
        {
            var logs = _entities.Where(l => l.ItemId.Equals(itemId)).OrderBy(l => l.CreatedAt).ToList();
            return logs;
        }
    }
}
