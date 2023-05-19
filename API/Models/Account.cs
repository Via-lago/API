using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("otp")]
        public int Otp { get; set; }
        [Column("is_used")]
        public bool IsUsed { get; set; }
        [Column("expired_date")]
        public DateTime ExpiredDate { get; set; }

        //cardinality
        public Employee? Employee { get; set; }
        public ICollection<AccountRole>? accountRoles { get; set; }

        
        }
}
