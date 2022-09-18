using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
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
    public class ItemService : BaseService<Item>, IItemService
    {
        private IItemRepository _itemRepository;
        private IBaseRepository<ItemLog> _logRepository;

        public ItemService(IBaseRepository<Item> baseRepository, IItemRepository itemRepository, IBaseRepository<ItemLog> logRepository) : base(baseRepository)
        {
            _itemRepository = itemRepository;
            _logRepository = logRepository;
        }

        public ServiceResult AddItem(Item item)
        {
            try
            {
                var itemAdded = _itemRepository.AddItem(item);

                if (itemAdded != null)
                {

                    var logCreate = new ItemLog()
                    {
                        ItemId = itemAdded.Id,
                        Type = ItemLogType.Create,
                        LogData = Newtonsoft.Json.JsonConvert.SerializeObject(itemAdded)
                    };

                    _logRepository.Add(logCreate);

                    _serviceResult.Response = new ResponseModel(2001, "OK", itemAdded);
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2002, "Thêm không thành công!");
                    _serviceResult.StatusCode = 500;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetChildAPartOfs(Guid itemId, int index, int count, string filter, string categoryCode)
        {
            try
            {
                var result = _itemRepository.GetChildAPartOfs(itemId, index, count, String.IsNullOrEmpty(filter) ? String.Empty : filter, String.IsNullOrEmpty(categoryCode) ? String.Empty : categoryCode);
                if (result != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetChildPositions(Guid itemId, int index, int count, string filter, string categoryCode)
        {
            try
            {
                var result = _itemRepository.GetChildPositions(itemId, index, count, String.IsNullOrEmpty(filter) ? String.Empty : filter, String.IsNullOrEmpty(categoryCode) ? String.Empty : categoryCode);
                if (result != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetItemDetail(Guid itemId)
        {
            try
            {
                var rs = _itemRepository.GetItemDetail(itemId);
                if (rs != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", rs);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", rs);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetItemNoParent(int index, int count, string filter, string categoryCode, Guid rootId)
        {
            try
            {
                var result = _itemRepository.GetItemNoParent(index, count, String.IsNullOrEmpty(filter) ? String.Empty : filter, String.IsNullOrEmpty(categoryCode) ? String.Empty : categoryCode, rootId);
                if (result != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetItems(string filter, ItemStatus? status, int index, int count, string categoryCode)
        {
            try
            {
                var result = _itemRepository.GetItems(String.IsNullOrEmpty(filter) ? String.Empty : filter, status, index, count, String.IsNullOrEmpty(categoryCode) ? String.Empty : categoryCode);
                List<Item> data = (List<Item>)result.GetType().GetProperty("data").GetValue(result, null);
                if (data.Count > 0)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetParentItem(Guid itemId)
        {
            try
            {
                var parent = _itemRepository.GetParentItem(itemId);
                if (parent != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", parent);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", parent);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetRoot(Guid itemId)
        {
            try
            {
                var root = _itemRepository.GetRoot(itemId);
                if (root != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", root);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", root);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public override ServiceResult Update(Item item, Guid id)
        {
            var oldItem = _itemRepository.GetById(id);

            var logUpdate = new ItemLog()
            {
                Type = ItemLogType.Update,
                ItemId = id,
                LogData = Newtonsoft.Json.JsonConvert.SerializeObject(
                    new JObject(
                            new JProperty("oldData", JObject.FromObject(oldItem)),
                            new JProperty("newData", JObject.FromObject(item))
                        )
                    ),
            };

            _logRepository.Add(logUpdate);

            return base.Update(item, id);
        }

        public override ServiceResult Delete(Guid id)
        {
            var logDelete = new ItemLog()
            {
                Type = ItemLogType.Delete,
                ItemId = id,
                LogData = "{}"
            };

            _logRepository.Add(logDelete);

            try
            {
                int rowAffect = _itemRepository.DeleteItem(id);
                _serviceResult.Response = new ResponseModel(2001, "OK", rowAffect);
                _serviceResult.StatusCode = 201;
                return _serviceResult;
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetItemsDeleted(string filter, ItemStatus? status, int index, int count, string categoryCode)
        {
            try
            {
                var result = _itemRepository.GetItemsDeleted(String.IsNullOrEmpty(filter) ? String.Empty : filter, status, index, count, String.IsNullOrEmpty(categoryCode) ? String.Empty : categoryCode);
                List<Item> data = (List<Item>)result.GetType().GetProperty("data").GetValue(result, null);
                if (data.Count > 0)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult UndoDeletedItem(Guid itemId)
        {
            try
            {
                var item = _itemRepository.GetById(itemId);

                var logRestore = new ItemLog()
                {
                    Type = ItemLogType.Restore,
                    ItemId = itemId,
                    LogData = Newtonsoft.Json.JsonConvert.SerializeObject(JObject.FromObject(item))
                };

                _logRepository.Add(logRestore);

                if (item == null)
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", item);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    item.IsDeleted = false;
                    var rs = _itemRepository.Update(item, itemId);
                    _serviceResult.Response = new ResponseModel(2001, "OK", rs);
                    _serviceResult.StatusCode = 201;
                    return _serviceResult;
                }

            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }
    }
}
