using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainer_ViewModel : AbstractEventDrivenViewModel {

    public AppContainer_ViewModel(StudyAppMappingObj mappingObj) {
        AppSequenceManagerList = new ObservableCollection<AppSequenceManager_ViewModel>();
        StudyAppMappingObj = mappingObj;
        AppList = new ObservableCollection<AdvancedApp_ViewModel>();
        InitializeContainerNumber();
        InitializeConstantApps();
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private int _containerNumber;

    private void InitializeContainerNumber() {
        _containerNumber = 2;
        for (int i = 0; i < _containerNumber; i++) {
            AppSequenceManagerList.Add(new AppSequenceManager_ViewModel());
        }
    }

    private void InitializeConstantApps() {
        ConstantAppList = new List<AdvancedApp_ViewModel>();
        
    }

    public ObservableCollection<AppSequenceManager_ViewModel> AppSequenceManagerList;

    private ObservableCollection<AdvancedApp_ViewModel> AppList;

    private List<AdvancedApp_ViewModel> ConstantAppList;


    private void AddAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        appModel.RegisterObserver(advancedAppViewModel);
        AppListAddItem();
        this.AppList.Add(advancedAppViewModel);
        for (int i = 0; i < AppSequenceManagerList.Count; i++) {
            AppSequenceManagerList[i].AddApp(advancedAppViewModel);
        }
    }

    public void RemoveAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        AppListRemoveItem();
        this.AppList.Remove(advancedAppViewModel);
        for (int i = 0; i < AppSequenceManagerList.Count; i++) {
            AppSequenceManagerList[i].RemoveApp(advancedAppViewModel);
        }
    }

    private void AppListAddItem() {
        
    }

    private void AppListRemoveItem() {
        
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