

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
        ExamContainerViewModel = new ExamContainer_ViewModel();
        HostingWindowViewModel = new HostingWindow_ViewModel();

        RegisterObserver(PopupManager.GetInstance());
        SetupCommands();
    }

    private void SetupCommands() {
        GotoPaViewCommand = new CommonCommand(GotoPaView);
        AddExamCommand = new CommonCommand(AddExam);
    }

    #endregion

    #region NOTIFIABLE_VIEW_MODELS

    private ExamContainer_ViewModel _examContainerViewModel;

    public ExamContainer_ViewModel ExamContainerViewModel {
        get {
            return this._examContainerViewModel;
        }
        set {
            if(this._examContainerViewModel == value) return;
            this._examContainerViewModel = value;
            PublishEvent(nameof(ExamContainerViewModel), this._examContainerViewModel);
            RisePropertyChanged(nameof(ExamContainerViewModel));
        }
    }

    private ApplicationContainer_ViewModel _applicationContainerViewModel;

    public ApplicationContainer_ViewModel ApplicationContainerViewModel {
        get {
            return this._applicationContainerViewModel;
        }
        set {
            if(this._applicationContainerViewModel == value) return;
            this._applicationContainerViewModel = value;
            PublishEvent(nameof(ApplicationContainerViewModel), this._applicationContainerViewModel);
            RisePropertyChanged(nameof(ApplicationContainerViewModel));
        }
    }

    private HostingWindow_ViewModel _hostingWindowViewModel;

    public HostingWindow_ViewModel HostingWindowViewModel {
        get {
            return this._hostingWindowViewModel;
        }
        set {
            if(_hostingWindowViewModel == value) return;
            _hostingWindowViewModel = value;
            PublishEvent(nameof(HostingWindowViewModel), this._hostingWindowViewModel);
            RisePropertyChanged(nameof(HostingWindowViewModel));
        }
    }

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
        if (propertyName.Equals(nameof(MainEntry_ModelFacade.ExamContainer))) {
            ExamContainer examContainer = (ExamContainer)o;
            examContainer.RegisterObserver(ExamContainerViewModel);
        }
    }
}