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
    }
}
