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
    
    public void PutStudyAppMapObj(StudyCollectionItem o) {
        
    }

    public StudyAppMappingObj GetStudyAppMapObject(StudyCollectionItem study) {
        
    }
    
}