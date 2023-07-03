using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.ViewModels; 

public class ScreenContentContainer_ViewModel : AbstractEventDrivenViewModel{

    public ScreenContentContainer_ViewModel() {
        InitSco();
    }

    public List<ScreenContentObject_ViewModel> ScoList;

    public void InitSco() {
        ScoList = new List<ScreenContentObject_ViewModel>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            ScoList.Add(new ScreenContentObject_ViewModel());
        }
    }

}