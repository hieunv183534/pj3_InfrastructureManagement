using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IRelationshipService _relationshipService;

        public ItemController(IItemService itemService, IRelationshipService relationshipService)
        {
            _itemService = itemService;
            _relationshipService = relationshipService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var serviceResult = _itemService.GetById(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles ="admin")]
        [HttpPost]
        public IActionResult Add([FromBody] Item item)
        {
            var serviceResult = _itemService.AddItem(item);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles ="admin")]
        [HttpPut]
        public IActionResult Update([FromBody] Item item)
        {
            var serviceResult = _itemService.Update(item, item.Id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles ="admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var serviceResult = _itemService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetItems([FromQuery] string? filter, [FromQuery] ItemStatus? status, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetItems(filter, status, index, count, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles ="admin")]
        [HttpGet("deleted")]
        public IActionResult GetItemsDeleted([FromQuery] string? filter, [FromQuery] ItemStatus? status, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetItemsDeleted(filter, status, index, count, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles ="admin")]
        [HttpGet("undoDeleted/{itemId}")]
        public IActionResult UndoDeletedItem([FromRoute] Guid itemId)
        {
            var serviceResult = _itemService.UndoDeletedItem(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getChildPositions/{itemId}")]
        public IActionResult GetChildPositions([FromRoute] Guid itemId, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? filter, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetChildPositions(itemId, index, count, filter, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getChildAPartOfs/{itemId}")]
        public IActionResult GetChildAPartOfs([FromRoute] Guid itemId, [FromQuery] int index, [FromQuery] int count, [FromQuery] string? filter, [FromQuery] string? categoryCode)
        {
            var serviceResult = _itemService.GetChildAPartOfs(itemId, index, count, filter, categoryCode);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize]
        [HttpGet("getItemDetail/{itemId}")]
        public IActionResult GetItemDetail(Guid itemId)
        {
            var serviceResult = _itemService.GetItemDetail(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteRelationship/{id}")]
        public IActionResult DeleteRelationship([FromRoute] Guid id)
        {
            var serviceResult = _relationshipService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addRelationship")]
        public IActionResult AddRelationship([FromBody] MapItem relationship)
        {
            var serviceResult = _relationshipService.Add(relationship);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getItemNoParent")]
        public IActionResult GetItemNoParent([FromQuery] int index, [FromQuery] int count, [FromQuery] string? filter, [FromQuery] string? categoryCode, [FromQuery] Guid rootId)
        {
            var serviceResult = _itemService.GetItemNoParent(index, count, filter, categoryCode , rootId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getRoot/{itemId}")]
        public IActionResult GetRoot([FromRoute] Guid itemId)
        {
            var serviceResult = _itemService.GetRoot(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getParentItem/{itemId}")]
        public IActionResult GetParentItem([FromRoute] Guid itemId)
        {
            var serviceResult = _itemService.GetParentItem(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
