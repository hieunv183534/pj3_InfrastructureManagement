using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IServices
{
    public interface IReportService : IBaseService<Report>
    {
        ServiceResult GetReports(int index, int count, ReportStatus? status, ReportType? type, Guid? reporterId);

        ServiceResult UpdateReport(Report report);
    }
}
