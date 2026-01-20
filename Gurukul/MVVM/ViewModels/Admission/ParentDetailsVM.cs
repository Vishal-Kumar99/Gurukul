
using Gurukul.Core;

namespace Gurukul.MVVM.ViewModels.Admission;

public class ParentDetailsVM : StepViewModelBase
{
    public override bool IsFinalStep => false;
    public override string StepName => "Parent Details";

    private string _fatherName;
    public string FatherName
    {
        get => _fatherName;
        set
        {
            _fatherName = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _motherName;
    public string MotherName
    {
        get => _motherName;
        set
        {
            _motherName = value;
            OnPropertyChanged();
        }
    }

    private string _mobileNo;
    public string MobileNo
    {
        get => _mobileNo;
        set
        {
            _mobileNo = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public ParentDetailsVM()
    {
        var draft = AppState.AdmissionDraft;

        FatherName = draft.FatherName;
        MotherName = draft.MotherName;
        MobileNo = draft.MobileNo;
        Email = draft.Email;

        Validate();
    }

    public override void Validate()
    {
        Errors.Clear();

        if (string.IsNullOrWhiteSpace(FatherName))
            Errors.Add("Father name is required.");

        if (string.IsNullOrWhiteSpace(MobileNo))
            Errors.Add("Mobile number is required.");
        else if (MobileNo.Length != 10)
            Errors.Add("Mobile number must be 10 digits.");

        OnPropertyChanged(nameof(IsValid));
    }

    public override void SaveDraft(int admissionId)
    {
        var draft = AppState.AdmissionDraft;

        draft.FatherName = FatherName;
        draft.MotherName = MotherName;
        draft.MobileNo = MobileNo;
        draft.Email = Email;
    }
}
