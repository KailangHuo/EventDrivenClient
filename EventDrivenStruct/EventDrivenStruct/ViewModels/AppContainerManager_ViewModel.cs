using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainerManager_ViewModel : AbstractEventDrivenViewModel {

    public AppContainerManager_ViewModel(StudyAppMappingObj mappingObj) {
        AppContainerList = new ObservableCollection<AppContainer_ViewModel>();
        StudyAppMappingObj = mappingObj;
        InitializeContainerNumber();
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private int _containerNumber;

    private void InitializeContainerNumber() {
        _containerNumber = 2;
        for (int i = 0; i < _containerNumber; i++) {
            AppContainerList.Add(new AppContainer_ViewModel());
        }
    }

    public ObservableCollection<AppContainer_ViewModel> AppContainerList;


    private void AddAdvancedAppViewModel(AppModel appModel) {
        for (int i = 0; i < AppContainerList.Count; i++) {
            AppContainerList[i].AddApp(appModel);
        }
    }

    private void RemoveAdvancedAppViewModel(AppModel appModel) {
        for (int i = 0; i < AppContainerList.Count; i++) {
            AppContainerList[i].RemoveApp(appModel);
        }
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