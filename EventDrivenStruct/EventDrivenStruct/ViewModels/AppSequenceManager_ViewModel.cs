using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceManager_ViewModel : AbstractEventDrivenViewModel{

    public AppSequenceManager_ViewModel( ) {
        _appSequenceStack = new List<AdvancedApp_ViewModel>();
    }

    private List<AdvancedApp_ViewModel> _appSequenceStack;

    private AdvancedApp_ViewModel _selectedApp;

    public AdvancedApp_ViewModel SelectedApp {
        get {
            return _selectedApp;
        }
        set {
            if(_selectedApp == value || !this._appSequenceStack.Contains(value))return;
            _selectedApp = value;
            MaintainAppSequece(_selectedApp);
            //PublishEvent to hosting window...
            RisePropertyChanged(nameof(SelectedApp));
        }
    }

    public void ChangedSelection(AdvancedApp_ViewModel advancedAppViewModel) {
        SelectedApp = advancedAppViewModel;
    }

    public void AddApp(AdvancedApp_ViewModel advancedAppViewModel) {
        _appSequenceStack.Insert(0,advancedAppViewModel);
        RefreshSelectedApp();
    }

    public void RemoveApp(AdvancedApp_ViewModel advancedAppViewModel) {
        this._appSequenceStack.Remove(advancedAppViewModel);
        RefreshSelectedApp();
    }

    private void RefreshSelectedApp() {
        if (_appSequenceStack.Count > 0) SelectedApp = _appSequenceStack[0];
        else SelectedApp = null; 
    }

    private void MaintainAppSequece(AdvancedApp_ViewModel appViewModel) {
        if(appViewModel == null) return;
        _appSequenceStack.Remove(appViewModel);
        _appSequenceStack.Insert(0, appViewModel);
    }

    
}