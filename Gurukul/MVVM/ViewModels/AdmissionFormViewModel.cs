
using Gurukul.Core;
using Gurukul.Services;

namespace Gurukul.MVVM.ViewModels;

public class AdmissionFormViewModel : Core.ViewModel
{
    private INavigationService _navigation;

    public INavigationService Navigation 
    { 
        get => _navigation; 
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToSettingView { get; set; }

    public AdmissionFormViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToSettingView = new RelayCommand(execute:obj => { Navigation.NavigateTo<SettingViewModel>(); }, canExecute:obj => true);
    }
}
