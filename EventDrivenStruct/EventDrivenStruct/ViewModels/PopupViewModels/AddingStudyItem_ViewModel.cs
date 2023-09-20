using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AddingStudyItem_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public AddingStudyItem_ViewModel() {
        _study = new Study();
        SetupCommands();
        CanClose = true;
    }

    private void SetupCommands() {
        this.CloseCommand = new CommonCommand(Close);
    }

    #endregion

    #region PROPERTIES

    private Study _study;

    public bool IsBlankFilledFinished() {
        return (!string.IsNullOrEmpty(this.StudyInstanceId));
    }

    public Study GetStudy() {
        return this._study;
    }

    #endregion

    #region NOTIFIABLE_PROPERTIES

    private bool _canClose;

    public bool CanClose {
        get {
            return _canClose;
        }
        set {
            if(_canClose == value)return;
            _canClose = value;
            RisePropertyChanged(nameof(CanClose));
        }
    }
    
    private string _studyInstanceId;

    public string StudyInstanceId {
        get {
            return _studyInstanceId;
        }
        set {
            if(_studyInstanceId == value) return;
            _studyInstanceId = value;
            _study.StudyInstanceId = value;
            _study.PatientName = value;
            _study.PatientGender = value;
            _study.PatientAge = value;
            RisePropertyChanged(nameof(StudyInstanceId));
        }
    }
    
    private string _patientName;

    public string PatientName {
        get {
            return _patientName;
        }
        set {
            if(_patientName == value) return;
            _patientName = value;
            _study.PatientName = _patientName;
            RisePropertyChanged(nameof(PatientName));
        }
    }
    
    private string _patientAge;

    public string PatientAge {
        get {
            return _patientAge;
        }
        set {
            if(_patientAge == value) return;
            _patientAge = value;
            _study.PatientAge = _patientAge;
            RisePropertyChanged(nameof(PatientAge));
        }
    }
    
    private string _patientGender;

    public string PatientGender {
        get {
            return _patientGender;
        }
        set {
            if(_patientGender == value) return;
            _patientGender = value;
            _study.PatientGender = _patientGender;
            RisePropertyChanged(nameof(PatientGender));
        }
    }

    #endregion

    #region COMMANDS

    public ICommand CloseCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    private void Close(object o = null) {
        PublishEvent(nameof(CloseCommand), this);
    }

    #endregion

}