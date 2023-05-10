using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class ExamAppMappingManager : AbstractEventDrivenObject{
    private volatile static ExamAppMappingManager _instance;

    private ExamAppMappingManager() {
        _map = new Dictionary<Exam, ExamAppMappingObj>();
    }

    public static ExamAppMappingManager GetInstance() {
        if (_instance == null) {
            lock (typeof(ExamAppMappingManager)) {
                if (_instance == null) {
                    _instance = new ExamAppMappingManager();
                    
                }
            }
        }

        return _instance;
    }

    private Dictionary<Exam, ExamAppMappingObj> _map;
    
    public void PutStudyAppMapObj(ExamAppMappingObj o) {
        if (_map.ContainsKey(o.Exam)) {
            _map[o.Exam] = o;
        }
        else {
            _map.Add(o.Exam, o);
        }
    }

    public ExamAppMappingObj GetStudyAppMapObject(Exam exam) {
        if (_map.ContainsKey(exam)) return _map[exam];
        else return null;
    }
    
}