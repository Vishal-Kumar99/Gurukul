
using Gurukul.MVVM.Models;
using Gurukul.Stores;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Gurukul.Core;

public static class AppState
{
    public static string _conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    private static AdmissionDraft _admissionDraft = new();
    public static AdmissionDraft AdmissionDraft
    {
        get => _admissionDraft;
        set
        {
            _admissionDraft = value;
            AdmissionDraftChanged?.Invoke();
        }
    }

    public static event Action? AdmissionDraftChanged;
    public static bool IsAdmissionInProgress { get; set; }
    public static AcademicYear? CurrentAcademicYear { get; set; }
    public static int ActiveAcademicYearId
    => CurrentAcademicYear?.AcademicYearId
       ?? throw new InvalidOperationException("Active Academic Year is not loaded.");

    public static ClassStore ClassStore { get; } = new();
    public static SectionStore SectionStore { get; } = new();

    public static AppSettings Settings { get; set; } = new();

    public static event Action SidebarBehaviorChanged;

    public static void NotifySidebarBehaviorChanged()
    {
        SidebarBehaviorChanged?.Invoke();
    }

    public static void LoadActiveAcademicYear()
    {
        using SqlConnection con = new(_conn);
        string query = @"SELECT TOP 1 AcademicYearId, YearName, IsActive FROM AcademicYear WHERE IsActive = 1";

        using SqlCommand cmd = new(query, con);
        con.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            CurrentAcademicYear = new AcademicYear
            {
                AcademicYearId = reader.GetInt32(0),
                YearName = reader.GetString(1),
                IsActive = reader.GetBoolean(2)
            };
        }
    }
}
