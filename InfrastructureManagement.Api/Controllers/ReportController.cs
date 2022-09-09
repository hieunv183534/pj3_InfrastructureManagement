using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfrastructureManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService itemService)
        {
            _reportService = itemService;
        }

        [HttpGet]
        public IActionResult GetListItem()
        {
            var serviceResult = _reportService.GetAll();
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var serviceResult = _reportService.GetById(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Report report)
        {
            var serviceResult = _reportService.Add(report);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Report report)
        {
            var serviceResult = _reportService.Update(report, report.Id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var serviceResult = _reportService.Delete(id);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
