using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyAppMappingManager : AbstractEventDrivenObject{
    
    private volatile static StudyAppMappingManager _instance;

    private StudyAppMappingManager() {
        _map = new Dictionary<StudyCollectionItem, StudyAppMappingObj>();
    }

    public static StudyAppMappingManager GetInstance() {
        if (_instance == null) {
            lock (typeof(StudyAppMappingManager)) {
                if (_instance == null) {
                    _instance = new StudyAppMappingManager();
                    
                }
            }
        }

        return _instance;
    }

    private Dictionary<StudyCollectionItem, StudyAppMappingObj> _map;
    
    public void PutStudyAppMapObj(StudyCollectionItem studyCollectionItem) {
        if(_map.ContainsKey(studyCollectionItem)) return;
        StudyAppMappingObj obj = new StudyAppMappingObj();
        obj.StudyCollectionItem = studyCollectionItem;
        _map.Add(studyCollectionItem, obj);
        obj.RegisterObserver(this);
        PublishEvent(nameof(PutStudyAppMapObj), obj);
    }

    public bool AddAppToMapObj(StudyCollectionItem studyItem, AppModel appModel) {
        if(!_map.ContainsKey(studyItem)) return false;
        _map[studyItem].AddAppModel((appModel));
        return true;
    }


    public void RemoveStudyAppMapObj(StudyCollectionItem studyItem) {
        if (_map.ContainsKey(studyItem)) {
            StudyAppMappingObj studyAppMappingObj = _map[studyItem];
            _map.Remove(studyItem);
            PublishEvent(nameof(RemoveStudyAppMapObj), studyAppMappingObj);
        }
    }

    public void RemoveAppFromStudyAppObj(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        if(!_map.ContainsKey(studyCollectionItem)) return;
        _map[studyCollectionItem].RemoveAppModel(appModel);
    }

    public void RemoveAllStudyAppObj() {
        _map = new Dictionary<StudyCollectionItem, StudyAppMappingObj>();
        PublishEvent(nameof(RemoveAllStudyAppObj), null);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyCollection.AddStudyCollectionItem))) {
            StudyCollectionItem item = (StudyCollectionItem)o; 
            PutStudyAppMapObj(item);
            return;
        }

        if (propertyName.Equals(nameof(StudyCollection.DeleteStudyCollectionItem))) {
            StudyCollectionItem item = (StudyCollectionItem)o;
            RemoveStudyAppMapObj(item);
            return;
        }

        if (propertyName.Equals(nameof(StudyAppMappingObj.AppListEmpty))) {
            StudyAppMappingObj obj = (StudyAppMappingObj)o;
            RemoveStudyAppMapObj(obj.StudyCollectionItem);
            return;
        }

        if (propertyName.Equals(nameof(StudyCollection.DeleteAllStudyCollectionItem))) {
            RemoveAllStudyAppObj();
            return;
        }

    }
}