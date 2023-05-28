
using API.ViewModels.Rooms;
using API.Models;

namespace API.Contracts
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        IEnumerable<MasterRoomVM> GetByDate(DateTime dateTime);
        IEnumerable<RoomUsedVM> GetCurrentlyUsedRooms();

        IEnumerable<RoomBookedTodayVM> GetAvailableRoom();
    }
}
