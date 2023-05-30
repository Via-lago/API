using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Repositories;
using API.ViewModels.Employees;

namespace API.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public Guid? FindGuidByEmail(string email);
        IEnumerable<MasterEmployeeVM> GetAllMasterEmployee();
        MasterEmployeeVM? GetMasterEmployeeByGuid(Guid guid);

        bool CheckEmailAndPhoneAndNIK(string value);
        Employee GetByEmail(string email);
    }
}
