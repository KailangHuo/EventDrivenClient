using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainer_ViewModel : AbstractEventDrivenViewModel {

    public AppContainer_ViewModel(StudyAppMappingObj mappingObj) {
        AppSequenceManagerCollection = new ObservableCollection<AppSequenceManager_ViewModel>();
        SelectedCollection = new List<AppItem_ViewModel>();
        StudyAppMappingObj = mappingObj;
        VisibleAppModelList = new ObservableCollection<AppItem_ViewModel>();
        RunningAppList = new List<AppItem_ViewModel>();
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
            SelectedCollection.Add(null);
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

    private List<AppItem_ViewModel> SelectedCollection;


    private void AddAdvancedAppViewModel(AppItem_ViewModel appItemModel) {
        appItemModel.RegisterObserver(this);
        RunningAppList.Add(appItemModel);
        VisibleAppModelListAddItem(appItemModel);
        if (SelectedCollection.Contains(appItemModel)) return;
        AppSequenceManagerCollection[0].PeekNodeAppItem = appItemModel;
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
                    AppSequenceItem appSequenceItem = new AppSequenceItem(appItemModel, j);
                    AppSequenceManagerCollection[i+j].AddApp(appSequenceItem);
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
    public void SequenceManagerAppSelected(AppSequenceManager_ViewModel appSequenceManager) {
        AppItem_ViewModel appItemViewModel = appSequenceManager.PeekNodeAppItem;
        int sequenceManagerIndex = AppSequenceManagerCollection.IndexOf(appSequenceManager);
        // 重排序, 全部移除再全部添加
        SequenceManagersRemoveItem(appItemViewModel);
        SequenceMangersAddItem(appItemViewModel, sequenceManagerIndex);
        if (!RunningAppList.Contains(appSequenceManager.PeekNodeAppItem)) {
            PublishEvent(nameof(SequenceManagerAppSelected), appSequenceManager.PeekNodeAppItem);
        }
        //广播调起应用
        PublishSelectionFinished();
    }

    private void SelectedCollectionChanged(AppSequenceManager_ViewModel appSequenceManagerViewModel) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if (AppSequenceManagerCollection[i].Equals(appSequenceManagerViewModel)) {
                SelectedCollection[i] = appSequenceManagerViewModel.PeekNodeAppItem;
            }
        }
    }

    public void PublishSelectionFinished() {
        PublishEvent(nameof(PublishSelectionFinished), SelectedCollection);
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

        if (propertyName.Equals(nameof(AppSequenceManager_ViewModel.TryUpdatePeekNode))) {
            AppSequenceManager_ViewModel appSequenceManager = (AppSequenceManager_ViewModel)o;
            SelectedCollectionChanged(appSequenceManager);
        }
    }
}