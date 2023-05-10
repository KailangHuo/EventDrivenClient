using EventDrivenElements;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class MainEntry_ModelFacade : AbstractEventDrivenObject {

    private static MainEntry_ModelFacade _instance;

    private MainEntry_ModelFacade() {
        ExamContainer = new ExamContainer();
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

    private ExamContainer _examContainer;
    public ExamContainer ExamContainer {
        get {
            return _examContainer;
        }
        set {
            if(_examContainer == value) return;
            _examContainer = value;
            PublishEvent(nameof(ExamContainer), _examContainer);
        }
    }
    
    public void AddExam(Exam exam) {
        this.ExamContainer.AddExam(exam);
    }

    public void DeleteExam(Exam exam) {
        this.ExamContainer.DeleteExam(exam);
    }
    
}