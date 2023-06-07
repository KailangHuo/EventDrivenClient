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
            obj.AddAppModel(appModel); 
            _map.Add(studyItem, obj);
        }
        else {
            _map[studyItem].AddAppModel(appModel);
        }
    }


    public void RemoveStudyAppMapObj(StudyCollectionItem studyItem) { 
        _map.Remove(studyItem);
    }

    public void RemoveAppFromStudyAppObj(StudyCollectionItem studyCollectionItem, AppModel appModel) {
        if(!_map.ContainsKey(studyCollectionItem)) return;
        _map[studyCollectionItem].RemoveAppModel(appModel);
    }

}