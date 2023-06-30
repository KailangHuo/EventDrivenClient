using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceManager_ViewModel : AbstractEventDrivenViewModel{

    public AppSequenceManager_ViewModel( ) {
        _appSequenceStack = new List<AppSequenceItem>();
        _selectedAppItem = null;
    }

    private List<AppSequenceItem> _appSequenceStack;

    private AppSequenceItem _peekAppSeqItem;

    private AppItem_ViewModel _selectedAppItem;
    
    public AppItem_ViewModel SelectedAppItem {
        get {
            return _selectedAppItem;
        }
        set {
            if(_selectedAppItem == value )return;
            _selectedAppItem = value;
            PublishEvent(nameof(SelectedAppItem), this);
        }
    }
    
    public void TryUpdatePeekNode() {
        if (_appSequenceStack.Count > 0) {
            _selectedAppItem = _appSequenceStack[0].AppItemViewModel;
        }
        else _selectedAppItem = null; 
        PublishEvent(nameof(TryUpdatePeekNode), this);
        RisePropertyChanged(nameof(SelectedAppItem));
        
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void ChangedSelection(AppModel appModel) {
        SelectedAppItem = new AppItem_ViewModel(appModel);
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void SelectToOpen(AppModel appModel) {
        ChangedSelection(appModel);
    }

    public void AddApp(AppSequenceItem appSequenceItem) {
        if(_appSequenceStack.Count > 0 && _appSequenceStack[0].AppItemViewModel.Equals(appSequenceItem.AppItemViewModel)) return;
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
        if (SelectedAppItem == null) return "Empty";
        return SelectedAppItem.AppName +"'s " + _appSequenceStack[0].AppSequenceNumber;
    }
}