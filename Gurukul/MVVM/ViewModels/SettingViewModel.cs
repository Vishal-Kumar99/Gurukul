
using Gurukul.Core;
using Gurukul.MVVM.Views;
using Gurukul.Services;
using System.Windows;

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

    private string _selectedSidebarBehavior;
    public string SelectedSidebarBehavior
    {
        get => _selectedSidebarBehavior;
        set
        {
            _selectedSidebarBehavior = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand OpenAcademicYearCommand { get; }
    public RelayCommand SaveSettingsCommand { get; }

    public SettingViewModel(AcademicYearViewModel academicYearViewModel)
    {
        RefreshActiveYear();
        OpenAcademicYearCommand = new RelayCommand(_ => OpenAcademicYearWindow());

        SelectedSidebarBehavior = AppState.Settings.SidebarBehavior;
        SaveSettingsCommand = new RelayCommand(_ => SaveSettings());

        _academicYearVM = academicYearViewModel;
    }

    private void SaveSettings()
    {
        AppState.Settings.SidebarBehavior = SelectedSidebarBehavior;
        SettingsService.SaveSettings(AppState.Settings);

        AppState.NotifySidebarBehaviorChanged();

        MessageBox.Show("Saved", "User Prferences", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public void RefreshActiveYear()
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
