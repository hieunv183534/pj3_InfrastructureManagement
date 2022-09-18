using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Services
{
    public class ItemLogService : BaseService<ItemLog>, IItemLogService
    {
        private IItemLogRepository _logRepository;

        public ItemLogService(IBaseRepository<ItemLog> baseRepository, IItemLogRepository logRepository) : base(baseRepository)
        {
            _logRepository = logRepository;
        }

        public ServiceResult GetLogOfItem(Guid itemId)
        {
            try
            {
                var log = _logRepository.GetLogOfItem(itemId);
                if (log.Count > 0)
                {
                    _serviceResult.Response = new ResponseModel(2000, "OK", log);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "No data or end of list data");
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
