using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainer_ViewModel : AbstractEventDrivenViewModel{

    public AppContainer_ViewModel() {
        _advancedAppViewModels = new ObservableCollection<AdvancedApp_ViewModel>();
    }

    private ObservableCollection<AdvancedApp_ViewModel> _advancedAppViewModels;

    private void AddAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        this._advancedAppViewModels.Add(advancedAppViewModel);
    }

    private void RemoveAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        this._advancedAppViewModels.Remove(advancedAppViewModel);
    }

    public void UpdateAppContainer(List<AppModel> appList) {
        _advancedAppViewModels.Clear();
        for (int i = 0; i < appList.Count; i++) {
            AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appList[i]);
            _advancedAppViewModels.Add(advancedAppViewModel);
        }
    }


}