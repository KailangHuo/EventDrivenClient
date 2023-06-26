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
    
    public AppItem_ViewModel PeekNodeAppItem {
        get {
            return _peekNodeAppItem;
        }
        set {
            if(_peekNodeAppItem == value )return;
            _peekNodeAppItem = value;
            PublishEvent(nameof(PeekNodeAppItem), this);
        }
    }
    
    private void RefreshPeekApp() {
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
        if(_appSequenceStack.Count > 0 && _appSequenceStack[0].Equals(appItemViewModel)) return;
        _appSequenceStack.Insert(0,appItemViewModel);
        RefreshPeekApp();
    }

    public void RemoveApp(AppItem_ViewModel appItemViewModel) {
        if(!_appSequenceStack.Contains(appItemViewModel)) return;
        this._appSequenceStack.Remove(appItemViewModel);
        RefreshPeekApp();
    }

    private void PlaceElementToTop(AppItem_ViewModel appItemViewModel) {
        if(_appSequenceStack.Count > 0 && _appSequenceStack[0].Equals(appItemViewModel)) return;
        if(appItemViewModel == null) return;
        _appSequenceStack.Remove(appItemViewModel);
        _appSequenceStack.Insert(0, appItemViewModel);
        RefreshPeekApp();
    }

    public override string ToString() {
        if (PeekNodeAppItem == null) return "Empty";
        return PeekNodeAppItem.AppName;
    }
}