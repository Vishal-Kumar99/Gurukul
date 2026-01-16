
namespace Gurukul.MVVM.Models;

public class StudentAttendance
{
    public int AttendanceId { get; set; }
    public int StudentId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public bool IsPresent { get; set; }
}
