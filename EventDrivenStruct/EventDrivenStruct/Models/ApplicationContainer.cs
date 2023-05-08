using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class ApplicationContainer : AbstractEventDrivenObject{
    
    private static ApplicationContainer _applicationContainer ;

    private ApplicationContainer() { }

    public static ApplicationContainer GetInstance() {
        if (_applicationContainer == null) {
            lock (typeof(ApplicationContainer)) {
                if (_applicationContainer == null) {
                    _applicationContainer = new ApplicationContainer();
                }
            }
        }

        return _applicationContainer;
    }
    
}