
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

namespace Gurukul.MVVM.ViewModels;

public class AcademicYearViewModel : Core.ViewModel
{
    public ObservableCollection<AcademicYear> SessionList { get; set; } = new();

    private DateTime? _selectedStartYear;
    public DateTime? SelectedStartYear
    {
        get => _selectedStartYear;
        set
        {
            _selectedStartYear = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand AddSessionCommand { get; }
    public RelayCommand CloseWindowCommand { get; }

    public AcademicYearViewModel()
    {
        LoadData();

        AddSessionCommand = new RelayCommand(_ => AddSession());
        CloseWindowCommand = new RelayCommand(w => CloseWindow(w));
    }

    private bool _isUpdating;

    private void LoadData()
    {
        try
        {
            using SqlConnection con = new(AppState._conn);
            string query = @"SELECT AcademicYearId, YearName, IsActive FROM AcademicYear";

            using SqlCommand cmd = new(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var year = new AcademicYear
                {
                    AcademicYearId = reader.GetInt32(0),
                    YearName = reader.GetString(1),
                    IsActive = reader.GetBoolean(2)
                };

                year.PropertyChanged += Year_PropertyChanged;
                SessionList.Add(year);

                if (year.IsActive)
                    AppState.CurrentAcademicYear = year;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Year_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_isUpdating) return;

        if (e.PropertyName == nameof(AcademicYear.IsActive))
        {
            var selected = sender as AcademicYear;

            if (selected == null || !selected.IsActive) return;

            try
            {
                _isUpdating = true;

                foreach (var year in SessionList)
                {
                    if (year != selected)
                        year.IsActive = false;
                }

                UpdateActiveYear(selected);
                AppState.CurrentAcademicYear = selected;
            }
            finally
            {
                _isUpdating = false;
            }
        }
    }

    private void UpdateActiveYear(AcademicYear activeYear)
    {
        try
        {
            using SqlConnection con = new(AppState._conn);
            con.Open();

            string deactivateAll = @"Update AcademicYear Set IsActive = 0";
            string activateOne = @"Update AcademicYear Set IsActive = 1 Where AcademicYearId = @Id";

            using SqlCommand deactivateCmd = new(deactivateAll, con);
            deactivateCmd.ExecuteNonQuery();

            using SqlCommand activateCmd = new(activateOne, con);
            activateCmd.Parameters.AddWithValue("@Id", activeYear.AcademicYearId);
            activateCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void AddSession()
    {
        if (SelectedStartYear == null) return;

        int start = SelectedStartYear.Value.Year;
        int end = start + 1;
        string name = $"{start} - {end % 100:D2}";
        DateTime date = DateTime.Now;

        if (SessionList.Any(s => s.YearName == name))
        {
            MessageBox.Show("Session already exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            using SqlConnection con = new(AppState._conn);
            string query = @"Insert Into AcademicYear (YearName, IsActive, CreatedOn) Output Inserted.AcademicYearId Values (@name, 0, @date)";

            using SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@date", date);
            con.Open();

            int id = (int)cmd.ExecuteScalar();

            var year = new AcademicYear
            {
                AcademicYearId = id,
                YearName = name,
                IsActive = false,
                CreatedAt = DateTime.Now
            };

            year.PropertyChanged += Year_PropertyChanged;
            SessionList.Add(year);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void CloseWindow(object window)
    {
        if (window is Window w)
            w.Close();
    }
}
