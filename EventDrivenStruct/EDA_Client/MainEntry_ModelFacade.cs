using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class MainEntry_ModelFacade : AbstractEventDrivenModel {

    #region CONSTRUCTION

    private static MainEntry_ModelFacade _instance;

    private MainEntry_ModelFacade() {
        this.MissionThreadPoolExecutor = new MissionThreadPoolExecutor(3, 10);
        
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

    private MissionThreadPoolExecutor MissionThreadPoolExecutor;

    #endregion

    #region NOTIFIABLE_PROPERTIES

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

    #endregion
    
    
    public void AddStudyItemWithApp(StudyCollectionItem studyItem, AppModel appModel) {
        Mission mission = new Mission(() => {
            StudyCollection.AddStudyCollectionItem(studyItem);
            StudyAppMappingManager.AddAppToMapObj(studyItem, appModel);
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);

    }

    public void AppendStudyToStudyCollectionItem(StudyCollectionItem studyItem, List<Study> studies) {
        Mission mission = new Mission(() => {
            if(!StudyCollection.Contains(studyItem) || studyItem == null) {
                ExceptionManager.GetInstance().ThrowAsyncException("声明的Study不存在!");
                return;
            }
            StudyCollection.AppendStudyToCollectionItem(studyItem, studies);
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);
    }

    public void AddAppToStudy(StudyCollectionItem studyItem, AppModel appModel) {
        Mission mission = new Mission(() => {
            if(!StudyCollection.Contains(studyItem) || studyItem == null) {
                ExceptionManager.GetInstance().ThrowAsyncException("声明的Study不存在!");
                return;
            }
            StudyAppMappingManager.AddAppToMapObj(studyItem, appModel);
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);
    }
    
    public void DeleteStudyItem(StudyCollectionItem studyItem) {
        Mission mission = new Mission(() => {
            StudyCollection.DeleteStudyCollectionItem(studyItem);
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);
    }
    
    public void DeleteAllStudy() {
        Mission mission = new Mission(() => {
            StudyCollection.DeleteAllStudyCollectionItem();
            // TEST_ONLY
            number = 0;
            TestStudyAppNode nextNode = SystemConfiguration.GetInstance().GetTestStudyList()[0];
            ActionString = "点击添加" + nextNode.patientName
                                  + " "
                                  + nextNode.appName;
            TriggeredActionBool = false;
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);
    }

    public void DeleteApp(AppModel appModel) {
        Mission mission = new Mission(() => {
            StudyAppMappingManager.RemoveAppFromStudyAppObj(appModel.StudyCollectionItem, appModel);
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);
    }

    #region |||TEST|||

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
        Mission mission = new Mission(() => {
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
        });
        MissionThreadPoolExecutor.EnqueueMission(mission);
    }

    #endregion
    
    
}