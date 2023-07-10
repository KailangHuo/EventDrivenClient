using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTitleItemContainer_ViewModel : AbstractEventDrivenViewModel {

    public AppTitleItemContainer_ViewModel(StudyAppMappingObj mappingObj) {
        AppSequenceManagerCollection = new ObservableCollection<AppTitleSequenceManager_ViewModel>();
        SelectedCollection = new List<AppTitleSequenceItem>();
        StudyAppMappingObj = mappingObj;
        VisibleAppModelList = new ObservableCollection<AppTitleItem_ViewModel>();
        RunningAppList = new List<AppTitleItem_ViewModel>();
        InitializeSequenceManagers();
        InitializeConstantApps();
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private int _sequenceManagerNumber;

    private void InitializeSequenceManagers() {
        _sequenceManagerNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < _sequenceManagerNumber; i++) {
            AppTitleSequenceManager_ViewModel appTitleSequenceManager = new AppTitleSequenceManager_ViewModel(i);
            AppSequenceManagerCollection.Add(appTitleSequenceManager);
            appTitleSequenceManager.RegisterObserver(this);
            SelectedCollection.Add(null);
        }
    }

    private void InitializeConstantApps() {
        List<string> ConstantApplist = SystemConfiguration.GetInstance().GetConstantAppList();
        for (int i = 0; i < ConstantApplist.Count; i++) {
            VisibleAppModelList.Add(new AppTitleItem_ViewModel(new AppModel(ConstantApplist[i])));
        }
    }

    public ObservableCollection<AppTitleSequenceManager_ViewModel> AppSequenceManagerCollection;

    public ObservableCollection<AppTitleItem_ViewModel> VisibleAppModelList;

    private List<AppTitleItem_ViewModel> RunningAppList;

    private List<AppTitleSequenceItem> SelectedCollection;


    private void AddAdvancedAppViewModel(AppTitleItem_ViewModel appTitleItemModel) {
        appTitleItemModel.RegisterObserver(this);
        RunningAppList.Add(appTitleItemModel);
        VisibleAppModelListAddItem(appTitleItemModel);
        if (AppAlreadySelected(appTitleItemModel)) return;
        AppSequenceManagerCollection[0].SelectedAppTitleItem = appTitleItemModel;
    }

    public void RemoveAdvancedAppViewModel(AppTitleItem_ViewModel appTitleItemModel) {
        appTitleItemModel.DeregisterObserver(this);
        RunningAppList.Remove(appTitleItemModel);
        VisibleAppModelListRemoveItem(appTitleItemModel);
        SequenceManagersRemoveItem(appTitleItemModel);
    }

    private void VisibleAppModelListAddItem(AppTitleItem_ViewModel appTitleItemModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appTitleItemModel.AppName))  return;
        VisibleAppModelList.Add(appTitleItemModel);
    }

    private void VisibleAppModelListRemoveItem(AppTitleItem_ViewModel appTitleItemModel) {
        if (SystemConfiguration.GetInstance().IsConstantApp(appTitleItemModel.AppName))  return;
        VisibleAppModelList.Remove(appTitleItemModel);
    }

    private void SequenceMangersAddItem(AppTitleItem_ViewModel appTitleItemModel, int triggerIndex) {
        int endTriggerNumber = triggerIndex + appTitleItemModel.MaxScreenConfigNumber;
        // 如果屏幕右界面超出屏幕数量, 矫正到最后一个屏幕
        int maxNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        if (endTriggerNumber > maxNumber) {
            triggerIndex = maxNumber - appTitleItemModel.MaxScreenConfigNumber;
        }
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if (i == triggerIndex) {
                for (int j = 0; j < appTitleItemModel.MaxScreenConfigNumber; j++) {
                    AppSequenceManagerCollection[i+j].AddAppSequenceItem(new AppTitleSequenceItem(appTitleItemModel, j));
                }
                break;
            }
        }
    }

    private void SequenceManagersRemoveItem(AppTitleItem_ViewModel appTitleItemModel) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            AppSequenceManagerCollection[i].RemoveApp(appTitleItemModel);
        }
    }
    public void SequenceManagerAppSelected(AppTitleSequenceManager_ViewModel appTitleSequenceManager) {
        AppTitleItem_ViewModel appTitleItemViewModel = appTitleSequenceManager.SelectedAppTitleItem;
        int sequenceManagerIndex = appTitleSequenceManager.SequenceNumber;
        // 重排序, 全部移除再全部添加
        SequenceManagersRemoveItem(appTitleItemViewModel);
        SequenceMangersAddItem(appTitleItemViewModel, sequenceManagerIndex);
        if (!RunningAppList.Contains(appTitleSequenceManager.SelectedAppTitleItem)) {
            PublishEvent(nameof(SequenceManagerAppSelected), appTitleSequenceManager.SelectedAppTitleItem);
        }
        //广播调起应用
        PublishSelectionFinished();
    }

    private void SelectedCollectionChanged(AppTitleSequenceManager_ViewModel appTitleSequenceManagerViewModel) {
        int seqIndex = appTitleSequenceManagerViewModel.SequenceNumber;
        if (seqIndex < 0 || seqIndex > SelectedCollection.Count) {
            throw new IndexOutOfRangeException();
            return;
        }

        SelectedCollection[seqIndex] = appTitleSequenceManagerViewModel.GetPeekAppSeqItem();
    }

    public void PublishSelectionFinished() {
        PublishEvent(nameof(PublishSelectionFinished), SelectedCollection);
    }

    private bool AppAlreadySelected(AppTitleItem_ViewModel appTitleItemViewModel) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if(AppSequenceManagerCollection[i].GetPeekAppSeqItem() == null) continue;
            if (AppSequenceManagerCollection[i].GetPeekAppSeqItem().AppTitleItemViewModel.Equals(appTitleItemViewModel)) {
                return true;
            }
        }

        return false;
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyAppMappingObj.AddAppModel))){
            AppModel appModel = (AppModel)o;
            AppTitleItem_ViewModel appTitleItemViewModel = new AppTitleItem_ViewModel(appModel);
            AddAdvancedAppViewModel(appTitleItemViewModel);
        }

        if (propertyName.Equals(nameof(StudyAppMappingObj.RemoveAppModel))) {
            AppModel appModel = (AppModel)o;
            AppTitleItem_ViewModel appTitleItemViewModel = new AppTitleItem_ViewModel(appModel);
            RemoveAdvancedAppViewModel(appTitleItemViewModel);
        }

        if (propertyName.Equals(nameof(AppTitleSequenceManager_ViewModel.SelectedAppTitleItem))) {
            AppTitleSequenceManager_ViewModel appTitleSequenceManager = (AppTitleSequenceManager_ViewModel)o;
            SequenceManagerAppSelected(appTitleSequenceManager);
        }

        if (propertyName.Equals(nameof(AppTitleSequenceManager_ViewModel.PeekNodeChanged))) {
            AppTitleSequenceManager_ViewModel appTitleSequenceManager = (AppTitleSequenceManager_ViewModel)o;
            SelectedCollectionChanged(appTitleSequenceManager);
        }
    }
}