using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var serviceResult = _itemService.GetById(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Item item)
        {
            var serviceResult = _itemService.Add(item);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Item item)
        {
            var serviceResult = _itemService.Update(item, item.Id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var serviceResult = _itemService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }


        [HttpGet]
        public IActionResult GetItems([FromQuery] string? filter, [FromQuery] ItemStatus? status, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetItems(filter,status,index,count,categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
