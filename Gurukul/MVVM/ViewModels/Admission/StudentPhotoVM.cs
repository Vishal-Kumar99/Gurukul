
using Gurukul.Core;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gurukul.MVVM.ViewModels.Admission;

public class StudentPhotoVM : StepViewModelBase
{
    public override bool IsFinalStep => false;
    public override string StepName => "Student Photo";

    private ImageSource _photoPreview;
    public ImageSource PhotoPreview
    {
        get => _photoPreview;
        set
        {
            _photoPreview = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand BrowsePhotoCommand { get; }

    private byte[] _photoBytes;
    private string _fileName;

    public StudentPhotoVM()
    {
        BrowsePhotoCommand = new RelayCommand(_ => BrowsePhoto());

        // Restore from draft (Back navigation)
        var draft = AppState.AdmissionDraft;
        if (draft.StudentPhoto != null)
        {
            _photoBytes = draft.StudentPhoto;
            _fileName = draft.PhotoFileName;
            PhotoPreview = LoadImage(_photoBytes);
        }

        Validate();
    }

    private void BrowsePhoto()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
            Title = "Select Student Photo"
        };

        if (dialog.ShowDialog() != true)
            return;

        _fileName = dialog.SafeFileName;
        _photoBytes = File.ReadAllBytes(dialog.FileName);

        PhotoPreview = LoadImage(_photoBytes);
        Validate();
    }

    public override void Validate()
    {
        Errors.Clear();

        if (_photoBytes == null || _photoBytes.Length == 0)
            Errors.Add("Student photo is required.");

        OnPropertyChanged(nameof(IsValid));
    }

    public override void SaveDraft(int admissionId)
    {
        var draft = AppState.AdmissionDraft;

        draft.StudentPhoto = _photoBytes;
        draft.PhotoFileName = _fileName;
    }

    private static ImageSource LoadImage(byte[] imageBytes)
    {
        using var ms = new MemoryStream(imageBytes);
        var image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.StreamSource = ms;
        image.EndInit();
        image.Freeze();
        return image;
    }
}
