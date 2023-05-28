using API.Models;
using API.ViewModels.Accounts;
using API.ViewModels.Login;

namespace API.Contracts
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        LoginVM Login(LoginVM loginVM);
        public Account GetByEmployeeId(Guid? employeeId);
        public int UpdateOTP(Guid? employeeId);
        int Register(RegisterVM registerVM);

        int ChangePasswordAccount(Guid? employeeId, ChangePasswordVM changePasswordVM);
    }
}
