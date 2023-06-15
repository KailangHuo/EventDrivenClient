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

    /// <summary>
    /// TODO:有大问题 -> 点击触发新增App时不可能再viewmodel层做, 你需要向facade发送命令来处理!!
    /// </summary>
    public AdvancedApp_ViewModel SelectedApp {
        get {
            return _selectedApp;
        }
        set {
            if(_selectedApp == value )return;
            _selectedApp = value;
            PublishEvent(nameof(SelectedApp), this);
        }
    }
    
    private void RefreshSelectedApp() {
        if (_appSequenceStack.Count > 0) _selectedApp = _appSequenceStack[0];
        else _selectedApp = null; 
        RisePropertyChanged(nameof(SelectedApp));
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void ChangedSelection(AppModel appModel) {
        SelectedApp = new AdvancedApp_ViewModel(appModel);
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void SelectToOpen(AppModel appModel) {
        ChangedSelection(appModel);
    }

    public void AddApp(AdvancedApp_ViewModel advancedAppViewModel) {
        if(_appSequenceStack.Contains(advancedAppViewModel)) return;
        _appSequenceStack.Insert(0,advancedAppViewModel);
        RefreshSelectedApp();
    }

    public void RemoveApp(AdvancedApp_ViewModel advancedAppViewModel) {
        if(!_appSequenceStack.Contains(advancedAppViewModel)) return;
        this._appSequenceStack.Remove(advancedAppViewModel);
        RefreshSelectedApp();
    }

    private void PlaceElementToTop(AdvancedApp_ViewModel appViewModel) {
        if(appViewModel.Equals(_appSequenceStack[0])) return;
        if(!_appSequenceStack.Contains(appViewModel)) return;
        if(appViewModel == null) return;
        _appSequenceStack.Remove(appViewModel);
        _appSequenceStack.Insert(0, appViewModel);
        RefreshSelectedApp();
    }

    public override string ToString() {
        if (SelectedApp == null) return "Empty";
        return SelectedApp.AppName;
    }
}