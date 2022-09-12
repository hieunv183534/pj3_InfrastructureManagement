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

        public object GetChildAPartOfs(Guid itemId, int index, int count, string filter, string categoryCode = "")
        {
            var type = RelationshipType.APartOf;
            var query = from mapItem in _databaseContext.MapItems
                        join item in _databaseContext.Items
                        on mapItem.ItemId equals item.Id
                        where (mapItem.RelationshipType == type) &&
                        (mapItem.ParentId == itemId) &&
                        (item.Name.Contains(filter) || item.Code.Contains(filter)) &&
                        item.CategoryCode.StartsWith(categoryCode)
                        select new
                        {
                            code = item.Code,
                            name = item.Name,
                            qualityScore = item.QualityScore,
                            status = item.Status,
                            isFixed = mapItem.IsFixed,
                            id = item.Id,
                            relationId = mapItem.Id
                        };
            var items = query.ToList();
            if (items.Count == 0) return null;
            return new
            {
                total = items.Count,
                data = items.Skip(index).Take(count).ToList()
            };
        }

        public object GetChildPositions(Guid itemId, int index, int count, string filter, string categoryCode)
        {
            var type = RelationshipType.JustPosition;
            var query = from mapItem in _databaseContext.MapItems
                        join item in _databaseContext.Items
                        on mapItem.ItemId equals item.Id
                        where (mapItem.RelationshipType == type) &&
                        (mapItem.ParentId == itemId) &&
                        (item.Name.Contains(filter) || item.Code.Contains(filter)) &&
                        item.CategoryCode.StartsWith(categoryCode)
                        select new
                        {
                            code = item.Code,
                            name = item.Name,
                            qualityScore = item.QualityScore,
                            status = item.Status,
                            isFixed = mapItem.IsFixed,
                            id = item.Id,
                            relationId = mapItem.Id
                        };

            var items = query.ToList();
            if (items.Count == 0) return null;
            return new
            {
                total = items.Count,
                data = items.Skip(index).Take(count).ToList()
            };
        }

        public object GetItemDetail(Guid itemId)
        {
            var baseInfo = (from i in _databaseContext.Items
                            join c in _databaseContext.Categorys
                            on i.CategoryId equals c.Id
                            where i.Id == itemId
                            select new
                            {
                                item = i,
                                category = c
                            })
                             .FirstOrDefault();

            if (baseInfo != null)
            {
                int numOfAPart = _databaseContext.MapItems.
                    Where(mi => mi.ParentId == itemId && mi.RelationshipType == RelationshipType.APartOf)
                    .Count();
                int numOfPosition = _databaseContext.MapItems.
                    Where(mi => mi.ParentId == itemId && mi.RelationshipType == RelationshipType.JustPosition)
                    .Count();

                return new
                {
                    baseInfo = baseInfo,
                    numOfAPart = numOfAPart,
                    numOfPosition = numOfPosition
                };
            }
            else return null;
        }

        public object GetItemNoParent(int index, int count, string filter, string categoryCode, Guid rootId)
        {
            var query = from i in _databaseContext.Items
                        join mi in _databaseContext.MapItems
                        on i.Id equals mi.ItemId into imi
                        from vh in imi.DefaultIfEmpty()
                        where i.Id != rootId &&
                        i.CategoryCode.StartsWith(categoryCode) &&
                        ( i.Name.Contains(filter) || i.Code.Contains(filter))
                        select new
                        {
                            code = i.Code,
                            name = i.Name,
                            qualityScore = i.QualityScore,
                            status = i.Status,
                            id = i.Id,
                            categoryCode = i.CategoryCode,
                            relationId = vh.Id == null ? "khong co" : vh.Id.ToString(),
                        };
            var items = query.ToList().Where(vh => vh.relationId.Equals("khong co")).ToList();

            if (items.Count == 0) return null;

            return new
            {
                total = items.Count,
                data = items.Skip(0).Take(1000).ToList()
            };
        }

        public object GetItems(string filter, ItemStatus? status, int index, int count, string categoryCode = "")
        {
            var query = from item in _databaseContext.Items
                        where (item.Name.Contains(filter) || item.Code.Contains(filter)) &&
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

        public object GetParentItem(Guid itemId)
        {
            var queery = from map in _databaseContext.MapItems
                         where map.ItemId == itemId
                         join parent in _databaseContext.Items
                         on map.ParentId equals parent.Id
                         join c in _databaseContext.Categorys
                         on parent.CategoryId equals c.Id
                         select new
                         {
                             item = parent,
                             category = c,
                             relation = map
                         };

            return queery.FirstOrDefault();
        }

        public Item GetRoot(Guid itemId)
        {
            var thisParentRelation = _databaseContext.MapItems.FirstOrDefault(mi => mi.ItemId == itemId);

            if(thisParentRelation == null) return _databaseContext.Items.FirstOrDefault(i=> i.Id == itemId);
            else
            {
                return GetRoot(thisParentRelation.ParentId);
            }
        }
    }
}
