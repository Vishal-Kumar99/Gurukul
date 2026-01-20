
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
using System.Windows.Data;

namespace Gurukul.MVVM.ViewModels;

public class AddClassViewModel : ViewModel
{
    public ObservableCollection<Class> ClassList => AppState.ClassStore.Classes;
    public ObservableCollection<Section> SectionList => AppState.SectionStore.Sections;
    public ObservableCollection<string> UniqueSectionList { get; private set; } = new();

    public ICollectionView SortedSectionView { get; }
    public ListCollectionView SortedClassView { get; }

    private string _newClassName;
    public string NewClassName 
    {
        get => _newClassName;
        set
        {
            _newClassName = value;
            OnPropertyChanged();
            AddClassCommand.RaiseCanExecuteChanged();
        }
    }

    private string _newSectionName;
    public string NewSectionName
    {
        get => _newSectionName;
        set
        {
            _newSectionName = value;
            OnPropertyChanged();
            AddSectionCommand.RaiseCanExecuteChanged();
        }
    }

    private Class _selectedClass;
    public Class SelectedClass
    {
        get => _selectedClass;
        set
        {
            _selectedClass = value;
            OnPropertyChanged();
        }
    }

    private Section _selectedSection;
    public Section SelectedSection
    {
        get => _selectedSection;
        set
        {
            _selectedSection = value;
            OnPropertyChanged();
            DeleteSectionCommand.RaiseCanExecuteChanged();
        }
    }

    public RelayCommand AddClassCommand { get; }
    public RelayCommand AddSectionCommand { get; }
    public RelayCommand DeleteSectionCommand { get; }

    public AddClassViewModel()
    {
        AddClassCommand = new RelayCommand(async _ => await AddClassAsync(), _ => !string.IsNullOrWhiteSpace(NewClassName));
        AddSectionCommand = new RelayCommand(async _ => await AddSectionAsync(), _ => !string.IsNullOrWhiteSpace(NewSectionName));
        DeleteSectionCommand = new RelayCommand(async _ => await DeleteSectionAsync(), _ => SelectedSection != null);

        SortedSectionView = CollectionViewSource.GetDefaultView(SectionList);
        SortedClassView = (ListCollectionView)CollectionViewSource.GetDefaultView(ClassList);
        SortedClassView.CustomSort = new Converters.SortComparer();

        SortedSectionView.SortDescriptions.Add(new SortDescription(nameof(Section.ClassName), ListSortDirection.Ascending));
        SortedSectionView.SortDescriptions.Add(new SortDescription(nameof(Section.SectionName), ListSortDirection.Ascending));

        _ = LoadInitialData();
    }

    private async Task LoadInitialData()
    {
        await AppState.ClassStore.LoadAsync();
        await AppState.SectionStore.LoadAsync();

        UpdateUniqueSectionList();
    }

    private async Task DeleteSectionAsync()
    {
        if (SelectedSection == null) return;

        var result = MessageBox.Show("Deleting this section will remove all related information. Continue?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes) return;

        try
        {
            using SqlConnection con = new(AppState._conn);
            string query = @"DELETE FROM Section WHERE SectionId = @id";
            using SqlCommand cmd = new(query, con);

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = SelectedSection.SectionId;

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            AppState.SectionStore.RemoveSection(SelectedSection);
            SelectedSection = null;

            UpdateUniqueSectionList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error deleting section: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task AddSectionAsync()
    {
        if (SelectedClass == null) return;

        bool exists = SectionList.Any(c => c.SectionName.Equals(NewSectionName, StringComparison.OrdinalIgnoreCase) && c.ClassId == SelectedClass.ClassId);

        if (exists)
        {
            MessageBox.Show("Section already exists.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            using SqlConnection con = new(AppState._conn);

            string query = @"INSERT INTO Section (SectionName, ClassId) VALUES (@SectionName, @ClassId); SELECT SCOPE_IDENTITY();";

            using SqlCommand cmd = new(query, con);
            cmd.Parameters.Add("@SectionName", System.Data.SqlDbType.NVarChar).Value = NewSectionName;
            cmd.Parameters.AddWithValue("@ClassId", System.Data.SqlDbType.Int).Value = SelectedClass.ClassId;

            await con.OpenAsync();
            var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            AppState.SectionStore.AddSection(new Section 
            { 
                SectionId = id, 
                SectionName = NewSectionName, 
                ClassId = SelectedClass.ClassId, 
                ClassName = SelectedClass.ClassName 
            });

            UpdateUniqueSectionList();
            NewSectionName = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding section: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task AddClassAsync()
    {
        if (ClassList.Any(c => c.ClassName.Equals(NewClassName, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Class already exists.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            using SqlConnection con = new(AppState._conn);

            string query = @"INSERT INTO Class (ClassName) VALUES (@Name); SELECT SCOPE_IDENTITY();";

            using SqlCommand cmd = new(query, con);
            cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = NewClassName;

            await con.OpenAsync();
            var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            AppState.ClassStore.AddClass(new Class 
            { 
                ClassId = id, 
                ClassName = NewClassName 
            });

            NewClassName = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding class: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void UpdateUniqueSectionList()
    {
        UniqueSectionList = new ObservableCollection<string>(
            AppState.SectionStore.Sections
            .Select(s => s.SectionName)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(s => s)
        );

        OnPropertyChanged(nameof(UniqueSectionList));
    }
}
