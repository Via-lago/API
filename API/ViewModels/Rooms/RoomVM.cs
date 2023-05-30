using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Rooms;

public class RoomVM
{
    public Guid? Guid { get; set; }
    
    [Required]
    public string Name { get; set; }
    [Required]
    public int Floor { get; set; }
    [Required]
    public int Capacity { get; set; }
}
