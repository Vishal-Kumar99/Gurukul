
namespace Gurukul.MVVM.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string FullName { get; set; }
    public string MobileNo { get; set; }
    public int? ClassTeacherOfClassId { get; set; }
}
