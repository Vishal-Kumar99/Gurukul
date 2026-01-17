
using Gurukul.Core;
using Gurukul.MVVM.Views;
using Gurukul.Services;

namespace Gurukul.MVVM.ViewModels;

public class SettingViewModel : Core.ViewModel
{
    private readonly AcademicYearViewModel _academicYearVM;

    private string _activeAcademicYear;
    public string ActiveAcademicYear
    {
        get => _activeAcademicYear;
        set
        {
            _activeAcademicYear = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand OpenAcademicYearCommand { get; }

    public SettingViewModel(AcademicYearViewModel academicYearViewModel)
    {
        RefreshActiveYear();
        OpenAcademicYearCommand = new RelayCommand(_ => OpenAcademicYearWindow());
        _academicYearVM = academicYearViewModel;
    }

    private void RefreshActiveYear()
    {
        ActiveAcademicYear = AppState.CurrentAcademicYear?.YearName ?? "Not Set";
    }

    private void OpenAcademicYearWindow()
    {
        var win = new AcademicYearView();
        win.DataContext = _academicYearVM;
        win.ShowDialog();
        RefreshActiveYear();
    }
}
