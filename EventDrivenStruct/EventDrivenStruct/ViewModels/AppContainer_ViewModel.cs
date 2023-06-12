using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppContainer_ViewModel : AbstractEventDrivenViewModel{

    public AppContainer_ViewModel( ) {
        _advancedAppViewModels = new ObservableCollection<AdvancedApp_ViewModel>();
        _appSequenceStack = new List<AdvancedApp_ViewModel>();
    }

    private ObservableCollection<AdvancedApp_ViewModel> _advancedAppViewModels;

    private List<AdvancedApp_ViewModel> _appSequenceStack;

    private AdvancedApp_ViewModel _selectedApp;

    public AdvancedApp_ViewModel SelectedApp {
        get {
            return _selectedApp;
        }
        set {
            if(_selectedApp == value)return;
            _selectedApp = value;
            MaintainAppSequece(_selectedApp);
            //PublishEvent...
            RisePropertyChanged(nameof(SelectedApp));
        }
    }

    public void AddApp(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        _appSequenceStack.Insert(0,advancedAppViewModel);
        appModel.RegisterObserver(advancedAppViewModel);
        this._advancedAppViewModels.Add(advancedAppViewModel);
        RefreshSelectedApp();
    }

    public void RemoveApp(AppModel appModel) {
        AdvancedApp_ViewModel advancedAppViewModel = new AdvancedApp_ViewModel(appModel);
        this._advancedAppViewModels.Remove(advancedAppViewModel);
        this._appSequenceStack.Remove(advancedAppViewModel);
        RefreshSelectedApp();
    }

    private void RefreshSelectedApp() {
        if (_advancedAppViewModels.Count > 0) SelectedApp = _advancedAppViewModels[0];
        else SelectedApp = null; 
    }

    private void MaintainAppSequece(AdvancedApp_ViewModel appViewModel) {
        if(appViewModel == null) return;
        _appSequenceStack.Remove(appViewModel);
        _appSequenceStack.Insert(0, appViewModel);
    }

    
}