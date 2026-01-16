
namespace Gurukul.MVVM.Models;

public class TeacherAttendance
{
    public int AttendanceId { get; set; }
    public int TeacherId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public bool IsPresent { get; set; }
}
