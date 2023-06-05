using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class AppModel : AbstractEventDrivenObject{

    public AppModel() {
        
    }

    public string AppName { get; private set; }
}