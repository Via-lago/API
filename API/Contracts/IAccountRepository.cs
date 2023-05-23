using API.Models;

namespace API.Contracts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Account GetByEmployeeId(Guid? employeeId);
        public int UpdateOTP(Guid? employeeId);
    }
}
