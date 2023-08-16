using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class Study_ViewModel : AbstractEventDrivenViewModel{

    public Study_ViewModel(StudyCollectionItem studyCollectionItem) : base(studyCollectionItem){
        this.StudyCollectionItem = studyCollectionItem;
        this.PatientName = studyCollectionItem.GetStudyComposition()[0].PatientName;
        this.PatientAge = studyCollectionItem.GetStudyComposition()[0].PatientAge;
        this.PatientGender = studyCollectionItem.GetStudyComposition()[0].PatientGender;
        this.StudyUid = studyCollectionItem.GetStudyComposition()[0].StudyInstanceId;
        SetupCommand();
    }

    private void SetupCommand() {
    }

    #region COMMANDS
    
    #endregion

    #region COMMAND_BINDING_METHODS
    
    #endregion

    public StudyCollectionItem StudyCollectionItem;

    private string _patientName;

    public string PatientName {
        get {
            return _patientName;
        }
        set {
            if(_patientName == value) return;
            _patientName = value;
            RisePropertyChanged(nameof(PatientName));
        }
    }

    private string _patientAge;

    public string PatientAge {
        get {
            return _patientAge;
        }
        set {
            if(_patientAge == value)return;
            _patientAge = value;
            RisePropertyChanged(nameof(PatientAge));
        }
    }

    private string _patientGender;

    public string PatientGender {
        get {
            return _patientGender;
        }
        set {
            if(_patientGender == value)return;
            _patientGender = value;
            RisePropertyChanged(nameof(PatientGender));
        }
    }

    private string _studyUid;

    public string StudyUid {
        get {
            return _studyUid;
        }
        set {
            if(_studyUid == (value))return;
            _studyUid = value;
            RisePropertyChanged(nameof(StudyUid));
        }
    }

    public void CloseStudy() {
        MainEntry_ModelFacade.GetInstance().DeleteStudyItem(this.StudyCollectionItem);
    }

}