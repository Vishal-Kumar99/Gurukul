
using Gurukul.Core;

namespace Gurukul.MVVM.ViewModels.Admission;

public class AddressVM : StepViewModelBase
{
    public override bool IsFinalStep => false;
    public override string StepName => "Address Details";

    private string _addressLine;
    public string AddressLine
    {
        get => _addressLine;
        set
        {
            _addressLine = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _city;
    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _state;
    public string State
    {
        get => _state;
        set
        {
            _state = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _pincode;
    public string Pincode
    {
        get => _pincode;
        set
        {
            _pincode = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _landmark;
    public string Landmark
    {
        get => _landmark;
        set
        {
            _landmark = value;
            OnPropertyChanged();
        }
    }

    public AddressVM()
    {
        var draft = AppState.AdmissionDraft;

        AddressLine = draft.AddressLine;
        City = draft.City;
        State = draft.State;
        Pincode = draft.Pincode;
        Landmark = draft.Landmark;

        Validate();
    }

    public override void Validate()
    {
        Errors.Clear();

        if (string.IsNullOrWhiteSpace(AddressLine))
            Errors.Add("Address is required.");

        if (string.IsNullOrWhiteSpace(City))
            Errors.Add("City is required.");

        if (string.IsNullOrWhiteSpace(State))
            Errors.Add("State is required.");

        if (string.IsNullOrWhiteSpace(Pincode))
            Errors.Add("Pincode is required.");
        else if (Pincode.Length != 6 || !Pincode.All(char.IsDigit))
            Errors.Add("Pincode must be 6 digits.");

        OnPropertyChanged(nameof(IsValid));
    }

    public override void SaveDraft(int admissionId)
    {
        var draft = AppState.AdmissionDraft;

        draft.AddressLine = AddressLine;
        draft.City = City;
        draft.State = State;
        draft.Pincode = Pincode;
        draft.Landmark = Landmark;
    }
}
