
namespace Gurukul.MVVM.Models;

public class AcademicYear : Core.ViewModel
{
    public int AcademicYearId { get; set; }
    public string YearName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

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
