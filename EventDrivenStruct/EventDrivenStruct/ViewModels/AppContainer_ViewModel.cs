using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainer_ViewModel : AbstractEventDrivenViewModel {

    public AppContainer_ViewModel(StudyAppMappingObj mappingObj) {
        AppSequenceManagerCollection = new ObservableCollection<AppSequenceManager_ViewModel>();
        StudyAppMappingObj = mappingObj;
        AppList = new ObservableCollection<AdvancedApp_ViewModel>();
        InitializeContainerNumber();
        InitializeConstantApps();
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private int _sequenceManagerNumber;

    private void InitializeContainerNumber() {
        _sequenceManagerNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < _sequenceManagerNumber; i++) {
            AppSequenceManagerCollection.Add(new AppSequenceManager_ViewModel());
        }
    }

    private void InitializeConstantApps() {
        List<string> ConstantApplist = SystemConfiguration.GetInstance().GetConstantAppList();
        for (int i = 0; i < ConstantApplist.Count; i++) {
            AppList.Add(new AdvancedApp_ViewModel(new AppModel(ConstantApplist[i])));
        }
    }

    public ObservableCollection<AppSequenceManager_ViewModel> AppSequenceManagerCollection;

    public ObservableCollection<AdvancedApp_ViewModel> AppList;


    private void AddAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        appModel.RegisterObserver(advancedAppViewModel);
        AppListAddItem(appModel);
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            AppSequenceManagerCollection[i].AddApp(advancedAppViewModel);
        }
    }

    public void RemoveAdvancedAppViewModel(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        AppListRemoveItem(appModel);
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            AppSequenceManagerCollection[i].RemoveApp(advancedAppViewModel);
        }
    }

    private void AppListAddItem(AppModel appModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appModel.AppName))  return;
        AppList.Add(new AdvancedApp_ViewModel(appModel));
    }

    private void AppListRemoveItem(AppModel appModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appModel.AppName))  return;
        AppList.Remove(new AdvancedApp_ViewModel(appModel));
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