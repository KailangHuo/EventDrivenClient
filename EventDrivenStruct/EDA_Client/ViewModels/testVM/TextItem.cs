using EventDrivenElements;

namespace EventDrivenStruct.ViewModels.testVM; 

public class TextItem : AbstractEventDrivenViewModel{

    public TextItem(string str) {
        this.AppName = str;
        this.AppText = str + " AppText";
    }

    private string _appName;
    public string AppName {
        get { return _appName;}
        set {
            if(_appName == value)return;
            _appName = value;
            RisePropertyChanged(nameof(AppName));
        }
    }

    private string _appText;
    public string AppText {
        get { return _appText;}
        set {
            if(_appText ==value) return;
            _appText = value;
            RisePropertyChanged(nameof(AppText));
        }
    }

}