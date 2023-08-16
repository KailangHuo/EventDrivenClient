using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class MainWindow_ViewModel : AbstractEventDrivenViewModel {
    
    #region CONSTRUCTION

    public MainWindow_ViewModel() {

        TestNumber = 0;
        
        StudyContainerViewModel = new StudyContainer_ViewModel();
        AppTabViewModel = new AppTab_ViewModel();
        ScreenManagerViewModel = ScreenManager_ViewModel.GetInstance();
        PatientAdminCenterViewModel = new PatientAdminCenter_ViewModel();
        
        StudyContainerViewModel.RegisterObserver(AppTabViewModel);
        AppTabViewModel.RegisterObserver(ScreenManagerViewModel);

        this.RegisterObserver(PopupManager.GetInstance());
        
        SetupCommands();
    }

    private void SetupCommands() {
        GotoPaTabCommand = new CommonCommand(GotoPaView);
        TEST_ADD_COMMAND = new CommonCommand(TEST_ADD);
        TEST_COMMAND = new CommonCommand(TEST_COMMAND_METHOD);
    }

    #endregion

    #region NOTIFIABLE_VIEW_MODELS

    public StudyContainer_ViewModel StudyContainerViewModel { get; private set; }

    public AppTab_ViewModel AppTabViewModel { get; private set; }

    public ScreenManager_ViewModel ScreenManagerViewModel { get; private set; }

    public PatientAdminCenter_ViewModel PatientAdminCenterViewModel { get; private set; }

    private string _actionButtonContent;

    public string ActionButtonContent {
        get {
            return _actionButtonContent;
        }
        set {
            if(_actionButtonContent == value) return;
            _actionButtonContent = value;
            RisePropertyChanged(nameof(ActionButtonContent));
        }
    }

    private int _testNumber;

    public int TestNumber {
        get {
            return _testNumber;
        }
        set {
            if(_testNumber == value) return;
            _testNumber = value;
            RisePropertyChanged(nameof(TestNumber));
        }
    }


    #endregion

    #region COMMANDS

    public ICommand GotoPaTabCommand { get; private set; }

    public ICommand TEST_ADD_COMMAND { get; private set; }

    public ICommand TEST_COMMAND { get; private set; }
    
    #endregion

    #region COMMAND_BINDING_METHODS

    public void TEST_COMMAND_METHOD(object o = null) {
        MainEntry_ModelFacade.GetInstance().TestAdd();
    }

    public void GotoPaView(object o = null) {
        int screenNumber = (int)o;
        this.AppTabViewModel.IsExpanded = false;
        this.ScreenManagerViewModel.InvokePatientAdmin(screenNumber);
    }

    public void TEST_ADD(object o = null) {
        PopupManager.GetInstance().MainWindow_AddWindowPopup();
    }
    

    #endregion

    #region PROPERTIES

    
    
    #endregion

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(MainEntry_ModelFacade.StudyCollection))) {
            StudyCollection studyCollection = (StudyCollection)o;
            studyCollection.RegisterObserver(StudyContainerViewModel);
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.StudyAppMappingManager))) {
            StudyAppMappingManager studyAppMappingManager = (StudyAppMappingManager)o;
            studyAppMappingManager.RegisterObserver(AppTabViewModel);
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.PatientAdminCenter))) {
            PatientAdminCenter patientAdminCenter = (PatientAdminCenter)o;
            patientAdminCenter.RegisterObserver(PatientAdminCenterViewModel);
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.ActionString))) {
            string str = (string)o;
            ActionButtonContent = str;
        }
    }

}