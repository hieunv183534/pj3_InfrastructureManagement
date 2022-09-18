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

        object GetItemsDeleted(string filter, ItemStatus? status, int index, int count, string categoryCode);

        object GetChildPositions(Guid itemId, int index, int count, string filter, string categoryCode);

        object GetChildAPartOfs(Guid itemId, int index, int count, string filter, string categoryCode);

        object GetItemDetail(Guid itemId);

        object GetItemNoParent(int index, int count, string filter, string categoryCode, Guid rootId);

        Item GetRoot(Guid itemId);

        object GetParentItem(Guid itemId);

        Item AddItem(Item item);

        int DeleteItem(Guid itemId);

    }
}
