using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories;
using API.ViewModels.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Guid? FindGuidByEmail(string email);
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);
        int CreateWithValidate(Employee employee);
    }
}
