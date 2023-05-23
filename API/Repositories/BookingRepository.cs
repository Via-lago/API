using API.Contexts;
using API.Contracts;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingManagementDbContext context) : base(context) { }
    }
}
