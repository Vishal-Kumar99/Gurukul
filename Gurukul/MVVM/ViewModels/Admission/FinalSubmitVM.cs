
using Gurukul.Core;
using Gurukul.MVVM.Models;
using Gurukul.Services;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace Gurukul.MVVM.ViewModels.Admission;

public class FinalSubmitVM : StepViewModelBase
{
    public override bool IsFinalStep => true;
    public override string StepName => "Confirm & Submit";
    public string AdmissionNo => Draft.AdmissionNo;

    private readonly INavigationService _navigation;

    public AdmissionDraft Draft => AppState.AdmissionDraft;

    //private string _admissionNo;
    //public string AdmissionNo
    //{
    //    get => _admissionNo;
    //    set
    //    {
    //        _admissionNo = value;
    //        OnPropertyChanged();
    //        Validate();
    //    }
    //}

    public FinalSubmitVM(INavigationService navigation)
    {
        _navigation = navigation;
    }

    public override void SaveDraft(int admissionId)
    {
        int retryCount = 0;
        const int maxRetries = 2;

        while (true)
        {
            try
            {
                AdmissionService.SubmitAdmission(Draft);

                OnPropertyChanged(nameof(AdmissionNo));

                var result = MessageBox.Show(
                    $"Admission Completed Successfully!\n\n" +
                    $"Admission No: {Draft.AdmissionNo}\n\n" +
                    "Do you want to add another admission?",
                    "Admission Completed",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                AppState.AdmissionDraft = new AdmissionDraft();
                AppState.IsAdmissionInProgress = false;

                if (result == MessageBoxResult.Yes)
                {
                    _navigation.NavigateTo<AdmissionWizardViewModel>();
                }
                else
                {
                    _navigation.NavigateTo<HomeViewModel>();
                }

                break;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                retryCount++;

                if (retryCount > maxRetries)
                {
                    MessageBox.Show(
                        "Failed to submit admission after multiple attempts due to duplicate admission number.",
                        "Admission Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    break;
                }

                //var newNo = AdmissionService.GenerateAdmissionNo();

                //var result = MessageBox.Show(
                //    $"New Admission Number: {newNo}\nRetry submission?",
                //    "Retry Admission",
                //    MessageBoxButton.YesNo,
                //    MessageBoxImage.Question);

                //if (result == MessageBoxResult.Yes)
                //    Draft.AdmissionNo = newNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unexpected error: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                break;
            }
        }
    }

    public override void Validate()
    {
        Errors.Clear();
        OnPropertyChanged(nameof(IsValid));
    }
}

