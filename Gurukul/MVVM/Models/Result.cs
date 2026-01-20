
namespace Gurukul.MVVM.Models;

public class Result
{
    public int ResultId { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public int SubjectId { get; set; }
    public decimal MarksObtained { get; set; }
    public decimal MaxMarks { get; set; }
}
