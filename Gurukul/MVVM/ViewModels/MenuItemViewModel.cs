
using Gurukul.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gurukul.MVVM.ViewModels;

public class MenuItemViewModel : Core.ViewModel
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public ObservableCollection<MenuItemViewModel> Options { get; set; }

    public bool HasOptions => Options != null && Options.Count > 0;

    private bool _isExpanded;
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            _isExpanded = value;
            OnPropertyChanged();
        }
    }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }

    public Type TargetViewModel { get; set; }

    public RelayCommand ToggleCommand { get; set; }
    
}
