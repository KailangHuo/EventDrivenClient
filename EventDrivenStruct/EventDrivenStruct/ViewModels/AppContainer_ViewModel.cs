using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainer_ViewModel : AbstractEventDrivenViewModel{

    public AppContainer_ViewModel(StudyAppMappingObj mappingObj) {
        _advancedAppViewModels = new ObservableCollection<AdvancedApp_ViewModel>();
        StudyAppMappingObj = mappingObj;
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private ObservableCollection<AdvancedApp_ViewModel> _advancedAppViewModels;

    private void AddAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        appModel.RegisterObserver(advancedAppViewModel);
        this._advancedAppViewModels.Add(advancedAppViewModel);
    }

    private void RemoveAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        this._advancedAppViewModels.Remove(advancedAppViewModel);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyAppMappingObj.AddAppModel))){
            AppModel appModel = (AppModel)o;
            AddAdvancedAppViewModel(appModel);
        }

        if (propertyName.Equals(nameof(StudyAppMappingObj.RemoveAppModel))) {
            AppModel appModel = (AppModel)o;
            RemoveAdvancedAppViewModel(appModel);
        }
    }
}