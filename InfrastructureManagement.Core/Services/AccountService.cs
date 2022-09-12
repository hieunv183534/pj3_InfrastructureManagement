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
    public class AccountService : BaseService<Account>, IAccountService
    {
        IAccountRepository _accountRepository;

        public AccountService(IBaseRepository<Account> baseRepository, IAccountRepository accountRepository) : base(baseRepository)
        {
            _accountRepository = accountRepository;
        }

        public ServiceResult GetAccountByUsername(string username)
        {
            try
            {
                var account = _accountRepository.GetAccountByUsername(username);
                if (account != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "OK", account);
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

        public async Task<ServiceResult> GetOverView()
        {
            try
            {
                var overview = await _accountRepository.GetOverView();
                if (overview != null)
                {
                    _serviceResult.Response = new ResponseModel(2000, "OK", overview);
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
