
namespace Gurukul.MVVM.Models;

public class Student
{
    public int StudentId { get; set; }
    public string AdmissionNo { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }

    public string AadhaarNo { get; set; }
    public string Religion { get; set; }
    public string CasteCategory { get; set; }
    public string BloodGroup { get; set; }
    public string Nationality { get; set; }
    public string MotherTongue { get; set; }
    public string PreviousSchool { get; set; }

    public int ClassId { get; set; }
    public int SectionId { get; set; }
    public int ParentId { get; set; }
    public int AcademicYearId { get; set; }

    public bool IsTransferred { get; set; }
    public bool IsTCIssued { get; set; }
}
