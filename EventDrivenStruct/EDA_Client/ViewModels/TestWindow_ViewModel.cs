using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class TestWindow_ViewModel : AbstractEventDrivenViewModel{

    public TestWindow_ViewModel() {
        this.StudyCollectionItem = SystemConfiguration.GetInstance().GetTestStudyList()[0].ConvertToAppModel().StudyCollectionItem;
        this.StudyViewModel = new Study_ViewModel(this.StudyCollectionItem);
    }

    public Study_ViewModel StudyViewModel { get; private set; }
    public StudyCollectionItem StudyCollectionItem { get; private set; }

}