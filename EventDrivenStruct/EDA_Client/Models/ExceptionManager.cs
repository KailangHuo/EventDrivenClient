using EventDrivenElements;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct.Models; 

public class ExceptionManager : AbstractEventDrivenModel {

    private static ExceptionManager _instance;

    public ExceptionManager() {
    }

    public static ExceptionManager GetInstance() {
        if (_instance == null) {
            lock (typeof(ExceptionManager)) {
                if (_instance == null) {
                    _instance = new ExceptionManager();
                }
            }
        }

        return _instance;
    }

    public void ThrowAsyncException(string content) {
        PublishEvent(nameof(ThrowAsyncException), content);
    }

}