using InfrastructureManagement.Core.Entities;
using InfrastructureManagement.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public Account GetAccountByUsername(string username)
        {
            return (from account in _databaseContext.Accounts where account.Username == username select account).FirstOrDefault();
        }

        public async Task<object> GetOverView()
        {
            Task<int>[] tasks = new Task<int>[]
            {
                GetNumCategory(),
                GetNumItem(), GetNumItemUsing(), GetNumItemMaintain(), GetNumItemBroken(),GetNumItemStorage(), GetNumItemLiquidation(),
                GetNumReport(), GetNumReportNew(), GetNumReportConfirm(), GetNumReportReject(), GetNumReportDoing(), GetNumReportComplete()
            };
            IEnumerable<int> nums = await Task.WhenAll(tasks);
            return nums;
        }

        private Task<int> GetNumCategory()
        {
            return Task.FromResult(_databaseContext.Categorys.Count());
        }

        private Task<int> GetNumItem()
        {
            return Task.FromResult(_databaseContext.Items.Count());
        }

        private Task<int> GetNumItemUsing()
        {
            return Task.FromResult(_databaseContext.Items.Where(i => i.Status == Core.Enums.ItemStatus.Using).Count());
        }

        private Task<int> GetNumItemMaintain()
        {
            return Task.FromResult(_databaseContext.Items.Where(i => i.Status == Core.Enums.ItemStatus.UnderMaintenance).Count());
        }

        private Task<int> GetNumItemBroken()
        {
            return Task.FromResult(_databaseContext.Items.Where(i => i.Status == Core.Enums.ItemStatus.Broken).Count());
        }

        private Task<int> GetNumItemStorage()
        {
            return Task.FromResult(_databaseContext.Items.Where(i => i.Status == Core.Enums.ItemStatus.Storage).Count());
        }

        private Task<int> GetNumItemLiquidation()
        {
            return Task.FromResult(_databaseContext.Items.Where(i => i.Status == Core.Enums.ItemStatus.Liquidation).Count());
        }

        private Task<int> GetNumReport()
        {
            return Task.FromResult(_databaseContext.Reports.Count());
        }

        private Task<int> GetNumReportNew()
        {
            return Task.FromResult(_databaseContext.Reports.Where(r => r.Status == Core.Enums.ReportStatus.New).Count());
        }

        private Task<int> GetNumReportConfirm()
        {
            return Task.FromResult(_databaseContext.Reports.Where(r => r.Status == Core.Enums.ReportStatus.Confirm).Count());
        }

        private Task<int> GetNumReportReject()
        {
            return Task.FromResult(_databaseContext.Reports.Where(r => r.Status == Core.Enums.ReportStatus.Reject).Count());
        }

        private Task<int> GetNumReportDoing()
        {
            return Task.FromResult(_databaseContext.Reports.Where(r => r.Status == Core.Enums.ReportStatus.Doing).Count());
        }

        private Task<int> GetNumReportComplete()
        {
            return Task.FromResult(_databaseContext.Reports.Where(r => r.Status == Core.Enums.ReportStatus.Complete).Count());
        }
    }
}
