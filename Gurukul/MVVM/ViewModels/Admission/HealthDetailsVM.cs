
using Gurukul.Core;
using System.Collections.ObjectModel;

namespace Gurukul.MVVM.ViewModels.Admission;

public class HealthDetailsVM : StepViewModelBase
{
    public override bool IsFinalStep => false;
    public override string StepName => "Health Details";

    // Common blood groups
    public ObservableCollection<string> BloodGroups { get; } = new()
    {
        "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
    };

    private string _bloodGroup;
    public string BloodGroup
    {
        get => _bloodGroup;
        set
        {
            _bloodGroup = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _medicalCondition;
    public string MedicalCondition
    {
        get => _medicalCondition;
        set
        {
            _medicalCondition = value;
            OnPropertyChanged();
        }
    }

    private string _allergyDetails;
    public string AllergyDetails
    {
        get => _allergyDetails;
        set
        {
            _allergyDetails = value;
            OnPropertyChanged();
        }
    }

    private bool _hasDisability;
    public bool HasDisability
    {
        get => _hasDisability;
        set
        {
            _hasDisability = value;
            OnPropertyChanged();
        }
    }

    public HealthDetailsVM()
    {
        var draft = AppState.AdmissionDraft;

        BloodGroup = draft.BloodGroup;
        MedicalCondition = draft.MedicalCondition;
        AllergyDetails = draft.AllergyDetails;
        HasDisability = draft.HasDisability;

        Validate();
    }

    public override void Validate()
    {
        Errors.Clear();

        if (string.IsNullOrWhiteSpace(BloodGroup))
            Errors.Add("Blood group is required.");

        OnPropertyChanged(nameof(IsValid));
    }

    public override void SaveDraft(int admissionId)
    {
        var draft = AppState.AdmissionDraft;

        draft.BloodGroup = BloodGroup;
        draft.MedicalCondition = MedicalCondition;
        draft.AllergyDetails = AllergyDetails;
        draft.HasDisability = HasDisability;
    }
}
