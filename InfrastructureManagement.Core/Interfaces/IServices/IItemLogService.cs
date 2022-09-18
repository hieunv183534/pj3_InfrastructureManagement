using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IServices
{
    public interface IItemLogService : IBaseService<ItemLog>
    {
        ServiceResult GetLogOfItem(Guid itemId);
    }
}
