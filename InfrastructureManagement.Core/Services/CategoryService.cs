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
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(IBaseRepository<Category> baseRepository, ICategoryRepository categoryRepository) : base(baseRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ServiceResult DeleteCategoryTree(string code)
        {
            try
            {
                int rowAffect = _categoryRepository.DeleteCategoryTree(code);
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
