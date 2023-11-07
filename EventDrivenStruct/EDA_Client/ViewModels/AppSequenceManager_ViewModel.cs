using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceManager_ViewModel : AbstractEventDrivenViewModel{
    
    #region CONSTRUCTION

    public AppSequenceManager_ViewModel(int sequenceNumber) {
        _appSequenceItemStack = new List<AppSequenceItem>();
        _appItemSelected = null;
        this.SequenceNumber = sequenceNumber;
        this._currentShowingAppSequenceItem = null;
    }

    #endregion

    #region NOTIFIABLE_PROPERTIES

    private AppItem_ViewModel _appItemSelected;
    
    public AppItem_ViewModel AppItemSelected {
        get {
            return _appItemSelected;
        }
        set {
            if(_appItemSelected == value )return;
            _appItemSelected = value;
            if(_appItemSelected != null)PublishEvent(nameof(AppItemSelected), this);
        }
    }

    #endregion
    
    #region PROPERTIES

    private AppSequenceItem _currentShowingAppSequenceItem;

    public int SequenceNumber;

    private List<AppSequenceItem> _appSequenceItemStack;

    #endregion
    
    #region METHODS

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

    #endregion
    
    #region |||TEST_ONLY|||

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
        MainEntry_ModelFacade.GetInstance().DeleteApp(appModel);
    }

    #endregion
    
    public override string ToString() {
        if (AppItemSelected == null) return "Empty";
        return AppItemSelected.AppName +"'s " + _appSequenceItemStack[0].AppSequenceNumber;
    }
}