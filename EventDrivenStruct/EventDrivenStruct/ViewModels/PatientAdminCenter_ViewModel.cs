using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class PatientAdminCenter_ViewModel : AbstractEventDrivenViewModel{

    public PatientAdminCenter_ViewModel() {
        PaContent = "";
    }
    
    private string _paContent;

    public string PaContent {
        get {
            return _paContent;
        }
        set {
            if(_paContent == value) return;
            _paContent = value;
            PublishEvent(nameof(PaContent), _paContent);
            RisePropertyChanged(nameof(PaContent));
        }
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(PatientAdminCenter.Content))) {
            string str = (string)o;
            PaContent = str;
        }
    }
}