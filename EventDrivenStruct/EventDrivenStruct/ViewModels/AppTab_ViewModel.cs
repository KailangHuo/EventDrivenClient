using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTab_ViewModel : AbstractEventDrivenViewModel{

    public AppTab_ViewModel() {
        AppContainerViewModel = new AppContainer_ViewModel();
    }

    public AppContainer_ViewModel AppContainerViewModel { get; private set; }

    private void UpdateAppContainer(Study_ViewModel studyViewModel) {
        StudyAppMappingObj obj = MainEntry_ModelFacade.GetInstance().GetMappingObjByStudy(studyViewModel._studyCollectionItem);
        
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyContainer_ViewModel.SelectedStudy))) {
            Study_ViewModel studyViewModel = (Study_ViewModel)o;
            
        }
    }
}