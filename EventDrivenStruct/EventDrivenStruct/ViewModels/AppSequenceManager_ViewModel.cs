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
        _appItemSelected = null;
        this.SequenceNumber = sequenceNumber;
        this._currentShowingAppSequenceItem = null;
    }

    private AppSequenceItem _currentShowingAppSequenceItem;

    public int SequenceNumber;

    private List<AppSequenceItem> _appSequenceItemStack;

    private AppItem_ViewModel _appItemSelected;
    
    public AppItem_ViewModel AppItemSelected {
        get {
            return _appItemSelected;
        }
        set {
            if(_appItemSelected == value )return;
            _appItemSelected = value;
            PublishEvent(nameof(AppItemSelected), this);
        }
    }
    
    private void TryUpdatePeekNode() {
        if (_appSequenceItemStack.Count > 0) {
            _appItemSelected = _appSequenceItemStack[0].AppItemViewModel;
        }
        else _appItemSelected = null; 
        PeekNodeChanged();
        RisePropertyChanged(nameof(AppItemSelected));
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

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void ChangedSelection(AppModel appModel) {
        AppItemSelected = new AppItem_ViewModel(appModel);
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void SelectToOpen(AppModel appModel) {
        ChangedSelection(appModel);
    }

    /// <summary>
    /// TEST ONLY
    /// </summary>
    /// <param name="appModel"></param>
    public void CloseApp(AppModel appModel) {
        for (int i = 0; i < _appSequenceItemStack.Count; i++) {
            if (_appSequenceItemStack[i].AppItemViewModel.AppModel.Equals(appModel)) {
                PublishEvent(nameof(CloseApp), appModel);
                break;
            }
        }
    }

    public void AddAppSequenceItem(AppSequenceItem appSequenceItem) {
        if(_appSequenceItemStack.Count > 0 && _appSequenceItemStack[0].AppItemViewModel.Equals(appSequenceItem.AppItemViewModel)) return;
        _appSequenceItemStack.Insert(0,appSequenceItem);
        TryUpdatePeekNode();
    }

    public void RemoveApp(AppItem_ViewModel appItemViewModel) {
        for (int i = 0; i < _appSequenceItemStack.Count; i++) {
            if (_appSequenceItemStack[i].AppItemViewModel.Equals(appItemViewModel)) {
                _appSequenceItemStack.Remove(_appSequenceItemStack[i]);
                TryUpdatePeekNode();
                break;
            }
        }
    }
    

    public override string ToString() {
        if (AppItemSelected == null) return "Empty";
        return AppItemSelected.AppName +"'s " + _appSequenceItemStack[0].AppSequenceNumber;
    }
}