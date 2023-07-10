using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTitleSequenceManager_ViewModel : AbstractEventDrivenViewModel{

    public AppTitleSequenceManager_ViewModel(int sequenceNumber) {
        _appSequenceItemStack = new List<AppTitleSequenceItem>();
        _selectedAppTitleItem = null;
        this.SequenceNumber = sequenceNumber;
        this._currentShowingAppTitleSequenceItem = null;
    }

    private AppTitleSequenceItem _currentShowingAppTitleSequenceItem;

    public int SequenceNumber;

    private List<AppTitleSequenceItem> _appSequenceItemStack;

    private AppTitleItem_ViewModel _selectedAppTitleItem;
    
    public AppTitleItem_ViewModel SelectedAppTitleItem {
        get {
            return _selectedAppTitleItem;
        }
        set {
            if(_selectedAppTitleItem == value )return;
            _selectedAppTitleItem = value;
            PublishEvent(nameof(SelectedAppTitleItem), this);
        }
    }
    
    private void TryUpdatePeekNode() {
        if (_appSequenceItemStack.Count > 0) {
            _selectedAppTitleItem = _appSequenceItemStack[0].AppTitleItemViewModel;
        }
        else _selectedAppTitleItem = null; 
        PeekNodeChanged();
        RisePropertyChanged(nameof(SelectedAppTitleItem));
    }

    public void PeekNodeChanged() {
        PublishEvent(nameof(PeekNodeChanged), this);
    }

    public AppTitleSequenceItem GetPeekAppSeqItem() {
        if (_appSequenceItemStack.Count > 0) {
            return _appSequenceItemStack[0];
        }

        return null;
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void ChangedSelection(AppModel appModel) {
        SelectedAppTitleItem = new AppTitleItem_ViewModel(appModel);
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void SelectToOpen(AppModel appModel) {
        ChangedSelection(appModel);
    }

    public void AddAppSequenceItem(AppTitleSequenceItem appTitleSequenceItem) {
        if(_appSequenceItemStack.Count > 0 && _appSequenceItemStack[0].AppTitleItemViewModel.Equals(appTitleSequenceItem.AppTitleItemViewModel)) return;
        _appSequenceItemStack.Insert(0,appTitleSequenceItem);
        TryUpdatePeekNode();
    }

    public void RemoveApp(AppTitleItem_ViewModel appTitleItemViewModel) {
        for (int i = 0; i < _appSequenceItemStack.Count; i++) {
            if (_appSequenceItemStack[i].AppTitleItemViewModel.Equals(appTitleItemViewModel)) {
                _appSequenceItemStack.Remove(_appSequenceItemStack[i]);
                TryUpdatePeekNode();
                break;
            }
        }
    }
    

    public override string ToString() {
        if (SelectedAppTitleItem == null) return "Empty";
        return SelectedAppTitleItem.AppName +"'s " + _appSequenceItemStack[0].AppSequenceNumber;
    }
}