
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Gurukul.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;

namespace Gurukul.MVVM.ViewModels.Admission;

public class StudentBasicVM : StepViewModelBase
{
    public override bool IsFinalStep => false;

    public ObservableCollection<Class> ClassList => AppState.ClassStore.Classes;
    public ObservableCollection<Section> SectionList => AppState.SectionStore.Sections;
    public ObservableCollection<string> UniqueSectionList { get; private set; } = new();

    public ICollectionView SortedSectionView { get; }
    public ListCollectionView SortedClassView { get; }

    public AdmissionDraft Draft => AppState.AdmissionDraft;

    private string _fullName;
    public string FullName
    {
        get => _fullName;
        set
        {
            _fullName = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private int _classId;
    public int ClassId
    {
        get => _classId;
        set
        {
            _classId = value;
            OnPropertyChanged();
            Validate();
        }
    }

    private int _sectionId;
    public int SectionId
    {
        get => _sectionId;
        set
        {
            _sectionId = value;
            OnPropertyChanged();
            Validate();
        }
    }

    public override string StepName => "Student Details";

    public StudentBasicVM()
    {
        AppState.AdmissionDraftChanged += () =>
        {
            OnPropertyChanged(nameof(Draft));
        };

        SortedSectionView = CollectionViewSource.GetDefaultView(SectionList);
        SortedClassView = (ListCollectionView)CollectionViewSource.GetDefaultView(ClassList);
        SortedClassView.CustomSort = new Converters.SortComparer();

        SortedSectionView.SortDescriptions.Add(new SortDescription(nameof(Section.ClassName), ListSortDirection.Ascending));
        SortedSectionView.SortDescriptions.Add(new SortDescription(nameof(Section.SectionName), ListSortDirection.Ascending));

        _ = LoadInitialData();

        var draft = AppState.AdmissionDraft;

        FullName = draft.FullName;
        ClassId = draft.ClassId;
        SectionId = draft.SectionId;

        Validate();
    }

    public async Task LoadInitialData()
    {
        await AppState.ClassStore.LoadAsync();
        await AppState.SectionStore.LoadAsync();

        UpdateUniqueSectionList();
    }

    public override void Validate()
    {
        Errors.Clear();

        //if(string.IsNullOrWhiteSpace(AdmissionNo))
        //    Errors.Add("Admission Number is required.");

        if (string.IsNullOrWhiteSpace(FullName))
            Errors.Add("Full Name is required.");

        if (ClassId == 0)
            Errors.Add("Class is required.");

        if (SectionId == 0)
            Errors.Add("Section is required.");

        OnPropertyChanged(nameof(IsValid));
    }

    public override void SaveDraft(int admissionId)
    {
        var draft = AppState.AdmissionDraft;

        //draft.AdmissionNo = AdmissionNo;
        draft.FullName = FullName;
        draft.ClassId = ClassId;
        draft.SectionId = SectionId;
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
