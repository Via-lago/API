using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IEmployeRepository : IRepository<Employee, Guid>
    {
        public Task<ResponseListVM<GetAllEmployee>>GetAll();
    }
}
