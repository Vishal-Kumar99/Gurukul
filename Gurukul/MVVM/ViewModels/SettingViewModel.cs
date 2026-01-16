
using Gurukul.Core;
using Gurukul.Services;

namespace Gurukul.MVVM.ViewModels;

public class SettingViewModel : Core.ViewModel
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

    public RelayCommand NavigateToHomeView { get; set; }

    public SettingViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToHomeView = new RelayCommand(execute:obj => { Navigation.NavigateTo<HomeViewModel>(); }, canExecute:obj => true);
    }
}
