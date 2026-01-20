
namespace Gurukul.MVVM.Models;

public class FeeStructure
{
    public int FeeStructureId { get; set; }
    public int ClassId { get; set; }
    public int AcademicYearId { get; set; }
    public decimal TuitionFee { get; set; }
    public decimal ExamFee { get; set; }
    public decimal MiscFee { get; set; }
    public decimal TotalFee { get; set; }
    public bool IsActive { get; set; }
}

