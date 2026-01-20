
using Gurukul.Core;

namespace Gurukul.MVVM.ViewModels.Admission;

public class AcademicDetailsVM : StepViewModelBase
{
    public override bool IsFinalStep => false;
    public override string StepName => "Academic Details";

    private string _previousSchool;
    public string PreviousSchool
    {
        get => _previousSchool;
        set
        {
            _previousSchool = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _lastClassStudied;
    public string LastClassStudied
    {
        get => _lastClassStudied;
        set
        {
            _lastClassStudied = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private string _board;
    public string Board
    {
        get => _board;
        set
        {
            _board = value;
            OnPropertyChanged();
        }
    }

    private string _result;
    public string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    private string _remarks;
    public string Remarks
    {
        get => _remarks;
        set
        {
            _remarks = value;
            OnPropertyChanged();
        }
    }

    public AcademicDetailsVM()
    {
        var draft = AppState.AdmissionDraft;

        PreviousSchool = draft.PreviousSchool;
        LastClassStudied = draft.LastClassStudied;
        Board = draft.Board;
        Result = draft.Result;
        Remarks = draft.Remarks;

        Validate();
    }

    public override void Validate()
    {
        Errors.Clear();

        if (string.IsNullOrWhiteSpace(PreviousSchool))
            Errors.Add("Previous school name is required.");

        if (string.IsNullOrWhiteSpace(LastClassStudied))
            Errors.Add("Last class studied is required.");

        OnPropertyChanged(nameof(IsValid));
    }

    public override void SaveDraft(int admissionId)
    {
        var draft = AppState.AdmissionDraft;

        draft.PreviousSchool = PreviousSchool;
        draft.LastClassStudied = LastClassStudied;
        draft.Board = Board;
        draft.Result = Result;
        draft.Remarks = Remarks;
    }
}
