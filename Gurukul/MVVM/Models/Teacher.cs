
namespace Gurukul.MVVM.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string FullName { get; set; }
    public string MobileNo { get; set; }
    public string Qualification { get; set; }
    public int ExperienceYears { get; set; }
    public string AadhaarNo { get; set; }
    public string PAN { get; set; }
    public DateTime JoiningDate { get; set; }
    public int? ClassTeacherOfClassId { get; set; }
}
