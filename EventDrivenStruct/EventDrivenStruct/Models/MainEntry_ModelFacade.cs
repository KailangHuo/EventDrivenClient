using EventDrivenElements;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class MainEntry_ModelFacade : AbstractEventDrivenObject {

    private static MainEntry_ModelFacade _instance;

    private MainEntry_ModelFacade() {
        StudyCollection = new StudyCollection();
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
            _studyAppMappingManager = StudyAppMappingManager.GetInstance();
            PublishEvent(nameof(StudyAppMappingManager), _studyAppMappingManager);
        }
    }

    public void AddStudyItemWithApp(StudyCollectionItem studyItem, AppModel appModel) {
        AddStudyItem(studyItem); 
        AddAppToStudy(studyItem, appModel);
    }

    private void AddStudyItem(StudyCollectionItem studyCollectionItem) {
        StudyCollection.AddStudyCollectionItem(studyCollectionItem);
    }

    public void AddAppToStudy(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        if(StudyCollection.Contains(studyCollectionItem))StudyAppMappingManager.PutStudyAppMapObj(studyCollectionItem, appModel);
    }

    public void DeleteStudyItem(StudyCollectionItem studyItem) {
        if (StudyCollection.Contains(studyItem)) {
            StudyAppMappingManager.RemoveStudyAppMapObj(studyItem);
            StudyCollection.DeleteStudyCollectionItem(studyItem);
        }
    }

    public void DeleteAppFromStudy(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        if (StudyCollection.Contains(studyCollectionItem)) {
            StudyAppMappingManager.RemoveAppFromStudyAppObj(studyCollectionItem, appModel);
        }
    }

    public StudyAppMappingObj GetMappingObjByStudy(StudyCollectionItem studyCollectionItem) {
        
    }
}