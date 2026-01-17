
namespace Gurukul.MVVM.Models;

public class AcademicYear : Core.ViewModel
{
    public int AcademicYearId { get; set; }
    public string YearName { get; set; }

    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnPropertyChanged();
        }
    }

    public DateTime CreatedAt { get; set; }
}
