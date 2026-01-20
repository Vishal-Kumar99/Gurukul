
namespace Gurukul.MVVM.Models;

public class StudentPromotion
{
    public int PromotionId { get; set; }
    public int StudentId { get; set; }
    public int FromClassId { get; set; }
    public int ToClassId { get; set; }
    public int FromAcademicYearId { get; set; }
    public int ToAcademicYearId { get; set; }
    public DateTime PromotionDate { get; set; }
}
