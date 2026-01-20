
namespace Gurukul.MVVM.ViewModels.Admission;

public class IdCardPreviewVM : StepViewModelBase
{
    public string StudentName { get; set; }
    public DateTime DOB { get; set; }
    public string Gender { get; set; }
    public override bool IsFinalStep => false;

    public override string StepName => "Student Details";

    public override void Validate()
    {

    }

    public override void SaveDraft(int admissionId)
    {
        // INSERT or UPDATE Student table
    }
}
