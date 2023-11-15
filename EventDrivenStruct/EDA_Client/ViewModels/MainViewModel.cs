using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class MainViewModel : AbstractEventDrivenViewModel {
    
    #region CONSTRUCTION

    public MainViewModel() {

        //TEST ONLY
        TestNumber = 0;
        SystemInfoCollector = SystemInfoCollector.GetInstance();
        
        StudyContainerViewModel = new StudyContainer_ViewModel();
        AppTabViewModel = new AppTab_ViewModel();
        ScreenManagerViewModel = ScreenManager_ViewModel.GetInstance();
        PatientAdminAppManagerViewModel = new PatientAdminAppManager_ViewModel();
        
        StudyContainerViewModel.RegisterObserver(AppTabViewModel);
        StudyContainerViewModel.RegisterObserver(PatientAdminAppManagerViewModel);
        
        AppTabViewModel.RegisterObserver(ScreenManagerViewModel);
        AppTabViewModel.RegisterObserver(PatientAdminAppManagerViewModel);
        
        PatientAdminAppManagerViewModel.RegisterObserver(ScreenManagerViewModel);

        this.RegisterObserver(PopupManager.GetInstance());
        
        SetupCommands();
        // TEST 
        this.TestPanelStatus = SystemConfiguration.GetInstance().GetTestPanelStatus();
    }

    private void SetupCommands() {
        GotoPaTabCommand = new CommonCommand(GotoPaView);
        TEST_ADD_COMMAND = new CommonCommand(TEST_ADD);
        TEST_COMMAND = new CommonCommand(TEST_COMMAND_METHOD);
        TEST_CLEARALL_COMMAND = new CommonCommand(TEST_CLEARALL);
        TEST_APPEND_COMMAND = new CommonCommand(TEST_APPEND);
    }

    #endregion

    #region NOTIFIABLE_PROPERTIES

    public StudyContainer_ViewModel StudyContainerViewModel { get; private set; }

    public AppTab_ViewModel AppTabViewModel { get; private set; }

    public ScreenManager_ViewModel ScreenManagerViewModel { get; private set; }

    public SystemInfoCollector SystemInfoCollector { get; private set; }

    public bool TestPanelStatus { get; private set; }

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
    
    private bool _triggeredActionBool;

    public bool TriggeredActionBool {
        get {
            return _triggeredActionBool;
        }
        set {
            if(_triggeredActionBool == value) return;
            _triggeredActionBool = value;
            RisePropertyChanged(nameof(TriggeredActionBool));
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

    public ICommand TEST_CLEARALL_COMMAND { get; private set; }

    public ICommand TEST_APPEND_COMMAND { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    private void TEST_COMMAND_METHOD(object o = null) {
        MainEntry_ModelFacade.GetInstance().TestAdd();
    }

    private void GotoPaView(object o = null) {
        int screenIndex = (int)o;
        this.AppTabViewModel.IsExpanded = false;
        this.PatientAdminAppManagerViewModel.InvokePaAt(screenIndex);
    }

    private void TEST_ADD(object o = null) {
        int screenIndex = (int)o;
        PopupManager.GetInstance().MainWindow_AddWindowPopup(screenIndex);
    }

    private void TEST_CLEARALL(object o = null) {
        this.StudyContainerViewModel.ClearAll(o);
    }

    private void TEST_APPEND(object o = null) {
        int screenIndex = (int)o;
        PopupManager.GetInstance().MainWindow_AppendWindowPopup(this.AppTabViewModel, screenIndex);
    }


    #endregion

    #region PROPERTIES

    public PatientAdminAppManager_ViewModel PatientAdminAppManagerViewModel { get; private set; }

    #endregion

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(MainEntry_ModelFacade.StudyCollection))) {
            StudyCollection studyCollection = (StudyCollection)o;
            studyCollection.RegisterObserver(StudyContainerViewModel);
            return;
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.StudyAppMappingManager))) {
            StudyAppMappingManager studyAppMappingManager = (StudyAppMappingManager)o;
            studyAppMappingManager.RegisterObserver(AppTabViewModel);
            return;
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.ActionString))) {
            string str = (string)o;
            ActionButtonContent = str;
            return;
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.PatientAdminCenterApp))) {
            PatientAdminCenterApp patientAdminCenterApp = (PatientAdminCenterApp)o;
            PatientAdminCenterApp_ViewModel patientAdminCenterAppViewModel =
                new PatientAdminCenterApp_ViewModel(patientAdminCenterApp);
            patientAdminCenterAppViewModel.RegisterObserver(patientAdminCenterApp);
            PatientAdminAppManagerViewModel.InitPaCenter(patientAdminCenterAppViewModel);
            return;
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.TriggeredActionBool))) {
            this.TriggeredActionBool = (bool)o;
            return;
        }

    }

}