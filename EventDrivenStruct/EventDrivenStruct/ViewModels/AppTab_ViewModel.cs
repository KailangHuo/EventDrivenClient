using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppContainerManager_ViewModel>();
    }

    private Dictionary<StudyCollectionItem, AppContainerManager_ViewModel> _map;

    private AppContainerManager_ViewModel _selectedAppContainerManager;
    
    public AppContainerManager_ViewModel SelectedAppContainerManager {
        get {
            return _selectedAppContainerManager;
        }
        set {
            if(_selectedAppContainerManager == value) return;
            _selectedAppContainerManager = value;
            RisePropertyChanged(nameof(SelectedAppContainerManager));
        }
    }
    
    
    private void PutInMap(StudyAppMappingObj studyAppMappingObj) {
        AppContainerManager_ViewModel appContainerManager = new AppContainerManager_ViewModel(studyAppMappingObj);
        studyAppMappingObj.RegisterObserver(appContainerManager);
        _map.Add(studyAppMappingObj.StudyCollectionItem, appContainerManager);
    }

    private void RemoveFromMap(StudyAppMappingObj studyAppMappingObj) {
        _map.Remove(studyAppMappingObj.StudyCollectionItem);
    }

    private void SwapSelectedAppContainer(StudyCollectionItem? studyCollectionItem) {
        SelectedAppContainerManager = studyCollectionItem == null ? null : _map[studyCollectionItem];
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
    }
}