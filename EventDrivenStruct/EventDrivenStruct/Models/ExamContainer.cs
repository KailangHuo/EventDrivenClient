using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class ExamContainer : AbstractEventDrivenObject{
    
    private static ExamContainer _instance ;
    private ExamContainer() { }

    public static ExamContainer GetInstance() {
        if (_instance == null) {
            lock (typeof(ExamContainer)) {
                if (_instance == null) {
                    _instance = new ExamContainer();
                }
            }
        }

        return _instance;
    }
    
}