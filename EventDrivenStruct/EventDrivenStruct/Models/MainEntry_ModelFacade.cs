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

    public void DeleteAppFromStudy(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(studyCollectionItem, appModel);
    }

    public void DeleteApp(AppModel appModel) {
        StudyAppMappingManager.RemoveAppFromStudyAppObj(appModel.StudyCollectionItem, appModel);
    }

    private int number;
    public void TestAdd() {
        if (number == 0) {
            StudyCollectionItem laoWang = MakeItem("老王",1);
            AppModel laowang_maxTest = new AppModel("MAXTEST", laoWang);
            AddStudyItemWithApp(laoWang, laowang_maxTest);
        }
        if (number == 1) {
            StudyCollectionItem laoWang = MakeItem("老王",1);
            AppModel laowang_OnOlogy = new AppModel("Oncology", laoWang);
            AddStudyItemWithApp(laoWang, laowang_OnOlogy);
        }
        if (number == 2) {
            StudyCollectionItem laoLi = MakeItem("li",2);
            AppModel laoLi_Dental = new AppModel("Dental", laoLi);
            AddStudyItemWithApp(laoLi, laoLi_Dental);
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