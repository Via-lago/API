using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Rooms
{
    public class RoomBookedTodayVM
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
