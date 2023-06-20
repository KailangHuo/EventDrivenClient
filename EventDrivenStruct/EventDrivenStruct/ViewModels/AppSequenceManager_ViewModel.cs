using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceManager_ViewModel : AbstractEventDrivenViewModel{

    public AppSequenceManager_ViewModel( ) {
        _appSequenceStack = new List<AppItem_ViewModel>();
    }

    private List<AppItem_ViewModel> _appSequenceStack;

    private AppItem_ViewModel _peekNodeAppItem;

    /// <summary>
    /// TODO:有大问题 -> 点击触发新增App时不可能再viewmodel层做, 你需要向facade发送命令来处理!!
    /// </summary>
    public AppItem_ViewModel PeekNodeAppItem {
        get {
            return _peekNodeAppItem;
        }
        set {
            if(_peekNodeAppItem == value )return;
            PublishEvent(nameof(PeekNodeAppItem), this);
        }
    }
    
    private void RefreshSelectedApp() {
        if (_appSequenceStack.Count > 0) _peekNodeAppItem = _appSequenceStack[0];
        else _peekNodeAppItem = null; 
        RisePropertyChanged(nameof(PeekNodeAppItem));
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void ChangedSelection(AppModel appModel) {
        PeekNodeAppItem = new AppItem_ViewModel(appModel);
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void SelectToOpen(AppModel appModel) {
        ChangedSelection(appModel);
    }

    public void AddApp(AppItem_ViewModel appItemViewModel) {
        if(_appSequenceStack[0] == appItemViewModel) return;
        _appSequenceStack.Insert(0,appItemViewModel);
        RefreshSelectedApp();
    }

    public void RemoveApp(AppItem_ViewModel appItemViewModel) {
        if(!_appSequenceStack.Contains(appItemViewModel)) return;
        this._appSequenceStack.Remove(appItemViewModel);
        RefreshSelectedApp();
    }

    private void PlaceElementToTop(AppItem_ViewModel appItemViewModel) {
        if(appItemViewModel.Equals(_appSequenceStack[0])) return;
        if(!_appSequenceStack.Contains(appItemViewModel)) return;
        if(appItemViewModel == null) return;
        _appSequenceStack.Remove(appItemViewModel);
        _appSequenceStack.Insert(0, appItemViewModel);
        RefreshSelectedApp();
    }

    public override string ToString() {
        if (PeekNodeAppItem == null) return "Empty";
        return PeekNodeAppItem.AppName;
    }
}