using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class AppModel : AbstractEventDrivenObject{

    public AppModel() {
        
    }

    public AppModel(string name) {
        AppName = name;
    }

    public string AppName { get; private set; }
}