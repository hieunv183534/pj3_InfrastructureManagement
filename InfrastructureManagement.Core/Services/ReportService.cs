using InfrastructureManagement.Core.Entities;
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
        public ReportService(IBaseRepository<Report> baseRepository) : base(baseRepository)
        {
        }
    }
}
