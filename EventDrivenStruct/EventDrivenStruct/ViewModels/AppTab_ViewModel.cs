using System.Collections.Generic;
using System.Collections.Specialized;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppItemContainer_ViewModel>();
        CurrentSelectedStudyCollectionItem = null;
    }

    private Dictionary<StudyCollectionItem, AppItemContainer_ViewModel> _map;

    private AppItemContainer_ViewModel _selectedAppItemContainer;

    //TEST ONLY
    private int times;
    
    public AppItemContainer_ViewModel SelectedAppItemContainer {
        get {
            return _selectedAppItemContainer;
        }
        set {
            if(_selectedAppItemContainer == value) return;
            _selectedAppItemContainer = value;
            PublishEvent(nameof(SelectedAppItemContainer), _selectedAppItemContainer);
            RisePropertyChanged(nameof(SelectedAppItemContainer));
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
        AppItemContainer_ViewModel appItemContainer = new AppItemContainer_ViewModel(studyAppMappingObj);
        studyAppMappingObj.RegisterObserver(appItemContainer);
        appItemContainer.RegisterObserver(this);
        _map.Add(studyAppMappingObj.StudyCollectionItem, appItemContainer);
    }

    private void RemoveFromMap(StudyAppMappingObj studyAppMappingObj) {
        _map.Remove(studyAppMappingObj.StudyCollectionItem);
    }

    private void SwapSelectedAppContainer(StudyCollectionItem? studyCollectionItem) {
        if (studyCollectionItem == null) {
            SelectedAppItemContainer = null;
        }
        else {
            SelectedAppItemContainer = _map[studyCollectionItem];
        }

        SelectedAppItemContainer = studyCollectionItem == null ? null : _map[studyCollectionItem];
        CurrentSelectedStudyCollectionItem = studyCollectionItem;
    }

    public void AppConSeqItemsSelectedChanged(List<AppSequenceItem> appSequenceItems) {
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

        if (propertyName.Equals(nameof(AppItemContainer_ViewModel.SequenceManagerAppSelected))) {
            AppItem_ViewModel appItemViewModel = (AppItem_ViewModel)o;
            //执行添加
            MainEntry_ModelFacade.GetInstance().AddStudyItemWithApp(CurrentSelectedStudyCollectionItem, (AppModel)appItemViewModel.HashReferenceContext);
        }

        if (propertyName.Equals(nameof(AppItemContainer_ViewModel.PublishSelectionFinished))) {
            List<AppSequenceItem> list = (List<AppSequenceItem>)o;
            AppConSeqItemsSelectedChanged(list);
        }
    }
}