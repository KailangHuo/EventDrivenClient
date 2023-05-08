

using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class MainWindow_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public MainWindow_ViewModel() {
        ApplicationContainerViewModel = new ApplicationContainer_ViewModel();
        ApplicationContainer.GetInstance().RegisterObserver(ApplicationContainerViewModel);
        
        ExamContainerViewModel = new ExamContainer_ViewModel();
        ExamContainer.GetInstance().RegisterObserver(ExamContainerViewModel);
        
        HostingWindowViewModel = new HostingWindow_ViewModel();
        HostingWindowContent.GetInstance().RegisterObserver(HostingWindowViewModel);
        
        HostingWindowContent.GetInstance().RegisterObserver(ApplicationContainer.GetInstance());
        ApplicationContainer.GetInstance().RegisterObserver(ExamContainer.GetInstance());
        
        RegisterObserver(PopupManager.GetInstance());
        SetupCommands();
    }

    private void SetupCommands() {
        GotoPaViewCommand = new CommonCommand(GotoPaView);
        AddExamCommand = new CommonCommand(AddExam);
    }

    #endregion

    #region NOTIFY_VIEW_MODELS

    public ExamContainer_ViewModel ExamContainerViewModel { get; set; }

    public ApplicationContainer_ViewModel ApplicationContainerViewModel { get; set; }

    public HostingWindow_ViewModel HostingWindowViewModel { get; set; }

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
    
    
}