
namespace Gurukul.MVVM.Models;

public class Subject
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public string SubjectCode { get; set; }
    public int ClassId { get; set; }
    public bool IsActive { get; set; }
}
