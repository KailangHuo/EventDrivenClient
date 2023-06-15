using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTab_ViewModel() {
        _map = new Dictionary<StudyCollectionItem, AppContainer_ViewModel>();
        CurrentSelectedStudyCollectionItem = null;
    }

    private Dictionary<StudyCollectionItem, AppContainer_ViewModel> _map;

    private StudyCollectionItem CurrentSelectedStudyCollectionItem;

    private AppContainer_ViewModel _selectedAppContainer;
    
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
    
    
    private void PutInMap(StudyAppMappingObj studyAppMappingObj) {
        AppContainer_ViewModel appContainer = new AppContainer_ViewModel(studyAppMappingObj);
        studyAppMappingObj.RegisterObserver(appContainer);
        _map.Add(studyAppMappingObj.StudyCollectionItem, appContainer);
    }

    private void RemoveFromMap(StudyAppMappingObj studyAppMappingObj) {
        _map.Remove(studyAppMappingObj.StudyCollectionItem);
    }

    private void SwapSelectedAppContainer(StudyCollectionItem? studyCollectionItem) {
        SelectedAppContainer = studyCollectionItem == null ? null : _map[studyCollectionItem];
        CurrentSelectedStudyCollectionItem = studyCollectionItem;
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
            AdvancedApp_ViewModel appViewModel = (AdvancedApp_ViewModel)o;
            MainEntry_ModelFacade.GetInstance().AddStudyItemWithApp(CurrentSelectedStudyCollectionItem, (AppModel)appViewModel.HashReferenceContext);
        }
    }
}