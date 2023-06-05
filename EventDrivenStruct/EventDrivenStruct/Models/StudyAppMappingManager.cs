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
    
    public void PutStudyAppMapObj(StudyCollectionItem studyItem, AppModel appModel) {
        if (!_map.ContainsKey(studyItem)) {
            StudyAppMappingObj obj = new StudyAppMappingObj();
            obj.StudyCollectionItem = studyItem;
            obj.AppList = new List<AppModel>();
            obj.AppList.Add(appModel);
            
            _map.Add(studyItem, obj);
            PublishEvent(nameof(PutStudyAppMapObj), obj);
        }
        else {
            if (!_map[studyItem].AppList.Contains(appModel)) {
                _map[studyItem].AppList.Add(appModel);
                PublishEvent(nameof(PutStudyAppMapObj), _map[studyItem]);
            }
            
        }
    }

    public void RemoveStudyAppMapObj(StudyCollectionItem studyItem) {
        if (!_map.ContainsKey(studyItem)) {
            PublishEvent(nameof(RemoveStudyAppMapObj), null);
            return;
        }

        _map.Remove(studyItem);
        PublishEvent(nameof(RemoveStudyAppMapObj), studyItem);
    }
    

}