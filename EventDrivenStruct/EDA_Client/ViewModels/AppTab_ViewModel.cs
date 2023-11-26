using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{
    
    #region CONSTRUCTION

    public AppTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppItemContainer_ViewModel>();
        IsExpanded = false;
        SelectedAppItemContainer = null;
        CurrentSelectedStudyCollectionItem = null;
        this.RegisterObserver(ExceptionManager.GetInstance());
        SetupCommands();
    }

    private void SetupCommands() {
        this.AddAppCommand = new CommonCommand(AddApp);
    }

    #endregion

    #region NOTIFIABLE_PROPERTIES

    public AppItemContainer_ViewModel SelectedAppItemContainer {
        get {
            return _selectedAppItemContainer;
        }
        set {
            if(_selectedAppItemContainer == value) return;
            _selectedAppItemContainer = value;
            IsExpanded = (_selectedAppItemContainer != null 
                          && _selectedAppItemContainer.HasRunningApp
                          && IsExpanded);
            PublishSelectedAppContainerChanged();
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
            if (_isExpanded) PublishEvent(nameof(SelectedAppItemContainer), _selectedAppItemContainer);
            PublishEvent(nameof(IsExpanded), _isExpanded);
            RisePropertyChanged(nameof(IsExpanded));
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

    private void PublishSelectedAppContainerChanged() {
        if (IsExpanded) {
            PublishEvent(nameof(SelectedAppItemContainer), _selectedAppItemContainer);
        }
    }

    public void SelectedAppContainerAppSelectionChanged() {
        if (IsExpanded) {
            PublishEvent(nameof(SelectedAppContainerAppSelectionChanged), SelectedAppItemContainer);
        }
    }

    #endregion
    
    #region PROPERTIES

    
    private Dictionary<StudyCollectionItem, AppItemContainer_ViewModel> _map;

    private AppItemContainer_ViewModel _selectedAppItemContainer;

    #endregion

    #region COMMANDS
    public ICommand AddAppCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void AddApp(object o = null) {
        int screenNumber = (int)o;
        PopupManager.GetInstance().MainWindow_AddAppWindowPopup(this, screenNumber);
    }

    #endregion

    #region METHODS

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

    #endregion

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyContainer_ViewModel.SelectedStudy))) {
            Study_ViewModel studyViewModel = (Study_ViewModel)o;
            SwapSelectedAppContainer(studyViewModel?.StudyCollectionItem);
            return;
        }

        if (propertyName.Equals(nameof(StudyAppMappingManager.PutStudyAppMapObj))) {
            StudyAppMappingObj studyAppMappingObj = (StudyAppMappingObj)o;
            PutInMap(studyAppMappingObj);
            return;
        }

        if (propertyName.Equals(nameof(StudyAppMappingManager.RemoveStudyAppMapObj))) {
            StudyAppMappingObj studyAppMappingObj = (StudyAppMappingObj)o;
            this.RemoveFromMap(studyAppMappingObj);
            return;
        }

        if (propertyName.Equals(nameof(StudyContainer_ViewModel.TriggerSelected))) {
            IsExpanded = true;
            return;
        }

        if (propertyName.Equals(nameof(AppItemContainer_ViewModel.SelectionFinished))) {
            SelectedAppContainerAppSelectionChanged();
            return;
        }

        if (propertyName.Equals(nameof(AppItemContainer_ViewModel.AppSeqSelected))) {
            AppModel appModel = (AppModel)o;
            MainEntry_ModelFacade.GetInstance().AddAppToStudy(_currentSelectedStudyCollectionItem, appModel);
            return;
        }

        if (propertyName.Equals(nameof(StudyAppMappingManager.RemoveAllStudyAppObj))) {
            RemoveAllFromMap();
            return;
        }

        if (propertyName.Equals(nameof(StudyContainer_ViewModel.RemoveStudyBroadCast))) {
            IsExpanded = false;
            return;
        }

    }
}