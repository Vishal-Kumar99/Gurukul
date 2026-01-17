
using Gurukul.MVVM.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Gurukul.Core;

public static class AppState
{
    public static AcademicYear? CurrentAcademicYear { get; set; }

    public static string _conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

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
