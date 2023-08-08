using System.Collections.Generic;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppItem_ViewModel : AbstractEventDrivenViewModel{

    public AppItem_ViewModel(AppModel appModel) : base(appModel){
        this.AppModel = appModel;
        this.AppName = appModel.AppName;
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
        SetupCommand();
    }

    private void SetupCommand() {
        CloseThisAppCommand = new CommonCommand(CloseThisApp);
    }

    #region COMMANDS

    public ICommand CloseThisAppCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void CloseThisApp(object o = null) {
        MainEntry_ModelFacade.GetInstance().DeleteApp(this.AppModel);
    }

    #endregion
    

    public AppModel AppModel;

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
        return this.AppModel.ToString() + "_VM";
    }
}