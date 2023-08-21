using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class MainWindow_ViewModel : AbstractEventDrivenViewModel {
    
    #region CONSTRUCTION

    public MainWindow_ViewModel() {

        TestNumber = 0;
        
        StudyContainerViewModel = new StudyContainer_ViewModel();
        AppTabViewModel = new AppTab_ViewModel();
        ScreenManagerViewModel = ScreenManager_ViewModel.GetInstance();
        PatientAdminAppManagerViewModel = new PatientAdminAppManager_ViewModel();
        
        StudyContainerViewModel.RegisterObserver(AppTabViewModel);
        AppTabViewModel.RegisterObserver(ScreenManagerViewModel);
        PatientAdminAppManagerViewModel.RegisterObserver(ScreenManagerViewModel);

        this.RegisterObserver(PopupManager.GetInstance());
        
        SetupCommands();
        IsFiveWindow = SystemConfiguration.GetInstance().GetScreenNumber() == 5;
        IsTwoWindow = SystemConfiguration.GetInstance().GetScreenNumber() == 2;
    }

    private void SetupCommands() {
        GotoPaTabCommand = new CommonCommand(GotoPaView);
        TEST_ADD_COMMAND = new CommonCommand(TEST_ADD);
        TEST_COMMAND = new CommonCommand(TEST_COMMAND_METHOD);
    }

    #endregion

    #region NOTIFIABLE_PROPERTIES

    public StudyContainer_ViewModel StudyContainerViewModel { get; private set; }

    public AppTab_ViewModel AppTabViewModel { get; private set; }

    public ScreenManager_ViewModel ScreenManagerViewModel { get; private set; }

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

    public bool IsFiveWindow { get; private set; }

    public bool IsTwoWindow { get; private set; }


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
        string str = (string)o;
        int screenNumber = int.Parse(str);
        this.AppTabViewModel.IsExpanded = false;
        this.PatientAdminAppManagerViewModel.InvokePaAt(screenNumber);
    }

    public void TEST_ADD(object o = null) {
        PopupManager.GetInstance().MainWindow_AddWindowPopup();
    }
    

    #endregion

    #region PROPERTIES

    public PatientAdminAppManager_ViewModel PatientAdminAppManagerViewModel { get; private set; }

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

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.ActionString))) {
            string str = (string)o;
            ActionButtonContent = str;
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.PatientAdminCenterApp))) {
            PatientAdminCenterApp patientAdminCenterApp = (PatientAdminCenterApp)o;
            PatientAdminCenterApp_ViewModel patientAdminCenterAppViewModel =
                new PatientAdminCenterApp_ViewModel(patientAdminCenterApp);
            patientAdminCenterAppViewModel.RegisterObserver(patientAdminCenterApp);
            PatientAdminAppManagerViewModel.InitPaCenter(patientAdminCenterAppViewModel);
        }

    }

}