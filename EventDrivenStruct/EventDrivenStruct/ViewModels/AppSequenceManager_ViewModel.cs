using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceManager_ViewModel : AbstractEventDrivenViewModel{

    public AppSequenceManager_ViewModel(int sequenceNumber) {
        _appSequenceItemStack = new List<AppSequenceItem>();
        _selectedAppItem = null;
        this.SequenceNumber = sequenceNumber;
        this._currentShowingAppSequenceItem = null;
    }

    private AppSequenceItem _currentShowingAppSequenceItem;

    public int SequenceNumber;

    private List<AppSequenceItem> _appSequenceItemStack;

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
    
    private void TryUpdatePeekNode() {
        if (_appSequenceItemStack.Count > 0) {
            _selectedAppItem = _appSequenceItemStack[0].AppItemViewModel;
        }
        else _selectedAppItem = null; 
        PeekNodeChanged();
        RisePropertyChanged(nameof(SelectedAppItem));
    }

    public void PeekNodeChanged() {
        PublishEvent(nameof(PeekNodeChanged), this);
    }

    public AppSequenceItem GetPeekAppSeqItem() {
        if (_appSequenceItemStack.Count > 0) {
            return _appSequenceItemStack[0];
        }

        return null;
    }

    public void InvokePeekApp() {
        if (_appSequenceItemStack.Count > 0 && _appSequenceItemStack[0] != null) {
            _appSequenceItemStack[0].InvokeApp();
        }
        else {
            Debug.WriteLine("Nothing Happened...");
        }
    }

    public void HidePeekApp() {
        if (_appSequenceItemStack.Count > 0 && _appSequenceItemStack[0] != null) {
            _appSequenceItemStack[0].HideApp();
        }
        else {
            Debug.WriteLine("Nothing Happened...");
        }
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

    public void AddAppSequenceItem(AppSequenceItem appSequenceItem) {
        if(_appSequenceItemStack.Count > 0 && _appSequenceItemStack[0].AppItemViewModel.Equals(appSequenceItem.AppItemViewModel)) return;
        _appSequenceItemStack.Insert(0,appSequenceItem);
        appSequenceItem.SetScreenSeqNumber(SequenceNumber);
        TryUpdatePeekNode();
    }

    public void RemoveApp(AppItem_ViewModel appItemViewModel) {
        for (int i = 0; i < _appSequenceItemStack.Count; i++) {
            if (_appSequenceItemStack[i].AppItemViewModel.Equals(appItemViewModel)) {
                _appSequenceItemStack[i].ResetScreenSeqNumber();
                _appSequenceItemStack.Remove(_appSequenceItemStack[i]);
                TryUpdatePeekNode();
                break;
            }
        }
    }
    

    public override string ToString() {
        if (SelectedAppItem == null) return "Empty";
        return SelectedAppItem.AppName +"'s " + _appSequenceItemStack[0].AppSequenceNumber;
    }
}