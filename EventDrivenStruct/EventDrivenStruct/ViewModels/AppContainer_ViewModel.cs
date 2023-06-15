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
        VisibleAppModelList = new ObservableCollection<AdvancedApp_ViewModel>();
        RunningAppList = new List<AdvancedApp_ViewModel>();
        SelectedCollection = new List<AdvancedApp_ViewModel>();
        InitializeSequenceManagers();
        InitializeConstantApps();
        InitializeSelectedCollection();
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
            VisibleAppModelList.Add(new AdvancedApp_ViewModel(new AppModel(ConstantApplist[i])));
        }
    }

    private void InitializeSelectedCollection() {
        for (int i = 0; i < _sequenceManagerNumber; i++) {
            SelectedCollection.Add(null);
        }
    }

    public ObservableCollection<AppSequenceManager_ViewModel> AppSequenceManagerCollection;

    public ObservableCollection<AdvancedApp_ViewModel> VisibleAppModelList;

    private List<AdvancedApp_ViewModel> RunningAppList;

    private List<AdvancedApp_ViewModel> SelectedCollection;


    private void AddAdvancedAppViewModel(AdvancedApp_ViewModel appModel) {
        appModel.RegisterObserver(this);
        RunningAppList.Add(appModel);
        VisibleAppModelListAddItem(appModel);
        SequenceMangersAddItem(appModel);
    }

    public void RemoveAdvancedAppViewModel(AdvancedApp_ViewModel appModel) {
        appModel.DeregisterObserver(this);
        RunningAppList.Remove(appModel);
        VisibleAppModelListRemoveItem(appModel);
        SequenceManagersRemoveItem(appModel);
    }

    private void VisibleAppModelListAddItem(AdvancedApp_ViewModel appModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appModel.AppName))  return;
        VisibleAppModelList.Add(appModel);
    }

    private void VisibleAppModelListRemoveItem(AdvancedApp_ViewModel appModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appModel.AppName))  return;
        VisibleAppModelList.Remove(appModel);
    }

    private void SequenceMangersAddItem(AdvancedApp_ViewModel appModel) {
        int triggerIndex = GetAppTriggerIndex(appModel);
        int endTriggerNumber = triggerIndex + appModel.MaxScreenConfigNumber;
        
        // 如果屏幕右界面超出屏幕数量, 矫正到最后一个屏幕
        int maxNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        if (endTriggerNumber > maxNumber) {
            triggerIndex = maxNumber - appModel.MaxScreenConfigNumber;
        }

        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if (i == triggerIndex) {
                for (int j = 0; j < appModel.MaxScreenConfigNumber; j++) {
                    AppSequenceManagerCollection[i+j].AddApp(appModel);
                }
                break;
            }
        }
        
    }

    private void SequenceManagersRemoveItem(AdvancedApp_ViewModel appModel) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            AppSequenceManagerCollection[i].RemoveApp(appModel);
        }
    }

    private int GetAppTriggerIndex(AdvancedApp_ViewModel appViewModel) {
        
    }

    
    /// <summary>
    /// TODO: appSequenceMana 触发新应用的selec后 要先修改所有的顺序呢? 还是先添加?
    /// 先维护/修改顺序 -> 会通知应用管理器通知应用换屏, 但是新应用还没有打开过
    /// 先添加 -> 新应用进来后会面临不知道去哪个屏
    ///
    /// 先修改顺序 -> 但是不发送更新命令, 
    /// 可能要方法拆分 
    /// </summary>
    /// <param name="appSequenceManager"></param>
    public void SequenceManagerAppSelected(AppSequenceManager_ViewModel appSequenceManager) {
        if (!RunningAppList.Contains(appSequenceManager.SelectedApp)) {
            PublishEvent(nameof(SequenceManagerAppSelected), appSequenceManager.SelectedApp);
            return;
        }
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