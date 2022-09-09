using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetListItem()
        {
            var serviceResult = _categoryService.GetAll();
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var serviceResult = _categoryService.GetById(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Category category)
        {
            var serviceResult = _categoryService.Add(category);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Category category)
        {
            var serviceResult = _categoryService.Update(category, category.Id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var serviceResult = _categoryService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpDelete]
        public IActionResult DeleteCategoryTree([FromQuery] string categoryCode)
        {
            var serviceResult = _categoryService.DeleteCategoryTree(categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
