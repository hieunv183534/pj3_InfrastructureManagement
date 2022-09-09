using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IServices
{
    public interface IItemService : IBaseService<Item>
    {
        ServiceResult GetItems(string filter, ItemStatus? status, int index, int count, string categoryCode);
    }
}
