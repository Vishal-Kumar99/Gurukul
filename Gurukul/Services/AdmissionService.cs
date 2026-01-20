
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Gurukul.Services;

public class AdmissionService
{
    private static readonly string _conn = AppState._conn;

    //public static string GenerateAdmissionNo()
    //{
    //    using SqlConnection con = new(_conn);
    //    using SqlCommand cmd = new("GetNextAdmissionNo", con);

    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    //    cmd.Parameters.AddWithValue("@AcademicYearId", AppState.ActiveAcademicYearId);

    //    var output = new SqlParameter("@NextAdmissionNo", System.Data.SqlDbType.NVarChar, 20)
    //    {
    //        Direction = System.Data.ParameterDirection.Output
    //    };
    //    cmd.Parameters.Add(output);

    //    con.Open();
    //    cmd.ExecuteNonQuery();

    //    return output.Value?.ToString() ?? string.Empty;
    //}

    public static int SubmitAdmission(AdmissionDraft draft)
    {
        using SqlConnection con = new(_conn);
        con.Open();

        using SqlTransaction tx = con.BeginTransaction();

        try
        {
            draft.AdmissionNo = GetAdmissionNo(con, tx);

            int parentId = InsertParent(con, tx, draft);
            int studentId = InsertStudent(con, tx, draft, parentId);

            tx.Commit();
            return studentId;
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    private static int InsertParent(SqlConnection con, SqlTransaction tx, AdmissionDraft draft)
    {
        const string sql = @"
        INSERT INTO Parent (FatherName, MobileNo, Email)
        VALUES (@FatherName, @MobileNo, @Email);
        SELECT SCOPE_IDENTITY();";

        using SqlCommand cmd = new(sql, con, tx);

        cmd.Parameters.AddWithValue("@FatherName", draft.FatherName ?? "");
        cmd.Parameters.AddWithValue("@MotherName", draft.MotherName ?? "");
        cmd.Parameters.AddWithValue("@MobileNo", draft.MobileNo ?? "");
        cmd.Parameters.AddWithValue("@Email", "");

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    private static int InsertStudent(
    SqlConnection con,
    SqlTransaction tx,
    AdmissionDraft draft,
    int parentId)
    {
        const string sql = @"
        INSERT INTO Student
        (
            AdmissionNo,
            FullName,
            ClassId,
            SectionId,
            ParentId,
            AcademicYearId
        )
        VALUES
        (
            @AdmissionNo,
            @FullName,
            @ClassId,
            @SectionId,
            @ParentId,
            @AcademicYearId
        );SELECT SCOPE_IDENTITY();";

        using SqlCommand cmd = new(sql, con, tx);

        cmd.Parameters.AddWithValue("@AdmissionNo", draft.AdmissionNo);
        cmd.Parameters.AddWithValue("@FullName", draft.FullName);
        cmd.Parameters.AddWithValue("@ClassId", draft.ClassId);
        cmd.Parameters.AddWithValue("@SectionId", draft.SectionId);
        cmd.Parameters.AddWithValue("@ParentId", parentId);
        cmd.Parameters.AddWithValue("@AcademicYearId", AppState.ActiveAcademicYearId);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    private static string GetAdmissionNo(SqlConnection con, SqlTransaction tx)
    {
        using SqlCommand cmd = new("GetNextAdmissionNo", con, tx);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@AcademicYearId", AppState.ActiveAcademicYearId);

        var output = new SqlParameter("@NextAdmissionNo", SqlDbType.NVarChar, 20)
        {
            Direction = ParameterDirection.Output
        };
        cmd.Parameters.Add(output);

        cmd.ExecuteNonQuery();

        return output.Value!.ToString();
    }
}
