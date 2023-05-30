using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Rooms
{
    public class RoomUsedVM
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required] 
        public string BookedBy { get; set; }
    }
}
