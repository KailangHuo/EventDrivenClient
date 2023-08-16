using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class PatientAdminCenter : AppModel{

    public PatientAdminCenter(string name = "PatientAdministration") : base(name) {
        Content = "[PA Content]";
    }

    private string _content;

    public string Content {
        get {
            return _content;
        }
        set {
            if(_content == value) return;
            _content = value;
            PublishEvent(nameof(Content), _content);
        }
    }

    public void Invoke() {
        
        PublishEvent(nameof(Invoke), this);
    }

}