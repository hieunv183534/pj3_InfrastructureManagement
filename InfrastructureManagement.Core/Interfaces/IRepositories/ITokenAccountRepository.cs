using InfrastructureManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IRepositories
{
    public interface ITokenAccountRepository : IBaseRepository<TokenAccount>
    {
        TokenAccount GetTokenByUsername(string username);

        TokenAccount GetTokenByToken(string token);

        int DeleteTokenByUsername(string username);

        int DeleteTokenByToken(string token);
    }
}
