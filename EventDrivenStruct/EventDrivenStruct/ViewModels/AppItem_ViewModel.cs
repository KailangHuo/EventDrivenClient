using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppItem_ViewModel : AbstractEventDrivenViewModel{

    public AppItem_ViewModel(AppModel appModel) : base(appModel){
        this._appModel = appModel;
        this.AppName = appModel.AppName;
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
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

    public int MaxScreenConfigNumber { get; private set; } 

    public override string ToString() {
        return this.AppName;
    }
}