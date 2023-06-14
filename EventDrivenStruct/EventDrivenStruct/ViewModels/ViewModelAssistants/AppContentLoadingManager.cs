using EventDrivenElements;

namespace EventDrivenStruct.ViewModels.ViewModelAssistants; 

public class AppContentLoadingManager : AbstractEventDrivenViewModel {

    private static AppContentLoadingManager _instance;

    private AppContentLoadingManager() {
        
    }

    public static AppContentLoadingManager GetInstance() {
        if (_instance == null) {
            lock (typeof(AppContentLoadingManager)) {
                if (_instance == null) {
                    _instance = new AppContentLoadingManager();
                }
            }
        }

        return _instance;
    }

    private void NotifyAppsInitialize(AppContainer_ViewModel appContainerViewModel) {
        
    }


    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(AppTab_ViewModel.SelectedAppContainer))) {
            AppContainer_ViewModel appContainerViewModel = (AppContainer_ViewModel)o;
            NotifyAppsInitialize(appContainerViewModel);
        }
    }
}