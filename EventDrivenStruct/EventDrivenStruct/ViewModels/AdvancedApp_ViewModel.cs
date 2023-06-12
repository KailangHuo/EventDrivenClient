using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AdvancedApp_ViewModel : AbstractEventDrivenViewModel{

    public AdvancedApp_ViewModel(AppModel appModel) : base(appModel){
        this._appModel = appModel;
        this.AppName = appModel.AppName;
    }

    private AppModel _appModel;

    private string _appName;

    public string AppName {
        get {
            return _appName;
        }
        set {
            if(_appName == (value))return;
            _appName = value;
            RisePropertyChanged(nameof(AppName));
        }
    }

    public override string ToString() {
        return this.AppName;
    }
}