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
        private readonly IBaseService<MapItem> _relationshipService;

        public ItemController(IItemService itemService, IBaseService<MapItem> relationshipService)
        {
            _itemService = itemService;
            _relationshipService = relationshipService;
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
            var serviceResult = _itemService.GetItems(filter, status, index, count, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("getChildPositions/{itemId}")]
        public IActionResult GetChildPositions([FromRoute] Guid itemId, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? filter, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetChildPositions(itemId, index, count, filter, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("getChildAPartOfs/{itemId}")]
        public IActionResult GetChildAPartOfs([FromRoute] Guid itemId, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? filter, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetChildAPartOfs(itemId, index, count, filter, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("getItemDetail/{itemId}")]
        public IActionResult GetItemDetail(Guid itemId)
        {
            var serviceResult = _itemService.GetItemDetail(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpDelete("deleteRelationship/{id}")]
        public IActionResult DeleteRelationship([FromRoute] Guid id)
        {

            var serviceResult = _relationshipService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPost("addRelationship")]
        public IActionResult AddRelationship([FromBody] MapItem relationship)
        {
            var serviceResult = _relationshipService.Add(relationship);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("getItemNoParent")]
        public IActionResult GetItemNoParent([FromQuery] int index, [FromQuery] int count, [FromQuery] string? filter, [FromQuery] string? categoryCode, [FromQuery] Guid rootId)
        {
            var serviceResult = _itemService.GetItemNoParent(index, count, filter, categoryCode , rootId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("getRoot/{itemId}")]
        public IActionResult GetRoot([FromRoute] Guid itemId)
        {
            var serviceResult = _itemService.GetRoot(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
