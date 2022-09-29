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
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService itemService)
        {
            _reportService = itemService;
        }

        [Authorize(Roles ="admin")]
        [HttpGet("getAdminReports")]
        public IActionResult GetAdminReports([FromQuery]int index, [FromQuery] int count, [FromQuery] ReportStatus? status, [FromQuery] ReportType? type)
        {
            var serviceResult = _reportService.GetReports(index, count, status, type, null);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "reporter")]
        [HttpGet("getMyReports")]
        public IActionResult GetMyReports([FromQuery] int index, [FromQuery] int count, [FromQuery] ReportStatus? status, [FromQuery] ReportType? type, [FromQuery] Guid reporterId)
        {
            var serviceResult = _reportService.GetReports(index, count, status, type, reporterId);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "reporter")]
        [HttpPost]
        public IActionResult PostReport([FromBody] Report report)
        {
            report.Status = ReportStatus.New;
            var serviceResult = _reportService.Add(report);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles = "reporter")]
        [HttpDelete("{id}")]
        public IActionResult DeleteReport([FromRoute] Guid id)
        {
            var serviceResult = _reportService.Delete(id);  
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }

        [Authorize(Roles ="admin")]
        [HttpPost("adminUpdate")]
        public IActionResult AdminUpdate(Report report)
        {
            var serviceResult = _reportService.UpdateReport(report);
            return StatusCode(serviceResult.StatusCode, serviceResult.Response);
        }
    }
}
