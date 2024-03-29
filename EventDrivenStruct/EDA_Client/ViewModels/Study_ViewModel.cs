using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class Study_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public Study_ViewModel(StudyCollectionItem studyCollectionItem) : base(studyCollectionItem){
        this.StudyCollectionItem = studyCollectionItem;
        studyCollectionItem.RegisterObserver(this);
        InitDefaultInfo(studyCollectionItem);
        SetupCommand();

        LockingStatusStr = " lock ";
    }
    
    private void SetupCommand() {
        LockSwitchCommand = new CommonCommand(LockSwitch);
    }

    private void InitDefaultInfo(StudyCollectionItem studyCollectionItem) {
        string representationString = "";
        if (studyCollectionItem.IsSinglePatient()) {
            this.PatientName = studyCollectionItem.GetStudyComposition()[0].PatientName;
            representationString += this.PatientName + "\n";
            this.PatientAge = studyCollectionItem.GetStudyComposition()[0].PatientAge;
            representationString += this.PatientAge + "\n";
            this.PatientGender = studyCollectionItem.GetStudyComposition()[0].PatientGender;
            representationString += this.PatientGender + "\n";
            this.StudyUid = studyCollectionItem.GetStudyComposition()[0].StudyInstanceId;
            representationString += this.StudyUid;
        }
        else {
            this.PatientName = "多患者 (" + studyCollectionItem.GetStudiesNumber() + ")";
            this.PatientAge = studyCollectionItem.ToString();
            this.PatientGender = "";
            this.StudyUid = "";
            representationString = studyCollectionItem.ToString();
        }

        RepresentationStr = representationString;

    }

    #endregion
    
    #region COMMANDS

    public ICommand LockSwitchCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void LockSwitch(object o = null) {
        if (IsLocked) this.StudyCollectionItem.Unlock();
        else this.StudyCollectionItem.Lock();
    }

    #endregion

    #region PROPERTIES

    public StudyCollectionItem StudyCollectionItem;

    #endregion
    
    #region NOTIFIABLE_PROPERTIES

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

    private bool _isLocked;

    public bool IsLocked {
        get {
            return _isLocked;
        }
        set {
            if(_isLocked == value)return;
            _isLocked = value;
            LockingStatusStr = _isLocked ? "unlock" : " lock ";
            RisePropertyChanged(nameof(IsLocked));
        }
    }

    private bool _isLockable;

    public bool IsLockable {
        get {
            return _isLockable;
        }
        set {
            if(_isLockable == value)return;
            _isLockable = value;
            RisePropertyChanged(nameof(IsLockable));
        }
    }

    private string _lockingStatusStr;

    public string LockingStatusStr {
        get {
            return _lockingStatusStr;
        }
        set {
            if(_lockingStatusStr == value)return;
            _lockingStatusStr = value;
            RisePropertyChanged(nameof(LockingStatusStr));
        }
    }

    private string _representationStr;

    public string RepresentationStr {
        get {
            return _representationStr;
        }
        set {
            if(_representationStr == value)return;
            _representationStr = value;
            RisePropertyChanged(nameof(RepresentationStr));
        }
    }

    #endregion
    
    public void CloseStudy() {
        MainEntry_ModelFacade.GetInstance().DeleteStudyItem(this.StudyCollectionItem);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyCollectionItem.IsLocked))) {
            bool isLocked = (bool)o;
            IsLocked = isLocked;
            return;
        }

        if (propertyName.Equals(nameof(StudyCollectionItem.IsLockable))) {
            bool isLockable = (bool)o;
            IsLockable = isLockable;
            return;
        }

        if (propertyName.Equals(nameof(StudyCollectionItem.StudyAmountChanged))) {
            StudyCollectionItem studyCollectionItem = (StudyCollectionItem)o;
            InitDefaultInfo(studyCollectionItem);
            return;
        }

    }
}