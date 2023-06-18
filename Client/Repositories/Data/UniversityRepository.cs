using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;

namespace Client.Repositories.Data
{
    public class UniversityRepository : GeneralRepository<University, Guid>, IUniversityRepository
    {


        public UniversityRepository(string request = "University/") : base(request)
        {

        }

    }
}