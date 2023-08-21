using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class PatientAdminCenterApp_ViewModel : AppItem_ViewModel{

    public PatientAdminCenterApp_ViewModel(PatientAdminCenterApp patientAdminCenterApp) : base(patientAdminCenterApp) {
        this.PatientAdminCenterApp = patientAdminCenterApp;
        this.Content = PatientAdminCenterApp.Content;
    }

    public PatientAdminCenterApp PatientAdminCenterApp;



    private string _content;

    public string Content {
        get {
            return _content;
        }
        set {
            if(_content == value)return;
            _content = value;
            PublishEvent(nameof(Content), _content);
            RisePropertyChanged(nameof(Content));
        }
    }

}