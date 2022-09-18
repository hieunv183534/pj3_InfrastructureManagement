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

        [HttpGet("{itemId}")]
        public IActionResult GetLogOfItem([FromRoute] Guid itemId)
        {
            var serviceResult = _itemLogService.GetLogOfItem(itemId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
