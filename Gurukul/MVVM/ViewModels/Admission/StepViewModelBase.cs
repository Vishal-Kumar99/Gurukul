
using Gurukul.Core;
using System.Collections.ObjectModel;

namespace Gurukul.MVVM.ViewModels.Admission;

public abstract class StepViewModelBase : ViewModel
{
    public ObservableCollection<string> Errors { get; } = new();

    public bool IsValid => Errors.Count == 0;
    public abstract bool IsFinalStep { get; }

    public abstract string StepName { get; }
    public abstract void Validate();
    public abstract void SaveDraft(int admissionId);
}
