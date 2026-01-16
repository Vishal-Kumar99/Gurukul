
using Gurukul.Core;
using Gurukul.Services;

namespace Gurukul.MVVM.ViewModels;

public class AddClassViewModel : ViewModel
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

    public AddClassViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        NavigateToSettingView = new RelayCommand(execute:obj => { Navigation.NavigateTo<SettingViewModel>(); }, canExecute:obj => true);
    }
}
