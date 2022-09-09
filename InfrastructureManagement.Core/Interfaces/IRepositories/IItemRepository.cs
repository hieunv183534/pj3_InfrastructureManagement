using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IRepositories
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        object GetItems(string filter, ItemStatus? status, int index, int count, string categoryCode);
    }
}
