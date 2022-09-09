using InfrastructureManagement.Core.Dtos;
using InfrastructureManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Core.Interfaces.IServices
{
    public interface ITokenAccountService : IBaseService<TokenAccount>
    {
        ServiceResult GetTokenByUsername(string username);

        ServiceResult DeleteTokenByUsername(string username);

        ServiceResult DeleteTokenByToken(string token);
    }
}
