
using Gurukul.Core;
using Gurukul.Services;

namespace Gurukul.MVVM.ViewModels;

public class MainViewModel : Core.ViewModel
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

    public SidebarViewModel SidebarVM { get; }

    public RelayCommand NavigateToHomeCommand { get; set; }
    public RelayCommand NavigateToSettingViewCommand { get; set; }

    public MainViewModel(INavigationService navService)
    {
        Navigation = navService;
        NavigateToHomeCommand = new RelayCommand(execute:obj => { Navigation.NavigateTo<HomeViewModel>(); }, canExecute:obj => true);
        NavigateToSettingViewCommand = new RelayCommand(execute:obj => { Navigation.NavigateTo<SettingViewModel>(); }, canExecute:obj => true);

        SidebarVM = new SidebarViewModel(Navigation);
    }

    
}
