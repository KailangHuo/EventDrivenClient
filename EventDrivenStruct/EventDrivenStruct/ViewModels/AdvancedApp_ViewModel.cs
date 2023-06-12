using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AdvancedApp_ViewModel : AbstractEventDrivenViewModel{

    public AdvancedApp_ViewModel(AppModel appModel) {
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
    
    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AdvancedApp_ViewModel t = (AdvancedApp_ViewModel)obj;
        return this._appModel == t._appModel;
    }

    public override int GetHashCode() {
        return this._appModel.GetHashCode();
    }

    #endregion

    public override string ToString() {
        return this.AppName;
    }
}