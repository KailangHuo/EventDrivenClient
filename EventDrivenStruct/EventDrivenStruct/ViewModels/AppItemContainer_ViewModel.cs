using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppItemContainer_ViewModel : AbstractEventDrivenViewModel {

    public AppItemContainer_ViewModel(StudyAppMappingObj mappingObj) {
        AppSequenceManagerCollection = new ObservableCollection<AppSequenceManager_ViewModel>();
        SelectedSequenceApps = new List<AppSequenceItem>();
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
            AppSequenceManager_ViewModel appSequenceManager = new AppSequenceManager_ViewModel(i);
            AppSequenceManagerCollection.Add(appSequenceManager);
            appSequenceManager.RegisterObserver(this);
            SelectedSequenceApps.Add(null);
        }
    }

    private void InitializeConstantApps() {
        List<string> ConstantApplist = SystemConfiguration.GetInstance().GetConstantAppList();
        for (int i = 0; i < ConstantApplist.Count; i++) {
            VisibleAppModelList.Add(new AppItem_ViewModel(new AppModel(ConstantApplist[i], StudyAppMappingObj.StudyCollectionItem)));
        }
    }

    public ObservableCollection<AppSequenceManager_ViewModel> AppSequenceManagerCollection { get; private set; }

    public ObservableCollection<AppItem_ViewModel> VisibleAppModelList { get; private set; }

    private List<AppItem_ViewModel> RunningAppList;

    private List<AppSequenceItem> _selectedSequenceApps;

    public List<AppSequenceItem> SelectedSequenceApps {
        get {
            return _selectedSequenceApps;
        }
        set {
            if(_selectedSequenceApps == value) return;
            _selectedSequenceApps = value;
        }
    }


    private void AddAdvancedAppViewModel(AppItem_ViewModel appItemModel) {
        appItemModel.RegisterObserver(this);
        RunningAppList.Add(appItemModel);
        VisibleAppModelListAddItem(appItemModel);
        if (AppAlreadySelected(appItemModel)) return;
        AppSequenceManagerCollection[0].AppItemSelected = appItemModel;
    }

    public void RemoveAdvancedAppViewModel(AppItem_ViewModel appItemModel) {
        appItemModel.DeregisterObserver(this);
        RunningAppList.Remove(appItemModel);
        VisibleAppModelListRemoveItem(appItemModel);
        SequenceManagersRemoveItem(appItemModel);
        SelectionFinished();
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
                    AppSequenceManagerCollection[i+j].AddAppSequenceItem(new AppSequenceItem(appItemModel, j));
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
    public void AppSeqSelected(AppSequenceManager_ViewModel appSequenceManager) {
        AppItem_ViewModel appItemViewModel = appSequenceManager.AppItemSelected;
        int sequenceManagerIndex = appSequenceManager.SequenceNumber;
        // 重排序, 全部移除再全部添加
        SequenceManagersRemoveItem(appItemViewModel);
        SequenceMangersAddItem(appItemViewModel, sequenceManagerIndex);
        if (!RunningAppList.Contains(appSequenceManager.AppItemSelected)) {
            PublishEvent(nameof(AppSeqSelected), appSequenceManager.AppItemSelected);
        }
        //选择完成
        SelectionFinished();
    }

    private void SelectedCollectionChanged(AppSequenceManager_ViewModel appSequenceManagerViewModel) {
        int seqIndex = appSequenceManagerViewModel.SequenceNumber;
        if (seqIndex < 0 || seqIndex > SelectedSequenceApps.Count) {
            throw new IndexOutOfRangeException();
            return;
        }

        SelectedSequenceApps[seqIndex] = appSequenceManagerViewModel.GetPeekAppSeqItem();
    }

    public void SelectionFinished() {
        PublishEvent(nameof(SelectionFinished), SelectedSequenceApps);
    }

    private bool AppAlreadySelected(AppItem_ViewModel appItemViewModel) {
        for (int i = 0; i < AppSequenceManagerCollection.Count; i++) {
            if(AppSequenceManagerCollection[i].GetPeekAppSeqItem() == null) continue;
            if (AppSequenceManagerCollection[i].GetPeekAppSeqItem().AppItemViewModel.Equals(appItemViewModel)) {
                return true;
            }
        }

        return false;
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

        if (propertyName.Equals(nameof(AppSequenceManager_ViewModel.AppItemSelected))) {
            AppSequenceManager_ViewModel appSequenceManager = (AppSequenceManager_ViewModel)o;
            AppSeqSelected(appSequenceManager);
        }

        if (propertyName.Equals(nameof(AppSequenceManager_ViewModel.PeekNodeChanged))) {
            AppSequenceManager_ViewModel appSequenceManager = (AppSequenceManager_ViewModel)o;
            SelectedCollectionChanged(appSequenceManager);
        }
    }
}