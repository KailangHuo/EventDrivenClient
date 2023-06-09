using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainerManager_ViewModel : AbstractEventDrivenViewModel {

    public AppContainerManager_ViewModel(StudyAppMappingObj mappingObj) {
        AppContainerList = new ObservableCollection<AppContainer_ViewModel>();
        StudyAppMappingObj = mappingObj;
        InitializeContainerNumber();
    }

    public StudyAppMappingObj StudyAppMappingObj;

    private int _containerNumber;

    private void InitializeContainerNumber() {
        _containerNumber = 1;
        for (int i = 0; i < _containerNumber; i++) {
            AppContainerList.Add(new AppContainer_ViewModel(StudyAppMappingObj));
        }
    }

    public ObservableCollection<AppContainer_ViewModel> AppContainerList;
    
    

}