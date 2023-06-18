using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class Account
    {
        [Required]
        public string Password { get; set; }
       
        [Required]
        public bool IsDeleted { get; set; }
        
        [Required]
        public int Otp { get; set; }
        
        public bool IsUsed { get; set; }

        public DateTime ExpiredTime { get; set; }
    }
}
