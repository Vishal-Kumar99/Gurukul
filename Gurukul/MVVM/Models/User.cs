
namespace Gurukul.MVVM.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
}
