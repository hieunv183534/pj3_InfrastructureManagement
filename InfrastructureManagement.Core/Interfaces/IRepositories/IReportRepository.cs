using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IRepositories
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        object GetReports(int index, int count, ReportStatus? status, ReportType? type, Guid? reporterId);

        int UpdateReport(Report report);
    }
}
