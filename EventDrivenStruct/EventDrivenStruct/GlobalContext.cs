using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct; 

public class GlobalContext {
    private static GlobalContext _globalContext ;
    private GlobalContext() { }

    public static GlobalContext GetInstance() {
        if (_globalContext == null) {
            lock (typeof(GlobalContext)) {
                if (_globalContext == null) {
                    _globalContext = new GlobalContext();
                }
            }
        }

        return _globalContext;
    }

    public MainWindow MainWindow { get; private set; }

    public void RegisterMainWindowViewModel(MainWindow mainWindow) {
        this.MainWindow = mainWindow;
    }
}