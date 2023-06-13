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
        InitializeSequenceManagers();
        InitializeConstantApps();
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private int _sequenceManagerNumber;

    private void InitializeSequenceManagers() {
        _sequenceManagerNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < _sequenceManagerNumber; i++) {
            AppSequenceManager_ViewModel appSequenceManager = new AppSequenceManager_ViewModel();
            AppSequenceManagerCollection.Add(appSequenceManager);
            appSequenceManager.RegisterObserver(this);
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


    private void AddAdvancedAppViewModel(AdvancedApp_ViewModel appModel) {
        appModel.RegisterObserver(appModel);
        AppListAddItem(appModel);
        SequenceMangersAddItem(appModel);
    }

    public void RemoveAdvancedAppViewModel(AdvancedApp_ViewModel appModel) {
        AppListRemoveItem(appModel);
        SequenceManagersRemoveItem(appModel);
    }

    private void AppListAddItem(AdvancedApp_ViewModel appModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appModel.AppName))  return;
        AppList.Add(appModel);
    }

    private void AppListRemoveItem(AdvancedApp_ViewModel appModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appModel.AppName))  return;
        AppList.Remove(appModel);
    }

    private void SequenceMangersAddItem(AdvancedApp_ViewModel appModel, int triggerNumber = 0) {
        int endTriggerNumber = triggerNumber + appModel.MaxScreenConfigNumber;
        
        // 如果屏幕右界面超出屏幕数量, 矫正到最后一个屏幕
        int maxNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        if (endTriggerNumber > maxNumber) {
            triggerNumber = maxNumber - appModel.MaxScreenConfigNumber;
        }

        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if (i == triggerNumber) {
                for (int j = 0; j < appModel.MaxScreenConfigNumber; j++) {
                    AppSequenceManagerCollection[i+j].AddApp(appModel);
                }
                break;
            }
        }
        
    }

    private void SequenceManagersRemoveItem(AdvancedApp_ViewModel appModel, int triggerNumber = 0) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            AppSequenceManagerCollection[i].RemoveApp(appModel);
        }
    }

    private void SequenceManagerAppSelected(AppSequenceManager_ViewModel appSequenceManager) {
        AdvancedApp_ViewModel appViewModel = appSequenceManager.SelectedApp;
        int sequenceManagerIndex = AppSequenceManagerCollection.IndexOf(appSequenceManager);
        SequenceManagersRemoveItem(appViewModel);
        SequenceMangersAddItem(appViewModel, sequenceManagerIndex);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyAppMappingObj.AddAppModel))){
            AppModel appModel = (AppModel)o;
            AdvancedApp_ViewModel appViewModel = new AdvancedApp_ViewModel(appModel);
            AddAdvancedAppViewModel(appViewModel);
        }

        if (propertyName.Equals(nameof(StudyAppMappingObj.RemoveAppModel))) {
            AppModel appModel = (AppModel)o;
            AdvancedApp_ViewModel appViewModel = new AdvancedApp_ViewModel(appModel);
            RemoveAdvancedAppViewModel(appViewModel);
        }

        if (propertyName.Equals(nameof(AppSequenceManager_ViewModel.SelectedApp))) {
            AppSequenceManager_ViewModel appSequenceManager = (AppSequenceManager_ViewModel)o;
            SequenceManagerAppSelected(appSequenceManager);
        }
    }
    

}