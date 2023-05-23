using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Guid? FindGuidByEmail(string email);
    }
}
