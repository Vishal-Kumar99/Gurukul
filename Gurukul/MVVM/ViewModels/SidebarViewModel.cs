
using Gurukul.Core;
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

            foreach (var item in Items)
            {
                item.IsExpanded = false;
                item.IsSelected = false;
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

    public ObservableCollection<MenuItemViewModel> Items { get; set; }

    public RelayCommand ToggleSidebarCommand { get; }
    public RelayCommand SubItemClickCommand { get; }

    public SidebarViewModel(INavigationService navigation)
    {
        _navigation = navigation;

        IsCollapsed = false;

        ToggleSidebarCommand = new RelayCommand(obj =>
        {
            IsCollapsed = !IsCollapsed;
        }, obj => true);

        SubItemClickCommand = new RelayCommand(obj =>
        {
            if (obj is not MenuItemViewModel item)
                return;
            if (item.TargetViewModel != null)
                _navigation.NavigateTo(item.TargetViewModel);

            //SelectOnly(item);
            if (IsCollapsed)
            {
                IsFlyoutOpen = false;
            }
        }, obj => true);

        BuildSidebar();
    }

    private void BuildSidebar()
    {
        Items = new ObservableCollection<MenuItemViewModel>
        {
            new MenuItemViewModel
            {
                Title = "Dashboard",
                Icon = "\uE80F",
                TargetViewModel = typeof(HomeViewModel)
            },
            //CreateItem("Dashboard", "\uE80F"),

            CreateItem("Students", "\uE77B",
                ("Admission Form", typeof(HomeViewModel)), ("List Students", typeof(HomeViewModel)), ("Student Categories", typeof(HomeViewModel)), ("Student House", typeof(HomeViewModel)), ("Student Activity", typeof(HomeViewModel)), ("Parents", typeof(HomeViewModel))),

            CreateItem("Teachers", "\uE716",
                ("Add Teacher", typeof(HomeViewModel)), ("List Teachers", typeof(HomeViewModel)), ("Teacher Salary", typeof(HomeViewModel))),

            CreateItem("Class", "\uE7BE",
                ("Add Class", typeof(HomeViewModel)), ("Class Routine", typeof(HomeViewModel)), ("Class Information", typeof(HomeViewModel))),

            CreateItem("Attendance", "\uE73E",
                ("Student Attendance", typeof(HomeViewModel)), ("Teacher Attendance", typeof(HomeViewModel))),

            CreateItem("Subjects", "\uE8F1",
                ("Add Subjects", typeof(HomeViewModel)), ("Assign Subject", typeof(HomeViewModel))),

            CreateItem("Examination", "\uE70F",
                ("Add Examination", typeof(HomeViewModel)), ("Exam Schedule", typeof(HomeViewModel)), ("Question Paper", typeof(HomeViewModel)), ("Marks Entry", typeof(HomeViewModel))),

            CreateItem("Fee Collection", "\uE8C7",
                ("Collect Fee", typeof(HomeViewModel)), ("Invoice", typeof(HomeViewModel)), ("Fee Structure", typeof(HomeViewModel)), ("Due Fee", typeof(HomeViewModel))),

            CreateItem("Transportation", "\uE804",
                ("Transport", typeof(HomeViewModel)), ("Transport Route", typeof(HomeViewModel)), ("Manage Vehicle", typeof(HomeViewModel))),

            new MenuItemViewModel
            {
                Title = "Notice",
                Icon = "\uE7ED",
                TargetViewModel = typeof(HomeViewModel)
            },

            new MenuItemViewModel
            {
                Title = "Roles",
                Icon = "\uE192",
                TargetViewModel = typeof(HomeViewModel)
            },
            //CreateItem("Notice", "\uE7ED"),

            //CreateItem("Roles", "\uE192"),

            CreateItem("Settings", "\uE713",
                ("General", typeof(HomeViewModel)), ("About", typeof(HomeViewModel)))
        };

        foreach (var item in Items)
        {
            item.ToggleCommand = new RelayCommand(obj => Toggle(obj), obj => true);
        }
    }

    //private void BuildSidebar()
    //{
    //    Items = new ObservableCollection<MenuItemViewModel>();

    //    // Home
    //    var homeItem = new MenuItemViewModel
    //    {
    //        Title = "Dashboard",
    //        Icon = "\uE80F"
    //    };


    //    homeItem.ToggleCommand = new RelayCommand(obj =>
    //    {
    //        SelectOnly(homeItem);
    //        _navigation.NavigateTo<HomeViewModel>();
    //    }, obj => true);

    //    Items.Add(homeItem);

    //    // Student
    //    var studentItems = new MenuItemViewModel
    //    {
    //        Title = "Students",
    //        Icon = "\uE77B",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Admission Form" },
    //            new MenuItemViewModel { Title = "List Students" },
    //            new MenuItemViewModel { Title = "Student Categories" },
    //            new MenuItemViewModel { Title = "Student House" },
    //            new MenuItemViewModel { Title = "Student Activity" },
    //            new MenuItemViewModel { Title = "Parents" }
    //        }
    //    };

    //    Items.Add(studentItems);

    //    // Teacher
    //    var teacherItems = new MenuItemViewModel
    //    {
    //        Title = "Teachers",
    //        Icon = "\uE716",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Add Teacher" },
    //            new MenuItemViewModel { Title = "List Teachers" },
    //            new MenuItemViewModel { Title = "Teacher Salary" }
    //        }
    //    };

    //    Items.Add(teacherItems);

    //    // Class
    //    var classItems = new MenuItemViewModel
    //    {
    //        Title = "Class",
    //        Icon = "\uE7BE",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Add Class" },
    //            new MenuItemViewModel { Title = "Class Routine" },
    //            new MenuItemViewModel { Title = "Class Information" }
    //        }
    //    };

    //    Items.Add(classItems);

    //    var attendanceItems = new MenuItemViewModel
    //    {
    //        Title = "Attendance",
    //        Icon = "\uE73E",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Student Attendance" },
    //            new MenuItemViewModel { Title = "Teacher Attendance" }
    //        }
    //    };

    //    Items.Add(attendanceItems);

    //    // Subject
    //    var subjectItems = new MenuItemViewModel
    //    {
    //        Title = "Subjects",
    //        Icon = "\uE8F1",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Add Subjects" }, // add list within this
    //            new MenuItemViewModel { Title = "Assign Subject" }
    //        }
    //    };

    //    Items.Add(subjectItems);

    //    // Examination
    //    var examinationItems = new MenuItemViewModel
    //    {
    //        Title = "Examination",
    //        Icon = "\uE70F",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Add Examination" },
    //            new MenuItemViewModel { Title = "Exam Schedule" },
    //            new MenuItemViewModel { Title = "Question Paper" },
    //            new MenuItemViewModel { Title = "Marks Entry" }
    //        }
    //    };

    //    Items.Add(examinationItems);

    //    // Fee Collection
    //    var feeCollectionItems = new MenuItemViewModel
    //    {
    //        Title = "Fee Collection",
    //        Icon = "\uE8C7",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Collect Fee" },
    //            new MenuItemViewModel { Title = "Invoice" },
    //            new MenuItemViewModel { Title = "Fee Structure" },
    //            new MenuItemViewModel { Title = "Due Fee" }
    //        }
    //    };

    //    Items.Add(feeCollectionItems);

    //    // Tranportation
    //    var transportItems = new MenuItemViewModel
    //    {
    //        Title = "Transportation",
    //        Icon = "\uE804",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "Transport" },
    //            new MenuItemViewModel { Title = "Transport Route" },
    //            new MenuItemViewModel { Title = "Manage Vehicle" }
    //        }
    //    };

    //    Items.Add(transportItems);

    //    // Notice
    //    var noticeItem = new MenuItemViewModel
    //    {
    //        Title = "Notice",
    //        Icon = "\uE7ED",
    //    };


    //    noticeItem.ToggleCommand = new RelayCommand(obj =>
    //    {
    //        SelectOnly(noticeItem);
    //        _navigation.NavigateTo<HomeViewModel>();
    //    }, obj => true);

    //    Items.Add(noticeItem);

    //    // Role Management
    //    var roleManagementItem = new MenuItemViewModel
    //    {
    //        Title = "Role Management",
    //        Icon = "\uE192",
    //    };


    //    roleManagementItem.ToggleCommand = new RelayCommand(obj =>
    //    {
    //        SelectOnly(roleManagementItem);
    //        _navigation.NavigateTo<HomeViewModel>();
    //    }, obj => true);

    //    Items.Add(roleManagementItem);

    //    // Settings
    //    var settingItems = new MenuItemViewModel
    //    {
    //        Title = "Settings",
    //        Icon = "\uE713",
    //        Options = new ObservableCollection<MenuItemViewModel>
    //        {
    //            new MenuItemViewModel { Title = "General" },
    //            new MenuItemViewModel { Title = "About" }
    //        }
    //    };

    //    Items.Add(settingItems);

    //    foreach (var item in Items)
    //    {
    //        if (item.ToggleCommand == null)
    //        {
    //            item.ToggleCommand = new RelayCommand(obj => Toggle(item), obj => true);
    //        }
    //    }
    //}

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

        //if (subItems.Length > 0)
        //{
        //    item.Options = new ObservableCollection<MenuItemViewModel>();

        //    foreach(var i in subItems)
        //    {
        //        item.Options.Add(new MenuItemViewModel { Title = i });
        //    }
        //}
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
            //NavigateLeaf(selected);
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

    //private void NavigateLeaf(MenuItemViewModel item)
    //{
    //    switch (item.Title)
    //    {
    //        case "Dashboard":
    //            _navigation.NavigateTo<HomeViewModel>();
    //            break;
    //        case "Notice":
    //            _navigation.NavigateTo<HomeViewModel>();
    //            break;
    //        case "Roles":
    //            _navigation.NavigateTo<HomeViewModel>();
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private void SelectOnly(MenuItemViewModel selected)
    {
        foreach (var item in Items)
        {
            item.IsSelected = item == selected;
            item.IsExpanded = false;
        }
    }
}
