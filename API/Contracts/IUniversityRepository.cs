using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IBaseRepository<University>
{
    IEnumerable<University> GetByName(string name);
    University CreateWithValidate(University university);
}