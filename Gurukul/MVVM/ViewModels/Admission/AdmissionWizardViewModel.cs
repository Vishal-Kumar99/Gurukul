
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Gurukul.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Gurukul.MVVM.ViewModels.Admission;

public class AdmissionWizardViewModel : ViewModel, INavigationGuard
{
    public bool IsFinalStep => CurrentStep?.IsFinalStep == true;

    public ObservableCollection<AdmissionStep> Steps { get; }
    public ObservableCollection<StepViewModelBase> StepViewModels { get; }

    private int _currentStepIndex = 0;
    public StepViewModelBase CurrentStep
    {
        get => StepViewModels[_currentStepIndex];
    }

    private readonly INavigationService _navigation;

    public RelayCommand CancelCommand { get; }
    public RelayCommand NextCommand { get; }
    public RelayCommand BackCommand { get; }

    public int AdmissionId { get; private set; }

    public AdmissionWizardViewModel(INavigationService navigation)
    {
        _navigation = navigation;

        AppState.IsAdmissionInProgress = true;

        CancelCommand = new RelayCommand(_ => CancelAdmission());

        Steps = new()
        {
            new() { Title = "Student Basic Info", Icon = "\uE76C" },
            new() { Title = "Parent Info", Icon = "\uE76C" },
            new() { Title = "Address", Icon = "\uE76C" },
            new() { Title = "Academics", Icon = "\uE76C" },
            new() { Title = "Health Info", Icon = "\uE76C" },
            new() { Title = "Photo", Icon = "\uE76C" },
            new() { Title = "FinalSubmit", Icon = "\uE76C" }
            //new() { Title = "ID Card", Icon = "\uE76C" }
        };

        StepViewModels = new()
        {
            new StudentBasicVM(),
            new ParentDetailsVM(),
            new AddressVM(),
            new AcademicDetailsVM(),
            new HealthDetailsVM(),
            new StudentPhotoVM(),
            new FinalSubmitVM()
            //new IdCardPreviewVM()
        };

        NextCommand = new RelayCommand(_ => Next(), _ => CurrentStep?.IsValid == true);

        BackCommand = new RelayCommand(_ => Back(), _ => _currentStepIndex > 0);

        ResetWizard();
        SetActiveStep(0);
    }

    public bool CanNavigateAway()
    {
        if (!AppState.IsAdmissionInProgress) return true;

        var result = MessageBox.Show(
            "You have an ongoing admission.\n\n" +
            "Do you want to cancel this admission?",
            "Cancel Admission",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
            return false;

        CancelAdmissionInternal();
        return true;
    }

    private void CancelAdmissionInternal()
    {
        AppState.IsAdmissionInProgress = false;

        AppState.AdmissionDraft = new AdmissionDraft();

        ResetWizard();
    }

    private void ResetWizard()
    {
        _currentStepIndex = 0;

        for (int i = 0; i < Steps.Count; i++)
            Steps[i].IsActive = i == 0;

        _previousStep = null;

        OnPropertyChanged(nameof(CurrentStep));
        OnPropertyChanged(nameof(IsFinalStep));

        NextCommand.RaiseCanExecuteChanged();
        BackCommand.RaiseCanExecuteChanged();
    }

    private void CancelAdmission()
    {
        var result = MessageBox.Show(
            "Are you sure you want to cancel this admission?\nAll entered data will be lost.",
            "Cancel Admission",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
            return;

        AppState.AdmissionDraft = new AdmissionDraft();

        _navigation.NavigateTo<HomeViewModel>();
    }

    private void Next()
    {
        try
        {
            CurrentStep.SaveDraft(AdmissionId);
        }
        catch
        {
            return;
        }

        if (!CurrentStep.IsFinalStep && _currentStepIndex < StepViewModels.Count - 1)
        {
            SetActiveStep(_currentStepIndex + 1);
        }
    }

    private void Back()
    {
        if (_currentStepIndex > 0)
        {
            SetActiveStep(_currentStepIndex - 1);
        }
    }

    private StepViewModelBase? _previousStep;

    private void SetActiveStep(int index)
    {
        if (_previousStep != null)
            _previousStep.PropertyChanged -= OnCurrentStepPropertyChanged;

        _currentStepIndex = index;

        for (int i = 0; i < Steps.Count; i++)
        {
            Steps[i].IsActive = i == index;
        }

        _previousStep = CurrentStep;
        _previousStep.PropertyChanged += OnCurrentStepPropertyChanged;

        OnPropertyChanged(nameof(CurrentStep));
        OnPropertyChanged(nameof(IsFinalStep));

        NextCommand.RaiseCanExecuteChanged();
        BackCommand.RaiseCanExecuteChanged();
    }

    private void OnCurrentStepPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(StepViewModelBase.IsValid))
        {
            NextCommand.RaiseCanExecuteChanged();
        }
    }
}
