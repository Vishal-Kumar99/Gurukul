
using Gurukul.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gurukul.MVVM.Models;

public class AdmissionStep : INotifyPropertyChanged
{
    public string Title { get; set; }
    public string Icon { get; set; }

    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive == value) return;

            _isActive = value;
            OnPropertyChanged(nameof(IsActive));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
