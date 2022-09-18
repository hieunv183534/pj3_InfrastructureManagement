using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Core.Interfaces.IServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Services
{
    public class RelationshipService : BaseService<MapItem>, IRelationshipService
    {
        private IBaseRepository<ItemLog> _logRepository;
        private IItemRepository _itemRepository;

        public RelationshipService(IBaseRepository<MapItem> baseRepository, IBaseRepository<ItemLog> logRepository, IItemRepository itemRepository) : base(baseRepository)
        {
            _logRepository = logRepository;
            _itemRepository = itemRepository;
        }


        public override ServiceResult Add(MapItem relationship)
        {
            var item = _itemRepository.GetById(relationship.ItemId);
            var parent = _itemRepository.GetById(relationship.ParentId);

            var logData = Newtonsoft.Json.JsonConvert.SerializeObject(
                        new JObject(
                            new JProperty("relationship", JObject.FromObject(relationship)),
                            new JProperty("item", JObject.FromObject(item)),
                            new JProperty("parent", JObject.FromObject(parent))
                            )
                    );

            var logAddParent = new ItemLog()
            {
                Type = Enums.ItemLogType.AddParent,
                ItemId = relationship.ItemId,
                LogData = logData
            };

            var logAddChild = new ItemLog()
            {
                Type = Enums.ItemLogType.AddChild,
                ItemId = relationship.ParentId,
                LogData = logData
            };

            _logRepository.Add(logAddParent);
            _logRepository.Add(logAddChild);

            return base.Add(relationship);
        }

        public override ServiceResult Delete(Guid id)
        {
            var relationship = _baseRepository.GetById(id);
            var item = _itemRepository.GetById(relationship.ItemId);
            var parent = _itemRepository.GetById(relationship.ParentId);

            var logData = Newtonsoft.Json.JsonConvert.SerializeObject(
                        new JObject(
                            new JProperty("relationship", JObject.FromObject(relationship)),
                            new JProperty("item", JObject.FromObject(item)),
                            new JProperty("parent", JObject.FromObject(parent))
                            )
                    );
            var logDeleteParent = new ItemLog()
            {
                Type = Enums.ItemLogType.DeleteParent,
                ItemId = relationship.ItemId,
                LogData = logData
            };

            var logDeleteChild = new ItemLog()
            {
                Type = Enums.ItemLogType.DeleteChild,
                ItemId = relationship.ParentId,
                LogData = logData
            };

            _logRepository.Add(logDeleteParent);
            _logRepository.Add(logDeleteChild);

            return base.Delete(id);
        }
    }
}
