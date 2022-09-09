using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemLogController : ControllerBase
    {
        private IItemLogService _itemLogService;

        public ItemLogController(IItemLogService itemLogService)
        {
            _itemLogService = itemLogService;
        }

        [HttpGet]
        public IActionResult GetListItem()
        {
            var serviceResult = _itemLogService.GetAll();
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var serviceResult = _itemLogService.GetById(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ItemLog itemLog)
        {
            var serviceResult = _itemLogService.Add(itemLog);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ItemLog itemLog)
        {
            var serviceResult = _itemLogService.Update(itemLog, itemLog.Id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var serviceResult = _itemLogService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
