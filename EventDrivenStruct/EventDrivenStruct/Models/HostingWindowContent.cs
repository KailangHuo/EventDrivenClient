using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class HostingWindowContent : AbstractEventDrivenObject{
    
    private static HostingWindowContent _instance ;
    private HostingWindowContent() { }

    public static HostingWindowContent GetInstance() {
        if (_instance == null) {
            lock (typeof(HostingWindowContent)) {
                if (_instance == null) {
                    _instance = new HostingWindowContent();
                }
            }
        }

        return _instance;
    }
    
}