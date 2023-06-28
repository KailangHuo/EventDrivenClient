using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppItem_ViewModel : AbstractEventDrivenViewModel{

    public AppItem_ViewModel(AppModel appModel) : base(appModel){
        this._appModel = appModel;
        this.AppName = appModel.AppName;
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
        InitMap();
        
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

    private Dictionary<int, int> _appPartialSequenceMap;

    public int MaxScreenConfigNumber { get; private set; }


    private void InitMap() {
        _appPartialSequenceMap = new Dictionary<int, int>();
        for (int i = 0; i < MaxScreenConfigNumber; i++) {
            _appPartialSequenceMap.Add(i, -1);
        }
    }

    public override string ToString() {
        return this.AppName;
    }
}