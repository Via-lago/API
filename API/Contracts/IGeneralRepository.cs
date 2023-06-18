using API.Models;
namespace API.Contracts
{
    public interface IGeneralRepository<T>
    {
        T? Create(T item);
        bool Update(T item);
        bool Delete(Guid guid);
        IEnumerable<T> GetAll();
        T? GetByGuid(Guid guid);
    }
}
 