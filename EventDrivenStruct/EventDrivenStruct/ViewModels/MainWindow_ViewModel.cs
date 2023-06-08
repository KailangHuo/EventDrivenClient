

using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class MainWindow_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public MainWindow_ViewModel() {
        StudyContainerViewModel = new StudyContainer_ViewModel();
        AppTabViewModel = new AppTab_ViewModel();
        StudyContainerViewModel.RegisterObserver(AppTabViewModel);
        
        RegisterObserver(PopupManager.GetInstance());
        SetupCommands();
    }

    private void SetupCommands() {
        GotoPaViewCommand = new CommonCommand(GotoPaView);
        AddExamCommand = new CommonCommand(AddExam);
    }

    #endregion

    #region NOTIFIABLE_VIEW_MODELS

    public StudyContainer_ViewModel StudyContainerViewModel { get; private set; }

    public AppTab_ViewModel AppTabViewModel { get; private set; }


    #endregion

    #region COMMANDS

    public ICommand GotoPaViewCommand { get; private set; }

    public ICommand AddExamCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void GotoPaView(object o = null) {
        PublishEvent(nameof(GotoPaView), o);
    }

    public void AddExam(object o = null) {
        PublishEvent(nameof(AddExam), o);
    } 

    #endregion

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(MainEntry_ModelFacade.StudyCollection))) {
            StudyCollection studyCollection = (StudyCollection)o;
            studyCollection.RegisterObserver(StudyContainerViewModel);
        }

        if (propertyName.Equals(nameof(MainEntry_ModelFacade.StudyAppMappingManager))) {
            StudyAppMappingManager studyAppMappingManager = (StudyAppMappingManager)o;
            studyAppMappingManager.RegisterObserver(AppTabViewModel);
        }
    }
}