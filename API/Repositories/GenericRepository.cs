using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        protected readonly BookingManagementDbContext _context;
        public GenericRepository(BookingManagementDbContext context)
        {
            _context = context;
        }


        /*
         * <summary>
         * Create a new university
         * </summary>
         * <param name="university">University object</param>
         * <returns>University object</returns>
         */
        public T? Create(T item)
        {
            try
            {
                typeof(T).GetProperty("CreatedDate")!
                         .SetValue(item,DateTime.Now);
                typeof(T).GetProperty("ModifiedDate")!
                      .SetValue(item, DateTime.Now);

                _context.Set<T>().Add(item);
                _context.SaveChanges();
                return item;
            }
            catch
            {
                return default(T);
            }
        }

        /*
         * <summary>
         * Update a university
         * </summary>
         * <param name="university">University object</param>
         * <returns>true if data updated</returns>
         * <returns>false if data not updated</returns>
         */
        public bool Update(T item)
        {
            try
            {
                var guid = (Guid)typeof(T).GetProperty("Guid")!.GetValue(item)!;
                var oldEntity = GetByGuid(guid);
                if (oldEntity == null) {
                    return false;
                }
                var getCreatedDate = typeof(T).GetProperty("CreatedDate")!.GetValue(oldEntity)!;
                
                typeof(T).GetProperty("CreatedDate")!.SetValue(item, getCreatedDate);
                typeof(T).GetProperty("ModifiedDate")!.SetValue(item, DateTime.Now);

                _context.Set<T>().Update(item);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * <summary>
         * Delete a university
         * </summary>
         * <param name="guid">University guid</param>
         * <returns>true if data deleted</returns>
         * <returns>false if data not deleted</returns>
         */
        public bool Delete(Guid guid)
        {
            try
            {
                var item = GetByGuid(guid);
                if (item == null)
                {
                    return false;
                }

                _context.Set<T>().Remove(item);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * <summary>
         * Get all universities
         * </summary>
         * <returns>List of universities</returns>
         * <returns>Empty list if no data found</returns>
         */
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        /*
         * <summary>
         * Get a university by guid
         * </summary>
         * <param name="guid">University guid</param>
         * <returns>University object</returns>
         * <returns>null if no data found</returns>
         */
        public T? GetByGuid(Guid guid)
        {
            var entity = _context.Set<T>().Find(guid);
            _context.ChangeTracker.Clear();
            return entity;
        }
    }
}
