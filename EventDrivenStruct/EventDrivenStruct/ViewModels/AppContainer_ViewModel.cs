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
        VisibleAppModelList = new ObservableCollection<AppItem_ViewModel>();
        RunningAppList = new List<AppItem_ViewModel>();
        SelectedInfoMap = new Dictionary<AppItem_ViewModel, int>();
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
            VisibleAppModelList.Add(new AppItem_ViewModel(new AppModel(ConstantApplist[i])));
        }
    }

    public ObservableCollection<AppSequenceManager_ViewModel> AppSequenceManagerCollection;

    public ObservableCollection<AppItem_ViewModel> VisibleAppModelList;

    private List<AppItem_ViewModel> RunningAppList;

    private Dictionary<AppItem_ViewModel, int> SelectedInfoMap;


    private void AddAdvancedAppViewModel(AppItem_ViewModel appItemModel) {
        appItemModel.RegisterObserver(this);
        RunningAppList.Add(appItemModel);
        VisibleAppModelListAddItem(appItemModel);
    }

    public void RemoveAdvancedAppViewModel(AppItem_ViewModel appItemModel) {
        appItemModel.DeregisterObserver(this);
        RunningAppList.Remove(appItemModel);
        VisibleAppModelListRemoveItem(appItemModel);
        SequenceManagersRemoveItem(appItemModel);
    }

    private void VisibleAppModelListAddItem(AppItem_ViewModel appItemModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appItemModel.AppName))  return;
        VisibleAppModelList.Add(appItemModel);
    }

    private void VisibleAppModelListRemoveItem(AppItem_ViewModel appItemModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appItemModel.AppName))  return;
        VisibleAppModelList.Remove(appItemModel);
    }

    private void SequenceMangersAddItem(AppItem_ViewModel appItemModel, int triggerIndex) {
        int endTriggerNumber = triggerIndex + appItemModel.MaxScreenConfigNumber;
        
        // 如果屏幕右界面超出屏幕数量, 矫正到最后一个屏幕
        int maxNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        if (endTriggerNumber > maxNumber) {
            triggerIndex = maxNumber - appItemModel.MaxScreenConfigNumber;
        }

        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if (i == triggerIndex) {
                for (int j = 0; j < appItemModel.MaxScreenConfigNumber; j++) {
                    AppSequenceManagerCollection[i+j].AddApp(appItemModel);
                }
                break;
            }
        }
        
    }

    private void SequenceManagersRemoveItem(AppItem_ViewModel appItemModel) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            AppSequenceManagerCollection[i].RemoveApp(appItemModel);
        }
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
        AppItem_ViewModel appItemViewModel = appSequenceManager.PeekNodeAppItem;
        int sequenceManagerIndex = AppSequenceManagerCollection.IndexOf(appSequenceManager);
        if (!RunningAppList.Contains(appSequenceManager.PeekNodeAppItem)) {
            PublishEvent(nameof(SequenceManagerAppSelected), appSequenceManager.PeekNodeAppItem);
        }
        // 触发添加重排序
        // 先全部移除
        SequenceManagersRemoveItem(appItemViewModel);
        //再添加到具体的seqMana里面
        SequenceMangersAddItem(appItemViewModel, sequenceManagerIndex);
        
        //广播调起应用
        PublishSelectionFinished();
    }

    public void PublishSelectionFinished() {
        Dictionary<AppItem_ViewModel, int> tempMap = new Dictionary<AppItem_ViewModel, int>();
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if (!tempMap.ContainsKey(AppSequenceManagerCollection[i].PeekNodeAppItem)) {
                tempMap.Add(AppSequenceManagerCollection[i].PeekNodeAppItem, i);
            }
        }
        if(tempMap.Equals(SelectedInfoMap)) return;
        SelectedInfoMap = tempMap;
        PublishEvent(nameof(PublishSelectionFinished), SelectedInfoMap);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyAppMappingObj.AddAppModel))){
            AppModel appModel = (AppModel)o;
            AppItem_ViewModel appItemViewModel = new AppItem_ViewModel(appModel);
            AddAdvancedAppViewModel(appItemViewModel);
        }

        if (propertyName.Equals(nameof(StudyAppMappingObj.RemoveAppModel))) {
            AppModel appModel = (AppModel)o;
            AppItem_ViewModel appItemViewModel = new AppItem_ViewModel(appModel);
            RemoveAdvancedAppViewModel(appItemViewModel);
        }

        if (propertyName.Equals(nameof(AppSequenceManager_ViewModel.PeekNodeAppItem))) {
            AppSequenceManager_ViewModel appSequenceManager = (AppSequenceManager_ViewModel)o;
            SequenceManagerAppSelected(appSequenceManager);
        }
        
        
    }
    

}