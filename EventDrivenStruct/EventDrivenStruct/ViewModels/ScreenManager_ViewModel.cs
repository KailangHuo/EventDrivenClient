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

    public void InvokePatientAdmin(int screenNumber) {
        
    }

    private void TryUpdateScreens(AppItemContainer_ViewModel appItemContainerViewModel) {
        if (appItemContainerViewModel == null) {
            for (int i = 0; i < ScreenCollection.Count; i++) {
                ScreenCollection[i].TryUpdateContent(null);
            }
        }
        else {
            for (int i = 0; i < ScreenCollection.Count; i++) {
                ScreenCollection[i].TryUpdateContent(appItemContainerViewModel.SelectedSequenceApps[i]);
            }   
        }
    }

    private void ResetScreens() {
        TryUpdateScreens(null);
    }


    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(AppTab_ViewModel.SelectedAppContainerSelectionChanged))) {
            AppItemContainer_ViewModel appItemContainerViewModel = (AppItemContainer_ViewModel)o;
            TryUpdateScreens(appItemContainerViewModel);
        }

        if (propertyName.Equals(nameof(AppTab_ViewModel.SelectedAppItemContainer))) {
            AppItemContainer_ViewModel appItemContainerViewModel = (AppItemContainer_ViewModel)o;
            TryUpdateScreens(appItemContainerViewModel);
        }

        if (propertyName.Equals(nameof(AppTab_ViewModel.IsExpanded))) {
            bool isExpanded = (bool)o;
            if(!isExpanded) ResetScreens();  
        }
    }
}