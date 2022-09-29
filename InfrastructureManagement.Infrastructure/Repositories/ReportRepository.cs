using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Enums;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public object GetReports(int index, int count, ReportStatus? status, ReportType? type, Guid? reporterId)
        {
            IQueryable<Report> query;
            if(type== ReportType.Broken)
            {
                query = from r in _databaseContext.Reports
                        join i in _databaseContext.Items
                        on r.PositionId equals i.Id
                        join a in _databaseContext.Accounts
                        on r.ReporterId equals a.Id
                        where (status == null || r.Status == status) &&
                        (type == null || r.Type == type) &&
                        (reporterId == null || r.ReporterId.Equals(reporterId))
                        select new Report()
                        {
                            Id = r.Id,
                            PositionId = r.PositionId,
                            PositionItem = i,
                            ReporterId = r.ReporterId,
                            Reporter = a,
                            Note = r.Note,
                            Reply = r.Reply,
                            Type = r.Type,
                            Status = r.Status,
                            CreatedAt = r.CreatedAt,
                            Urls = r.Urls
                        };
            }
            else
            {
                query = from r in _databaseContext.Reports
                        join c in _databaseContext.Categorys
                        on r.CategoryId equals c.Id
                        join i in _databaseContext.Items
                        on r.PositionId equals i.Id
                        join a in _databaseContext.Accounts
                        on r.ReporterId equals a.Id
                        where (status == null || r.Status == status) &&
                        (type == null || r.Type == type) &&
                        (reporterId == null || r.ReporterId.Equals(reporterId))
                        select new Report()
                        {
                            Id = r.Id,
                            CategoryId = r.CategoryId,
                            Category = c,
                            Quantity = r.Quantity,
                            PositionId = r.PositionId,
                            PositionItem = i,
                            ReporterId = r.ReporterId,
                            Reporter = a,
                            Note = r.Note,
                            Reply = r.Reply,
                            Type = r.Type,
                            Status = r.Status,
                            CreatedAt = r.CreatedAt,
                            Urls = r.Urls
                        };
            }
             
            var reports = query.ToList();
           
            return new
            {
                total = reports.Count,
                data = reports.Skip(index).Take(count).ToList()
            };
        }

        public int UpdateReport(Report report)
        {
            var oldReport = _entities.FirstOrDefault(r=> r.Id == report.Id);    
            if (oldReport == null)
                return 0;
            else
            {
                oldReport.Reply = report.Reply;
                oldReport.Status = report.Status;
            };

            return _databaseContext.SaveChanges();
        }
    }
}
