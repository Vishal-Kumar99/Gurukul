
namespace Gurukul.MVVM.Models;

public class TransferCertificate
{
    public int TCId { get; set; }
    public int StudentId { get; set; }
    public string TCNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public string Reason { get; set; }
    public string LastClassStudied { get; set; }
    public string Conduct { get; set; }
    public bool IsMigrated { get; set; }
}
