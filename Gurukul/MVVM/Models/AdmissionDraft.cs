
using Gurukul.Core;

namespace Gurukul.MVVM.Models;

public class AdmissionDraft : ObservableObject
{
    // Student
    private string _admissionNo;
    public string AdmissionNo 
    { 
        get => _admissionNo;
        set
        {
            _admissionNo = value;
            OnPropertyChanged();
        }
    }

    private string _fullName;
    public string FullName
    {
        get => _fullName;
        set
        {
            _fullName = value;
            OnPropertyChanged();
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
        }
    }

    // Parent
    private string _fatherName;
    public string FatherName
    {
        get => _fatherName;
        set
        {
            _fatherName = value;
            OnPropertyChanged();
        }
    }

    private string _fatherOccupation;
    public string FatherOccupation
    {
        get => _fatherOccupation;
        set
        {
            _fatherOccupation = value;
            OnPropertyChanged();
        }
    }

    private string _motherName;
    public string MotherName
    {
        get => _motherName;
        set
        {
            _motherName = value;
            OnPropertyChanged();
        }
    }

    private string _motherOccupation;
    public string MotherOccupation
    {
        get => _motherOccupation;
        set
        {
            _motherOccupation = value;
            OnPropertyChanged();
        }
    }

    private decimal _annualIncome;
    public decimal AnnualIncome
    {
        get => _annualIncome;
        set
        {
            _annualIncome = value;
            OnPropertyChanged();
        }
    }

    private string _mobileNo;
    public string MobileNo
    {
        get => _mobileNo;
        set
        {
            _mobileNo = value;
            OnPropertyChanged();
        }
    }

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    // Address
    private string _addressLine;
    public string AddressLine
    {
        get => _addressLine;
        set
        {
            _addressLine = value;
            OnPropertyChanged();
        }
    }

    private string _city;
    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
        }
    }

    private string _state;
    public string State
    {
        get => _state;
        set
        {
            _state = value;
            OnPropertyChanged();
        }
    }

    private string _pincode;
    public string Pincode
    {
        get => _pincode;
        set
        {
            _pincode = value;
            OnPropertyChanged();
        }
    }

    private string _landmark;
    public string Landmark
    {
        get => _landmark;
        set
        {
            _landmark = value;
            OnPropertyChanged();
        }
    }

    // Academic
    private string _previousSchool;
    public string PreviousSchool
    {
        get => _previousSchool;
        set
        {
            _previousSchool = value;
            OnPropertyChanged();
        }
    }

    private string _lastClassStudied;
    public string LastClassStudied
    {
        get => _lastClassStudied;
        set
        {
            _lastClassStudied = value;
            OnPropertyChanged();
        }
    }

    private string _board;
    public string Board
    {
        get => _board;
        set
        {
            _board = value;
            OnPropertyChanged();
        }
    }

    private string _result;
    public string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    private string _remarks;
    public string Remarks
    {
        get => _remarks;
        set
        {
            _remarks = value;
            OnPropertyChanged();
        }
    }

    // Health
    private string _bloodGroup;
    public string BloodGroup
    {
        get => _bloodGroup;
        set
        {
            _bloodGroup = value;
            OnPropertyChanged();
        }
    }

    private string _medicalCondition;
    public string MedicalCondition
    {
        get => _medicalCondition;
        set
        {
            _medicalCondition = value;
            OnPropertyChanged();
        }
    }

    private string _allergyDetails;
    public string AllergyDetails
    {
        get => _allergyDetails;
        set
        {
            _allergyDetails = value;
            OnPropertyChanged();
        }
    }

    private bool _hasDisability;
    public bool HasDisability
    {
        get => _hasDisability;
        set
        {
            _hasDisability = value;
            OnPropertyChanged();
        }
    }

    // Photo
    private byte[] _studentPhoto;
    public byte[] StudentPhoto
    {
        get => _studentPhoto;
        set
        {
            _studentPhoto = value;
            OnPropertyChanged();
        }
    }

    private string _photoFileName;
    public string PhotoFileName
    {
        get => _photoFileName;
        set
        {
            _photoFileName = value;
            OnPropertyChanged();
        }
    }

    // Meta
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
