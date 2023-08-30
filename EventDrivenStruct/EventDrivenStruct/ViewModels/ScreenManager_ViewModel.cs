using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.ViewModels; 

public class ScreenManager_ViewModel : AbstractEventDrivenViewModel {

    private static ScreenManager_ViewModel _instance;

    public static ScreenManager_ViewModel GetInstance() {
        if (_instance == null) {
            lock (typeof(ScreenManager_ViewModel)) {
                if (_instance == null) {
                    _instance = new ScreenManager_ViewModel();
                }
            }
        }

        return _instance;
    }

    private ScreenManager_ViewModel() {
        InitScreens();
    }

    public List<Screen_ViewModel> ScreenCollection { get; private set; }

    private void InitScreens() {
        ScreenCollection = new List<Screen_ViewModel>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            ScreenCollection.Add(new Screen_ViewModel(i));
        }
    }

    private void TryUpdateScreens(List<AppSequenceItem> appList) {
        if (appList == null) {
            for (int i = 0; i < ScreenCollection.Count; i++) {
                ScreenCollection[i].TryUpdateContent(null);
            }
        }
        else {
            for (int i = 0; i < ScreenCollection.Count; i++) {
                ScreenCollection[i].TryUpdateContent(appList[i]);
            }   
        }
    }

    private void ResetScreens() {
        TryUpdateScreens(null);
        ScreenCleared();
    }

    public void ScreenCleared() {
        PublishEvent(nameof(ScreenCleared), null);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(AppTab_ViewModel.SelectedAppContainerAppSelectionChanged))) {
            AppItemContainer_ViewModel appItemContainerViewModel = (AppItemContainer_ViewModel)o;
            if(!appItemContainerViewModel.HasRunningApp) return;
            TryUpdateScreens(appItemContainerViewModel.SelectedSequenceApps);
            return;
        }

        if (propertyName.Equals(nameof(AppTab_ViewModel.SelectedAppItemContainer))) {
            AppItemContainer_ViewModel appItemContainerViewModel = (AppItemContainer_ViewModel)o;
            if(!appItemContainerViewModel.HasRunningApp) return;
            TryUpdateScreens(appItemContainerViewModel.SelectedSequenceApps);
            return;
        }

        if (propertyName.Equals(nameof(AppTab_ViewModel.IsExpanded))) {
            bool isExpanded = (bool)o;
            if(!isExpanded) ResetScreens();  
            return;
        }

        if (propertyName.Equals(nameof(PatientAdminAppManager_ViewModel.PaSelectionFinished))) {
            List<AppSequenceItem> list = (List<AppSequenceItem>)o;
            TryUpdateScreens(list);
            return;
        }

    }
}