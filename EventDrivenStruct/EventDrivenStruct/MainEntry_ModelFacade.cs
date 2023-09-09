using System.ComponentModel.DataAnnotations;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class MainEntry_ModelFacade : AbstractEventDrivenObject {

    private static MainEntry_ModelFacade _instance;

    private MainEntry_ModelFacade() {
        StudyCollection = new StudyCollection();
        StudyAppMappingManager = StudyAppMappingManager.GetInstance();
        PatientAdminCenterApp = PatientAdminCenterApp.GetInstance();
        
        StudyCollection.RegisterObserver(StudyAppMappingManager);
        StudyAppMappingManager.RegisterObserver(StudyCollection);
        //TEST
        TestStudyAppNode nextNode = SystemConfiguration.GetInstance().GetTestStudyList()[0];
        ActionString = "点击添加" + nextNode.patientName
                              + " "
                              + nextNode.appName;
        TriggeredActionBool = false;
    }

    public static MainEntry_ModelFacade GetInstance() {
        if (_instance == null) {
            lock (typeof(MainEntry_ModelFacade)) {
                if (_instance == null) {
                    _instance = new MainEntry_ModelFacade();
                }
            }
        }

        return _instance;
    }

    private StudyCollection _studyCollection;
    public StudyCollection StudyCollection {
        get {
            return _studyCollection;
        }
        set {
            if(_studyCollection == value) return;
            _studyCollection = value;
            PublishEvent(nameof(StudyCollection), _studyCollection);
        }
    }

    private StudyAppMappingManager _studyAppMappingManager;

    public StudyAppMappingManager StudyAppMappingManager {
        get {
            return _studyAppMappingManager;
        }
        set {
            if(_studyAppMappingManager == value) return;
            _studyAppMappingManager = value;
            PublishEvent(nameof(StudyAppMappingManager), _studyAppMappingManager);
        }
    }

    private PatientAdminCenterApp _patientAdminCenterApp;

    public PatientAdminCenterApp PatientAdminCenterApp {
        get {
            return _patientAdminCenterApp;
        }
        set {
            if (_patientAdminCenterApp == value) return;
            _patientAdminCenterApp = value;
            PublishEvent(nameof(PatientAdminCenterApp), _patientAdminCenterApp);
        }
    }

    public void AddStudyItemWithApp(StudyCollectionItem studyItem, AppModel appModel) {
        StudyCollection.AddStudyCollectionItem(studyItem);
        StudyAppMappingManager.AddAppToMapObj(studyItem, appModel);
    }

    public void AddAppToStudy(StudyCollectionItem studyItem, AppModel appModel) {
        if(!StudyCollection.Contains(studyItem) || studyItem == null) return;
        StudyAppMappingManager.AddAppToMapObj(studyItem, appModel);
    }

    public void DeleteStudyItem(StudyCollectionItem studyItem) {
        StudyCollection.DeleteStudyCollectionItem(studyItem);
    }

    public void DeleteAllStudy() {
        StudyCollection.DeleteAllStudyCollectionItem();
        // TEST_ONLY
        number = 0;
        TestStudyAppNode nextNode = SystemConfiguration.GetInstance().GetTestStudyList()[0];
        ActionString = "点击添加" + nextNode.patientName
                              + " "
                              + nextNode.appName;
        TriggeredActionBool = false;
    }

    public void DeleteAppFromStudy(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(studyCollectionItem, appModel);
    }

    public void DeleteApp(AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(appModel.StudyCollectionItem, appModel);
    }
    


    private int number;
    
    private string _actionString;

    public string ActionString {
        get {
            return _actionString;
        }
        set {
            if(_actionString == value) return;
            _actionString = value;
            PublishEvent(nameof(ActionString), _actionString);
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
            PublishEvent(nameof(TriggeredActionBool), _triggeredActionBool);
        }
    }

    public void TestAdd() {
        if (number >= SystemConfiguration.GetInstance().GetTestStudyList().Count) {
            return;
        }

        AppModel appModel = SystemConfiguration.GetInstance().GetTestStudyList()[number].ConvertToAppModel();
        AddStudyItemWithApp(appModel.StudyCollectionItem, appModel);
        
        number++;

        if (number < SystemConfiguration.GetInstance().GetTestStudyList().Count) {
            TestStudyAppNode nextNode = SystemConfiguration.GetInstance().GetTestStudyList()[number];
            ActionString = "点击添加" + nextNode.patientName
                                  + " "
                                  + nextNode.appName;
        }
        else {
            ActionString = "清除所有检查后才可使用";
            TriggeredActionBool = true;
        }


    }

}