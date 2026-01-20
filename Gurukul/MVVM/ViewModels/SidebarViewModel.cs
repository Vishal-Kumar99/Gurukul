
using Gurukul.Core;
using Gurukul.MVVM.ViewModels.Admission;
using Gurukul.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Gurukul.MVVM.ViewModels;

public class SidebarViewModel : Core.ViewModel
{
    private readonly INavigationService _navigation;

    private bool _isCollapsed;
    public bool IsCollapsed
    {
        get => _isCollapsed;
        set
        {
            if (_isCollapsed == value)
                return;

            _isCollapsed = value;
            OnPropertyChanged();

            IsFlyoutOpen = false;

            if (Items != null)
            {
                foreach (var item in Items)
                {
                    item.IsExpanded = false;
                    item.IsSelected = false;
                }
            }
        }
    }

    private MenuItemViewModel _flyoutItem;
    public MenuItemViewModel FlyoutItem
    {
        get => _flyoutItem;
        set
        {
            _flyoutItem = value;
            OnPropertyChanged();
        }
    }

    private bool _isFlyoutOpen;
    public bool IsFlyoutOpen
    {
        get => _isFlyoutOpen;
        set
        {
            _isFlyoutOpen = value;
            OnPropertyChanged();
        }
    }

    private UIElement _popupTarget;
    public UIElement PopupTarget
    {
        get => _popupTarget;
        set
        {
            _popupTarget = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<MenuItemViewModel> Items { get; set; } = new();

    public RelayCommand ToggleSidebarCommand { get; }
    public RelayCommand SubItemClickCommand { get; }

    public SidebarViewModel(INavigationService navigation)
    {
        _navigation = navigation;

        BuildSidebar();

        IsCollapsed = AppState.Settings.SidebarBehavior == "Collapsed";

        ToggleSidebarCommand = new RelayCommand(obj =>
        {
            IsCollapsed = !IsCollapsed;

            AppState.Settings.SidebarBehavior = IsCollapsed ? "Collapsed" : "Expanded";
            SettingsService.SaveSettings(AppState.Settings);
        });

        SubItemClickCommand = new RelayCommand(obj =>
        {
            if (obj is not MenuItemViewModel item)
                return;
            if (item.TargetViewModel != null)
                _navigation.NavigateTo(item.TargetViewModel);

            if (IsCollapsed)
            {
                IsFlyoutOpen = false;
            }
        });


        AppState.SidebarBehaviorChanged += OnSidebarBehaviorChanged;
    }

    private void OnSidebarBehaviorChanged()
    {
        IsCollapsed = AppState.Settings.SidebarBehavior == "Collapsed";
    }

    private void BuildSidebar()
    {
        Items.Clear();

        Items.Add(new MenuItemViewModel
        {
            Title = "Dashboard",
            Icon = "\uE80F",
            TargetViewModel = typeof(HomeViewModel)
        });

        Items.Add(CreateItem("Students", "\uE77B",
            ("Admission Form", typeof(AdmissionWizardViewModel)),
            ("List Students", typeof(HomeViewModel)),
            ("Student Categories", typeof(HomeViewModel)),
            ("Student House", typeof(HomeViewModel)),
            ("Student Activity", typeof(HomeViewModel)),
            ("Parents", typeof(HomeViewModel))));

        Items.Add(CreateItem("Teachers", "\uE716",
            ("Add Teacher", typeof(HomeViewModel)),
            ("List Teachers", typeof(HomeViewModel)),
            ("Teacher Salary", typeof(HomeViewModel))));

        Items.Add(CreateItem("Class", "\uE7BE",
            ("Add Class", typeof(AddClassViewModel)),
            ("Class Routine", typeof(HomeViewModel)),
            ("Class Information", typeof(HomeViewModel))));

        Items.Add(CreateItem("Attendance", "\uE73E",
            ("Student Attendance", typeof(HomeViewModel)),
            ("Teacher Attendance", typeof(HomeViewModel))));

        Items.Add(CreateItem("Subjects", "\uE8F1",
            ("Add Subjects", typeof(HomeViewModel)),
            ("Assign Subject", typeof(HomeViewModel))));

        Items.Add(CreateItem("Examination", "\uE70F",
            ("Add Examination", typeof(HomeViewModel)),
            ("Exam Schedule", typeof(HomeViewModel)),
            ("Question Paper", typeof(HomeViewModel)),
            ("Marks Entry", typeof(HomeViewModel))));

        Items.Add(CreateItem("Fee Collection", "\uE8C7",
            ("Collect Fee", typeof(HomeViewModel)),
            ("Invoice", typeof(HomeViewModel)),
            ("Fee Structure", typeof(HomeViewModel)),
            ("Due Fee", typeof(HomeViewModel))));

        Items.Add(CreateItem("Transportation", "\uE804",
            ("Transport", typeof(HomeViewModel)),
            ("Transport Route", typeof(HomeViewModel)),
            ("Manage Vehicle", typeof(HomeViewModel))));

        Items.Add(new MenuItemViewModel
        {
            Title = "Notice",
            Icon = "\uE7ED",
            TargetViewModel = typeof(HomeViewModel)
        });

        Items.Add(new MenuItemViewModel
        {
            Title = "Roles",
            Icon = "\uE192",
            TargetViewModel = typeof(HomeViewModel)
        });

        Items.Add(CreateItem("Settings", "\uE713",
            ("General", typeof(SettingViewModel)),
            ("About", typeof(HomeViewModel))));

        //Items = new ObservableCollection<MenuItemViewModel>
        //{
        //    new MenuItemViewModel
        //    {
        //        Title = "Dashboard",
        //        Icon = "\uE80F",
        //        TargetViewModel = typeof(HomeViewModel)
        //    },

        //    CreateItem("Students", "\uE77B",
        //        ("Admission Form", typeof(AdmissionFormViewModel)), ("List Students", typeof(HomeViewModel)), ("Student Categories", typeof(HomeViewModel)), ("Student House", typeof(HomeViewModel)), ("Student Activity", typeof(HomeViewModel)), ("Parents", typeof(HomeViewModel))),

        //    CreateItem("Teachers", "\uE716",
        //        ("Add Teacher", typeof(HomeViewModel)), ("List Teachers", typeof(HomeViewModel)), ("Teacher Salary", typeof(HomeViewModel))),

        //    CreateItem("Class", "\uE7BE",
        //        ("Add Class", typeof(AddClassViewModel)), ("Class Routine", typeof(HomeViewModel)), ("Class Information", typeof(HomeViewModel))),

        //    CreateItem("Attendance", "\uE73E",
        //        ("Student Attendance", typeof(HomeViewModel)), ("Teacher Attendance", typeof(HomeViewModel))),

        //    CreateItem("Subjects", "\uE8F1",
        //        ("Add Subjects", typeof(HomeViewModel)), ("Assign Subject", typeof(HomeViewModel))),

        //    CreateItem("Examination", "\uE70F",
        //        ("Add Examination", typeof(HomeViewModel)), ("Exam Schedule", typeof(HomeViewModel)), ("Question Paper", typeof(HomeViewModel)), ("Marks Entry", typeof(HomeViewModel))),

        //    CreateItem("Fee Collection", "\uE8C7",
        //        ("Collect Fee", typeof(HomeViewModel)), ("Invoice", typeof(HomeViewModel)), ("Fee Structure", typeof(HomeViewModel)), ("Due Fee", typeof(HomeViewModel))),

        //    CreateItem("Transportation", "\uE804",
        //        ("Transport", typeof(HomeViewModel)), ("Transport Route", typeof(HomeViewModel)), ("Manage Vehicle", typeof(HomeViewModel))),

        //    new MenuItemViewModel
        //    {
        //        Title = "Notice",
        //        Icon = "\uE7ED",
        //        TargetViewModel = typeof(HomeViewModel)
        //    },

        //    new MenuItemViewModel
        //    {
        //        Title = "Roles",
        //        Icon = "\uE192",
        //        TargetViewModel = typeof(HomeViewModel)
        //    },

        //    CreateItem("Settings", "\uE713",
        //        ("General", typeof(SettingViewModel)), ("About", typeof(HomeViewModel)))
        //};

        foreach (var item in Items)
        {
            item.ToggleCommand = new RelayCommand(obj => Toggle(obj));
        }
    }

    private MenuItemViewModel CreateItem(string title, string icon, params (string title, Type target)[] subItems)
    {
        var item = new MenuItemViewModel
        {
            Title = title,
            Icon = icon,
            Options = new ObservableCollection<MenuItemViewModel>()
        };

        foreach (var sub in subItems)
        {
            item.Options.Add(new MenuItemViewModel 
            { 
                Title = sub.title,
                TargetViewModel = sub.target
            });
        }
        return item;
    }

    private void Toggle(object parameter)
    {
        if (parameter is not FrameworkElement element)
            return;

        if (element.DataContext is not MenuItemViewModel selected)
            return;

        PopupTarget = element;

        if (!selected.HasOptions)
        {
            if (selected.TargetViewModel != null)
                _navigation.NavigateTo(selected.TargetViewModel);

            SelectOnly(selected);
            IsFlyoutOpen = false;
            return;
        }

        if (IsCollapsed && selected.HasOptions)
        {
            FlyoutItem = selected;
            IsFlyoutOpen = true;
            return;
        }

        foreach (var item in Items)
        {
            if (item == selected)
            {
                item.IsExpanded = !item.IsExpanded;
                item.IsSelected = item.IsExpanded;
            }
            else
            {
                item.IsExpanded = false;
                item.IsSelected = false;
            }
        }

        IsFlyoutOpen = false;
    }

    private void SelectOnly(MenuItemViewModel selected)
    {
        foreach (var item in Items)
        {
            item.IsSelected = item == selected;
            item.IsExpanded = false;
        }
    }
}
