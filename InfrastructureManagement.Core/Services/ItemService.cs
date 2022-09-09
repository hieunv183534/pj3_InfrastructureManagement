using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Core.Interfaces.IServices;
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

        public ItemService(IBaseRepository<Item> baseRepository, IItemRepository itemRepository) : base(baseRepository)
        {
            _itemRepository = itemRepository;
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
    }
}
