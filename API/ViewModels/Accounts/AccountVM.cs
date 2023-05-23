using API.Models;

namespace API.ViewModels.Accounts;

public class AccountVM
{
    public Guid? Guid { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }
}
