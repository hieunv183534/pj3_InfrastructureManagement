using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }

        public virtual ServiceResult Add(TEntity entity)
        {
            try
            {
                int rowAffect = _baseRepository.Add(entity);
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

        public ServiceResult Delete(Guid id)
        {
            try
            {
                int rowAffect = _baseRepository.Delete(id);
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

        public virtual ServiceResult GetAll()
        {
            try
            {
                var entitys = _baseRepository.GetAll();
                if (entitys.Count > 0)
                {
                    _serviceResult.Response = new ResponseModel(2000, "OK", entitys);
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

        public virtual ServiceResult GetById(Guid id)
        {
            try
            {
                var entity = _baseRepository.GetById(id);
                if (entity != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "OK", entity);
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

        public virtual ServiceResult Update(TEntity entity, Guid id)
        {
            try
            {
                int rowAffect = _baseRepository.Update(entity, id);
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
    }
}
