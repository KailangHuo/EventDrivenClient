using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class MainEntry_ModelFacade : AbstractEventDrivenObject {

    #region CONSTRUCTION

    private static MainEntry_ModelFacade _instance;

    private MainEntry_ModelFacade() {
        this.MissionExecutionBuffer = new MissionExecutionBuffer();
        this.WorkerThread = new Thread(WorkerStart);
        this.WorkerThread.Start();
        
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

    private MissionExecutionBuffer MissionExecutionBuffer;

    private Thread WorkerThread;

    private void WorkerStart() {
        while (true) {
            Mission mission = MissionExecutionBuffer.TakeTopMission();
            if (mission.MissionType == MissionType.NULL) {
                Thread.Sleep(100);
                continue;
            }

            mission.Execute();
        }
    }

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

    #region WORKER_METHOD

    private bool AddStudyItemWithAppWorker(StudyCollectionItem studyItem, AppModel appModel) {
        StudyCollection.AddStudyCollectionItem(studyItem);
        return StudyAppMappingManager.AddAppToMapObj(studyItem, appModel);
    }

    private bool AppendStudyToStudyCollectionItemWorker(StudyCollectionItem studyItem, List<Study> studies) {
        if(!StudyCollection.Contains(studyItem) || studyItem == null) {
            ExceptionManager.GetInstance().ThrowAsyncException("声明的Study不存在!");
            return false;
        }
        
        return StudyCollection.AppendStudyToCollectionItem(studyItem, studies);
    }
    
    private bool AddAppToStudyWorker(StudyCollectionItem studyItem, AppModel appModel) {
        if(!StudyCollection.Contains(studyItem) || studyItem == null) {
            ExceptionManager.GetInstance().ThrowAsyncException("声明的Study不存在!");
            return false;
        }
        return StudyAppMappingManager.AddAppToMapObj(studyItem, appModel);
    }
    
    private void DeleteStudyItemWorker(StudyCollectionItem studyItem) {
        StudyCollection.DeleteStudyCollectionItem(studyItem);
    }
    
    private void DeleteAllStudyWorker() {
        StudyCollection.DeleteAllStudyCollectionItem();
        // TEST_ONLY
        number = 0;
        TestStudyAppNode nextNode = SystemConfiguration.GetInstance().GetTestStudyList()[0];
        ActionString = "点击添加" + nextNode.patientName
                              + " "
                              + nextNode.appName;
        TriggeredActionBool = false;
    }
    
    private void DeleteAppWorker(AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(appModel.StudyCollectionItem, appModel);
    }
    
    #endregion
    
    public bool AddStudyItemWithApp(StudyCollectionItem studyItem, AppModel appModel) {
        List<object> paramList = new List<object>();
        paramList.Add(studyItem);
        paramList.Add(appModel);
        Mission mission = new Mission(MissionType.SERIAL, this, "AddStudyItemWithAppWorker", paramList);
        return this.MissionExecutionBuffer.PutMission(mission);
    }

    public bool AppendStudyToStudyCollectionItem(StudyCollectionItem studyItem, List<Study> studies) {
        List<object> paramList = new List<object>();
        paramList.Add(studyItem);
        paramList.Add(studies);
        Mission mission = new Mission(MissionType.SERIAL, this, "AppendStudyToStudyCollectionItemWorker", paramList);
        return this.MissionExecutionBuffer.PutMission(mission);
    }

    public bool AddAppToStudy(StudyCollectionItem studyItem, AppModel appModel) {
        List<object> paramList = new List<object>();
        paramList.Add(studyItem);
        paramList.Add(appModel);
        Mission mission = new Mission(MissionType.SERIAL, this, "AddAppToStudyWorker", paramList);
        return this.MissionExecutionBuffer.PutMission(mission);
    }
    
    public void DeleteStudyItem(StudyCollectionItem studyItem) {
        List<object> paramList = new List<object>();
        paramList.Add(studyItem);
        Mission mission = new Mission(MissionType.SERIAL, this, "DeleteStudyItemWorker", paramList);
        this.MissionExecutionBuffer.PutMission(mission);
    }
    
    public void DeleteAllStudy() {
        List<object> paramList = new List<object>();
        Mission mission = new Mission(MissionType.SERIAL, this, "DeleteAllStudyWorker", paramList);
        this.MissionExecutionBuffer.PutMission(mission);
    }

    public void DeleteApp(AppModel appModel) {
        List<object> paramList = new List<object>();
        paramList.Add(appModel);
        Mission mission = new Mission(MissionType.SERIAL, this, "DeleteAppWorker", paramList);
        this.MissionExecutionBuffer.PutMission(mission);    
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
        List<object> paramList = new List<object>();
        Mission mission = new Mission(MissionType.SERIAL, this, "TestAddWorker", paramList);
        this.MissionExecutionBuffer.PutMission(mission);  
    }

    private void TestAddWorker() { 
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

    #endregion
    
    
}