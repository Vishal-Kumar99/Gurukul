

using Gurukul.Core;
using Gurukul.MVVM.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace Gurukul.Stores;

public class SectionStore
{
    public ObservableCollection<Section> Sections { get; } = new();

    public bool _isLoaded = false;

    public async Task LoadAsync()
    {
        if (_isLoaded) return;

        Sections.Clear();

        try
        {
            using SqlConnection con = new(AppState._conn);
            string query = @"SELECT S.SectionId, S.SectionName, S.ClassId, C.ClassName
                             FROM Section S
                             INNER JOIN Class C ON S.ClassId = C.ClassId";

            using SqlCommand cmd = new(query, con);
            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Sections.Add(new Section
                {
                    SectionId = reader.GetInt32(0),
                    SectionName = reader.GetString(1),
                    ClassId = reader.GetInt32(2),
                    ClassName = reader.GetString(3)
                });
            }

            _isLoaded = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading sections: {ex.Message}");
        }
    }

    public void AddSection(Section newSection)
    {
        Sections.Add(newSection);
    }

    public void RemoveSection(Section section)
    {
        Sections.Remove(section);
    }
}
