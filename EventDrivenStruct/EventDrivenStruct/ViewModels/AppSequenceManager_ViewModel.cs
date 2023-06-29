using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceManager_ViewModel : AbstractEventDrivenViewModel{

    public AppSequenceManager_ViewModel( ) {
        _appSequenceStack = new List<AppSequenceItem>();
    }

    private List<AppSequenceItem> _appSequenceStack;

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
    
    public void TryUpdatePeekNode() {
        if (_appSequenceStack.Count > 0) _peekNodeAppItem = _appSequenceStack[0].AppItemViewModel;
        else _peekNodeAppItem = null; 
        PublishEvent(nameof(TryUpdatePeekNode), this);
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

    public void AddApp(AppSequenceItem appSequenceItem) {
        if(_appSequenceStack.Count > 0 && _appSequenceStack[0].Equals(appSequenceItem)) return;
        _appSequenceStack.Insert(0,appSequenceItem);
        TryUpdatePeekNode();
    }

    public void RemoveApp(AppItem_ViewModel appItemViewModel) {
        for (int i = 0; i < _appSequenceStack.Count; i++) {
            if (_appSequenceStack[i].AppItemViewModel.Equals(appItemViewModel)) {
                _appSequenceStack.Remove(_appSequenceStack[i]);
                TryUpdatePeekNode();
                break;
            }
        }
    }

    public override string ToString() {
        if (PeekNodeAppItem == null) return "Empty";
        return PeekNodeAppItem.AppName;
    }
}