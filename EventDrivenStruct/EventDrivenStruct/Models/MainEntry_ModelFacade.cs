using EventDrivenElements;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class MainEntry_ModelFacade : AbstractEventDrivenObject {

    private static MainEntry_ModelFacade _instance;

    private MainEntry_ModelFacade() {
        StudyCollection = new StudyCollection();
        StudyAppMappingManager = StudyAppMappingManager.GetInstance();
        StudyCollection.RegisterObserver(StudyAppMappingManager);
        StudyAppMappingManager.RegisterObserver(StudyCollection);
        //TEST
        ActionString = "点击添加老王 三屏应用";
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

    private PatientAdminCenter _patientAdminCenter;

    public PatientAdminCenter PatientAdminCenter {
        get {
            return _patientAdminCenter;
        }
        set {
            if(_patientAdminCenter == value) return;
            _patientAdminCenter = value;
            PublishEvent(nameof(PatientAdminCenter), _patientAdminCenter);
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
        ActionString = "点击添加老王 三屏应用";
    }

    public void DeleteAppFromStudy(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(studyCollectionItem, appModel);
    }

    public void DeleteApp(AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(appModel.StudyCollectionItem, appModel);
    }


    public void InvokePatientAdminCenter() {
        this.PatientAdminCenter.Invoke();
    }



    private int number;
    
    public string _actionString;

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

    public void TestAdd() {
        if (number == 0) {
            StudyCollectionItem laoWang = MakeItem("老王",1);
            AppModel laowang_maxTest = new AppModel("三屏应用", laoWang);
            AddStudyItemWithApp(laoWang, laowang_maxTest);
            ActionString = "点击添加老王 Oncology应用";
        }
        if (number == 1) {
            StudyCollectionItem laoWang = MakeItem("老王",1);
            AppModel laowang_OnOlogy = new AppModel("Oncology", laoWang);
            AddStudyItemWithApp(laoWang, laowang_OnOlogy);
            ActionString = "点击添加老李 Dental应用";
        }
        if (number == 2) {
            StudyCollectionItem laoLi = MakeItem("老李",2);
            AppModel laoLi_Dental = new AppModel("Dental", laoLi);
            AddStudyItemWithApp(laoLi, laoLi_Dental);
            ActionString = "清除所有检查后才可使用";
        }

        number++;
    }
    
    private StudyCollectionItem MakeItem(string param1, int times) {
        StudyCollectionItem studyCollectionItem = new StudyCollectionItem();

        for (int i = 0; i < times; i++) {
            Study study = new Study(param1+ i + ". " );
            studyCollectionItem.AddInStudyComposition(study);
        }
            
        return studyCollectionItem;
    }

}