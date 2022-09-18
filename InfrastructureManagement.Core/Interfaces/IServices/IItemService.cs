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

        ServiceResult GetItemsDeleted(string filter, ItemStatus? status, int index, int count, string categoryCode);

        ServiceResult GetChildPositions(Guid itemId, int index, int count, string filter, string categoryCode);

        ServiceResult GetChildAPartOfs(Guid itemId, int index, int count, string filter, string categoryCode);

        ServiceResult GetItemDetail(Guid itemId);

        ServiceResult GetItemNoParent(int index, int count, string filter, string categoryCode, Guid rootId);

        ServiceResult GetRoot(Guid itemId);

        ServiceResult GetParentItem(Guid itemId);

        ServiceResult AddItem(Item item);

        ServiceResult UndoDeletedItem(Guid itemId);
    }
}
