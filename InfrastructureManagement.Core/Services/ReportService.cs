using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using InfrastructureManagement.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Services
{
    public class ReportService : BaseService<Report>, IReportService
    {

        protected IReportRepository _reportRepository;

        public ReportService(IBaseRepository<Report> baseRepository, IReportRepository reportRepository) : base(baseRepository)
        {
            _reportRepository = reportRepository;
        }

        public ServiceResult GetReports(int index, int count, ReportStatus? status, ReportType? type, Guid? reporterId)
        {
            try
            {
                var result = _reportRepository.GetReports(index, count, status, type, reporterId);
                List<Report> data = (List<Report>)result.GetType().GetProperty("data").GetValue(result, null);
                if (data.Count > 0)
                {
                    _serviceResult.Response = new ResponseModel(2000, "Ok", result);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "Không có bản ghi nào!", result);
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

        public ServiceResult UpdateReport(Report report)
        {
            try
            {
                var result = _reportRepository.UpdateReport(report);
                _serviceResult.Response = new ResponseModel(1000, "OK", report);
                _serviceResult.StatusCode = 200;
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
