using EventDrivenStruct.Models;
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

    public MainWindow_ViewModel MainWindowViewModel { get; private set; }

    public MainEntry_ModelFacade MainEntryModelFacade { get; private set; }

    public void RegisterMainWindow(MainWindow mainWindow) {
        this.MainWindow = mainWindow;
    }

    public void RegisterMainWindowViewModel(MainWindow_ViewModel mainWindowViewModel) {
        this.MainWindowViewModel = mainWindowViewModel;
        BuildObservantsRelationship();
    }

    public void RegisterModelFacade(MainEntry_ModelFacade mainEntryModelFacade) {
        this.MainEntryModelFacade = mainEntryModelFacade;
        BuildObservantsRelationship();
    }

    private void BuildObservantsRelationship() {
        if(MainWindowViewModel != null && MainEntryModelFacade != null) MainEntryModelFacade.RegisterObserver(MainWindowViewModel);
    }

}