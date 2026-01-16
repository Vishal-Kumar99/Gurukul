
namespace Gurukul.MVVM.Models;

public class FeePayment
{
    public int PaymentId { get; set; }
    public int StudentId { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}
