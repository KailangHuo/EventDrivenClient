
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AddExamWindow_ViewModel : AbstractEventDrivenViewModel{
    
    public AddExamWindow_ViewModel() {
        ConfirmCommand = new CommonCommand(Confirm);
        CancleCommand = new CommonCommand(Cancle);
        SetUpApplicationTypeList();
        _exam = new Exam();
        _app = new DiagnoApplication();
        _mappingObj = new ExamAppMappingObj();
        _mappingObj.Exam = _exam;
        _mappingObj.AppList.Add(_app);
    }

    #region PROPERTY

    private Exam _exam;

    private DiagnoApplication _app;

    private ExamAppMappingObj _mappingObj;

    #endregion

    #region COMMAND

    public ICommand ConfirmCommand { get; private set; }
    public ICommand CancleCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHOD

    public void Confirm(object o = null) {
        if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.ExamID) || string.IsNullOrEmpty(AppType)) {
            MessageBox.Show("参数不完整！");
            return;
        }


        PublishEvent(nameof(Confirm), this._mappingObj);
        AddExamWindow addExamWindow = (AddExamWindow)o;
        addExamWindow.Close();
        /*StudyDTO studyDto = new StudyDTO(this);
        ApplicationDTO appDto = new ApplicationDTO(studyDto, AppType);
        StudyAppMapManager.GetInstance().RegisterStudyAppMapObj(new StudyAppMapObject(studyDto, appDto));
        GlobalContext.GetInstance().ClientViewModel.OpenStudyAndApplication(studyDto);*/
    }

    public void Cancle(object o = null) {
        AddExamWindow addExamWindow = (AddExamWindow)o;
        addExamWindow.Close();
        //this._openStudyWindow.Close();
    }


    #endregion
    

    private string _examId;

    public string ExamID {
        get {
            return _examId;
        }
        set {
            if(_examId == value) return;
            _examId = value;
            _exam.ExamInstanceId = value;
            RisePropertyChanged(nameof(ExamID));
        }
    }
    
    private string _name;

    public string Name {
        get {
            return _name;
        }
        set {
            if(_name == value) return;
            _name = value;
            _exam.PatientInfo.PatientName = value;
            RisePropertyChanged(nameof(Name));
        }
    }
    
    private string _age;

    public string Age {
        get {
            return _age;
        }
        set {
            if(_age == value) return;
            _age = value;
            _exam.PatientInfo.PatientAge = value;
            RisePropertyChanged(nameof(Age));
        }
    }
    
    private string _gender;

    public string Gender {
        get {
            return _gender;
        }
        set {
            if(_gender == value) return;
            _gender = value;
            _exam.PatientInfo.PatientGender = value;
            RisePropertyChanged(nameof(Gender));
        }
    }

    private string _apptype;

    public string AppType {
        get {
            return _apptype;
        }
        set {
            _apptype = value;
            RisePropertyChanged(nameof(AppType));
        }
    }

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
        AppTypes.Add("Web2D");
        AppTypes.Add("Review3D");
        AppTypes.Add("MMFusion");
        AppTypes.Add("Report");
    }
}