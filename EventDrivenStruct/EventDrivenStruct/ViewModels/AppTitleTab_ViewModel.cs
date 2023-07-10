using System.Collections.Generic;
using System.Collections.Specialized;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTitleTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTitleTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppTitleItemContainer_ViewModel>();
        CurrentSelectedStudyCollectionItem = null;
    }

    private Dictionary<StudyCollectionItem, AppTitleItemContainer_ViewModel> _map;

    private AppTitleItemContainer_ViewModel _selectedAppTitleItemContainer;

    //TEST ONLY
    private int times;
    
    public AppTitleItemContainer_ViewModel SelectedAppTitleItemContainer {
        get {
            return _selectedAppTitleItemContainer;
        }
        set {
            if(_selectedAppTitleItemContainer == value) return;
            _selectedAppTitleItemContainer = value;
            PublishEvent(nameof(SelectedAppTitleItemContainer), _selectedAppTitleItemContainer);
            RisePropertyChanged(nameof(SelectedAppTitleItemContainer));
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
        AppTitleItemContainer_ViewModel appTitleItemContainer = new AppTitleItemContainer_ViewModel(studyAppMappingObj);
        studyAppMappingObj.RegisterObserver(appTitleItemContainer);
        appTitleItemContainer.RegisterObserver(this);
        _map.Add(studyAppMappingObj.StudyCollectionItem, appTitleItemContainer);
    }

    private void RemoveFromMap(StudyAppMappingObj studyAppMappingObj) {
        _map.Remove(studyAppMappingObj.StudyCollectionItem);
    }

    private void SwapSelectedAppContainer(StudyCollectionItem? studyCollectionItem) {
        if (studyCollectionItem == null) {
            SelectedAppTitleItemContainer = null;
        }
        else {
            SelectedAppTitleItemContainer = _map[studyCollectionItem];
        }

        SelectedAppTitleItemContainer = studyCollectionItem == null ? null : _map[studyCollectionItem];
        CurrentSelectedStudyCollectionItem = studyCollectionItem;
    }

    public void AppConSeqItemsSelectedChanged(List<AppTitleSequenceItem> appSequenceItems) {
        List<ScreenContentObject> screenContentObjects = new List<ScreenContentObject>();
        for (int i = 0; i < appSequenceItems.Count; i++) {
            screenContentObjects.Add(new ScreenContentObject(CurrentSelectedStudyCollectionItem, appSequenceItems[i]));
        }
        
        PublishEvent(nameof(AppConSeqItemsSelectedChanged), screenContentObjects);
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

        if (propertyName.Equals(nameof(AppTitleItemContainer_ViewModel.SequenceManagerAppSelected))) {
            AppTitleItem_ViewModel appTitleItemViewModel = (AppTitleItem_ViewModel)o;
            //执行添加
            MainEntry_ModelFacade.GetInstance().AddStudyItemWithApp(CurrentSelectedStudyCollectionItem, (AppModel)appTitleItemViewModel.HashReferenceContext);
        }

        if (propertyName.Equals(nameof(AppTitleItemContainer_ViewModel.PublishSelectionFinished))) {
            List<AppTitleSequenceItem> list = (List<AppTitleSequenceItem>)o;
            AppConSeqItemsSelectedChanged(list);
        }
    }
}