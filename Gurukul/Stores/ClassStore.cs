
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;

namespace Gurukul.Stores;

public class ClassStore
{
    public ObservableCollection<Class> Classes { get; } = new();

    private bool _isLoaded = false;

    public async Task LoadAsync()
    {
        if (_isLoaded)
            return;

        Classes.Clear();

        try
        {
            using SqlConnection con = new(AppState._conn);
            string query = @"SELECT ClassId, ClassName FROM Class";
            using SqlCommand cmd = new(query, con);

            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Classes.Add(new Class
                {
                    ClassId = reader.GetInt32(0),
                    ClassName = reader.GetString(1)
                });
            }
            _isLoaded = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading classes: {ex.Message}");
        }
    }

    public void AddClass(Class newClass)
    {
        Classes.Add(newClass);
    }
}
