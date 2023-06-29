using System.Collections.Generic;
using System.Collections.Specialized;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppContainer_ViewModel>();
        CurrentSelectedStudyCollectionItem = null;
    }

    private Dictionary<StudyCollectionItem, AppContainer_ViewModel> _map;

    private AppContainer_ViewModel _selectedAppContainer;

    //TEST ONLY
    private int times;
    
    public AppContainer_ViewModel SelectedAppContainer {
        get {
            return _selectedAppContainer;
        }
        set {
            if(_selectedAppContainer == value) return;
            _selectedAppContainer = value;
            PublishEvent(nameof(SelectedAppContainer), _selectedAppContainer);
            RisePropertyChanged(nameof(SelectedAppContainer));
        }
    }
    
    private StudyCollectionItem _currentSelectedStudyCollectionItem;

    public StudyCollectionItem CurrentSelectedStudyCollectionItem {
        get {
            return _currentSelectedStudyCollectionItem;
        }
        set {
            if(_currentSelectedStudyCollectionItem == value) return;
            _currentSelectedStudyCollectionItem = value;
            RisePropertyChanged(nameof(CurrentSelectedStudyCollectionItem));
        }
    }


    private void PutInMap(StudyAppMappingObj studyAppMappingObj) {
        AppContainer_ViewModel appContainer = new AppContainer_ViewModel(studyAppMappingObj);
        studyAppMappingObj.RegisterObserver(appContainer);
        appContainer.RegisterObserver(this);
        _map.Add(studyAppMappingObj.StudyCollectionItem, appContainer);
    }

    private void RemoveFromMap(StudyAppMappingObj studyAppMappingObj) {
        _map.Remove(studyAppMappingObj.StudyCollectionItem);
    }

    private void SwapSelectedAppContainer(StudyCollectionItem? studyCollectionItem) {
        if (studyCollectionItem == null) {
            SelectedAppContainer = null;
        }
        else {
            SelectedAppContainer = _map[studyCollectionItem];
        }

        SelectedAppContainer = studyCollectionItem == null ? null : _map[studyCollectionItem];
        CurrentSelectedStudyCollectionItem = studyCollectionItem;
    }

    private void NotifyOpenApp(AppItem_ViewModel appItemViewModel, int screenIndex) {
        TCP_Sender.GetInstance().SendOpen(CurrentSelectedStudyCollectionItem.GetStudyUidComposition(), appItemViewModel.AppName, screenIndex, times);
    }

    private void NotifyHideApp(AppItem_ViewModel appItemViewModel) {
        TCP_Sender.GetInstance().SendHide(CurrentSelectedStudyCollectionItem.GetStudyUidComposition(), appItemViewModel.AppName, times);
    }

    private void NotifyCloseApp(AppItem_ViewModel appItemViewModel) {
        TCP_Sender.GetInstance().SendClose(CurrentSelectedStudyCollectionItem.GetStudyUidComposition(), appItemViewModel.AppName,  times);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyContainer_ViewModel.SelectedStudy))) {
            Study_ViewModel studyViewModel = (Study_ViewModel)o;
            SwapSelectedAppContainer(studyViewModel?.StudyCollectionItem);
        }

        if (propertyName.Equals(nameof(StudyAppMappingManager.PutStudyAppMapObj))) {
            StudyAppMappingObj studyAppMappingObj = (StudyAppMappingObj)o;
            PutInMap(studyAppMappingObj);
        }

        if (propertyName.Equals(nameof(StudyAppMappingManager.RemoveStudyAppMapObj))) {
            StudyAppMappingObj studyAppMappingObj = (StudyAppMappingObj)o;
            this.RemoveFromMap(studyAppMappingObj);
        }

        if (propertyName.Equals(nameof(AppContainer_ViewModel.SequenceManagerAppSelected))) {
            AppItem_ViewModel appItemViewModel = (AppItem_ViewModel)o;
            //执行添加
            MainEntry_ModelFacade.GetInstance().AddStudyItemWithApp(CurrentSelectedStudyCollectionItem, (AppModel)appItemViewModel.HashReferenceContext);
        }

        if (propertyName.Equals(nameof(AppContainer_ViewModel.PublishSelectionFinished))) {
            List<AppItem_ViewModel> appList = (List<AppItem_ViewModel>)o;
            //NotifyOpenApp(appItemViewModel, index);
            times++;
        }
    }
}