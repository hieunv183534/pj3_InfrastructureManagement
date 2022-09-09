using InfrastructureManagement.Core.Dtos;
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
    public class TokenAccountService : BaseService<TokenAccount>, ITokenAccountService
    {
        ITokenAccountRepository _tokenAccountRepository;

        public TokenAccountService(IBaseRepository<TokenAccount> baseRepository, ITokenAccountRepository tokenAccountRepository) : base(baseRepository)
        {
            _tokenAccountRepository = tokenAccountRepository;
        }

        public ServiceResult DeleteTokenByToken(string token)
        {
            try
            {
                int rowAffect = _tokenAccountRepository.DeleteTokenByToken(token);
                _serviceResult.Response = new ResponseModel(2001, "OK", rowAffect);
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult DeleteTokenByUsername(string username)
        {
            try
            {
                int rowAffect = _tokenAccountRepository.DeleteTokenByUsername(username);
                _serviceResult.Response = new ResponseModel(2001, "OK", rowAffect);
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
            catch (Exception ex)
            {
                _serviceResult.Response = new ResponseModel(9999, "Exception Error", new { msg = ex.Message });
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }

        public ServiceResult GetTokenByUsername(string username)
        {
            try
            {
                var tokenAccount = _tokenAccountRepository.GetTokenByUsername(username);
                if (tokenAccount != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "OK", tokenAccount);
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.Response = new ResponseModel(2004, "No data or end of list data");
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
    }
}
