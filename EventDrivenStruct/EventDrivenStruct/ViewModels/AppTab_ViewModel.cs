using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppItemContainer_ViewModel>();
        IsExpanded = false;
        SelectedAppItemContainer = null;
        CurrentSelectedStudyCollectionItem = null;
    }

    private Dictionary<StudyCollectionItem, AppItemContainer_ViewModel> _map;

    private AppItemContainer_ViewModel _selectedAppItemContainer;

    #region NOTIFIABLE_PROPERTIES

    public AppItemContainer_ViewModel SelectedAppItemContainer {
        get {
            return _selectedAppItemContainer;
        }
        set {
            if(_selectedAppItemContainer == value) return;
            _selectedAppItemContainer = value;
            IsExpanded = !(_selectedAppItemContainer == null);
            PublishEvent(nameof(SelectedAppItemContainer), _selectedAppItemContainer);
            RisePropertyChanged(nameof(SelectedAppItemContainer));
        }
    }

    private bool _isExpanded;

    public bool IsExpanded {
        get {
            return _isExpanded;
        }
        set {
            if(_isExpanded == value) return;
            _isExpanded = value;
            RisePropertyChanged(nameof(IsExpanded));
            if (_isExpanded) PublishEvent(nameof(SelectedAppItemContainer), _selectedAppItemContainer);
            PublishEvent(nameof(IsExpanded), _isExpanded);
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

    #endregion

    #region COMMANDS



    #endregion

    #region COMMAND_BINDING_METHODS



    #endregion

    private void PutInMap(StudyAppMappingObj studyAppMappingObj) {
        AppItemContainer_ViewModel appItemContainer = new AppItemContainer_ViewModel(studyAppMappingObj);
        studyAppMappingObj.RegisterObserver(appItemContainer);
        appItemContainer.RegisterObserver(this);
        _map.Add(studyAppMappingObj.StudyCollectionItem, appItemContainer);
    }

    private void RemoveFromMap(StudyAppMappingObj studyAppMappingObj) {
        _map.Remove(studyAppMappingObj.StudyCollectionItem);
    }

    private void RemoveAllFromMap() {
        _map = new Dictionary<StudyCollectionItem, AppItemContainer_ViewModel>();
    }

    private void SwapSelectedAppContainer(StudyCollectionItem? studyCollectionItem) {
        if (studyCollectionItem == null) {
            SelectedAppItemContainer = null;
        }
        else {
            SelectedAppItemContainer = _map[studyCollectionItem];
        }

        CurrentSelectedStudyCollectionItem = studyCollectionItem;
    }
    
    public void SelectedAppContainerSelectionChanged() {
        if (IsExpanded) {
            PublishEvent(nameof(SelectedAppContainerSelectionChanged), SelectedAppItemContainer);
        }
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

        if (propertyName.Equals(nameof(StudyContainer_ViewModel.TriggerSelected))) {
            IsExpanded = true;
        }

        if (propertyName.Equals(nameof(AppItemContainer_ViewModel.SelectionFinished))) {
            SelectedAppContainerSelectionChanged();
            //AppConSeqItemsSelectedChanged(list);
        }

        if (propertyName.Equals(nameof(StudyAppMappingManager.RemoveAllStudyAppObj))) {
            RemoveAllFromMap();
        }

        if (propertyName.Equals(nameof(StudyContainer_ViewModel.RemoveStudyBroadCast))) {
            IsExpanded = false;
        }

    }
}