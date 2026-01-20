
using Gurukul.Core;

namespace Gurukul.Services;

public interface INavigationService
{
    ViewModel CurrentView { get; }
    void NavigateTo<T>() where T : ViewModel;
    void NavigateTo(Type viewModelType);
}

public interface INavigationGuard
{
    bool CanNavigateAway();
}

public class NavigationService : ObservableObject, INavigationService
{
    public Func<Type, ViewModel> _viewModelFactory { get; }
    private ViewModel _currentView;

    public ViewModel CurrentView 
    { 
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModel
    {
        if (_currentView is INavigationGuard guard)
        {
            if (!guard.CanNavigateAway())
                return;
        }

        ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }

    public void NavigateTo(Type viewModelType)
    {
        if (_currentView is INavigationGuard guard)
        {
            if (!guard.CanNavigateAway())
                return;
        }

        ViewModel viewModel = _viewModelFactory.Invoke(viewModelType);
        CurrentView = viewModel;
    }
}
