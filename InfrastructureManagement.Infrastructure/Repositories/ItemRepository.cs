using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public object GetItems(string filter, ItemStatus? status, int index, int count, string categoryCode = "")
        {
            var query = from item in _databaseContext.Items where
                        (item.Name.Contains(filter) || item.Code.Contains(filter)) &&
                        item.CategoryCode.StartsWith(categoryCode) &&
                        (status == null || item.Status == status)
                        select item;
            List<Item> items = query.ToList();
            return new
            {
                total = items.Count,
                data = items.Skip(index).Take(count).ToList()
            };
        }
    }
}
