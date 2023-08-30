
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AddExamWindow_ViewModel : AbstractEventDrivenViewModel{
    
    public AddExamWindow_ViewModel() {
        _study = new Study();
        ConfirmCommand = new CommonCommand(Confirm);
        CancleCommand = new CommonCommand(Cancle);
        SetUpApplicationTypeList();
    }

    #region PROPERTY

    private Study _study;

    private AppModel _appModel;

    #endregion

    #region NOTIFIABLE_PROPERTIES

    private bool _isLifeCycleEnd;

    public bool IsLifeCycleEnd {
        get {
            return _isLifeCycleEnd;
        }
        set {
            if(_isLifeCycleEnd == value)return;
            _isLifeCycleEnd = value;
            RisePropertyChanged(nameof(IsLifeCycleEnd));
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

    private string _apptype;

    public string AppType {
        get {
            return _apptype;
        }
        set {
            if(_apptype == value)return;
            _apptype = value;
            RisePropertyChanged(nameof(AppType));
        }
    }

    #endregion

    #region COMMAND

    public ICommand ConfirmCommand { get; private set; }
    public ICommand CancleCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHOD

    public void Confirm(object o = null) {
        if (string.IsNullOrEmpty(this.StudyInstanceId) || string.IsNullOrEmpty(AppType)) {
            MessageBox.Show("参数不完整！");
            return;
        }

        List<Study> studies = new List<Study>();
        studies.Add(_study);
        StudyCollectionItem studyCollectionItem = new StudyCollectionItem(studies);
        _appModel = new AppModel(_apptype, studyCollectionItem);
        
        MainEntry_ModelFacade.GetInstance().AddStudyItemWithApp(studyCollectionItem, _appModel);
        IsLifeCycleEnd = true;
    }

    public void Cancle(object o = null) {
        IsLifeCycleEnd = true;
    }


    #endregion
    

    

    private ObservableCollection<string> _appTypes;

    public ObservableCollection<string> AppTypes {
        get {
            return _appTypes;
        }
        set {
            _appTypes = value;
            RisePropertyChanged(nameof(AppTypes));
        }
    }

    public void SetUpApplicationTypeList() {
        AppTypes = new ObservableCollection<string>();
        List<string> typeList = SystemConfiguration.GetInstance().GetAppList();
        for (int i = 0; i < typeList.Count; i++) {
            AppTypes.Add(typeList[i]);
        }
    }
}