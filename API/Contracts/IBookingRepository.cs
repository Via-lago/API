using API.Models;
using API.ViewModels.Bookings;

namespace API.Contracts
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        IEnumerable<BookingDurationVM> GetBookingDuration();

        // Kelompok 4
        IEnumerable<BookingDetailVM> GetAllBookingDetail();
        BookingDetailVM GetBookingDetailByGuid(Guid guid);
    }
}
