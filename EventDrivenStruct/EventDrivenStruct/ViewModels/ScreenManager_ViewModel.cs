using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.ViewModels; 

public class ScreenManager_ViewModel : AbstractEventDrivenViewModel{

    public ScreenManager_ViewModel() {
        InitScreens();
        
    }

    public List<Screen_ViewModel> ScreenCollection;

    private void InitScreens() {
        ScreenCollection = new List<Screen_ViewModel>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            ScreenCollection.Add(new Screen_ViewModel(i));
        }
    }

    private void TryUpdateScreens(AppItemContainer_ViewModel appItemContainerViewModel) {
        for (int i = 0; i < ScreenCollection.Count; i++) {
            ScreenCollection[i].TryUpdateContent(appItemContainerViewModel.SelectedSequenceApps[i]);
        }
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

    }
}