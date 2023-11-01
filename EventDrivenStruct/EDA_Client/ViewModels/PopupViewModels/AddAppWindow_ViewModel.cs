using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AddAppWindow_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public AddAppWindow_ViewModel(AppTab_ViewModel appTabViewModel, int screenNumber) {
        this._appTabViewModel = appTabViewModel;
        this._screenNumber = screenNumber;
        this.IsLifeCycleEnd = false;
        SetUpApplicationTypeList();
        SetupCommands();
    }
    
    private void SetUpApplicationTypeList() {
        AppTypes = new ObservableCollection<string>();
        List<string> typeList = SystemConfiguration.GetInstance().GetAppList();
        for (int i = 0; i < typeList.Count; i++) {
            AppTypes.Add(typeList[i]);
        }
    }
    
    private void SetupCommands() {
        ConfirmCommand = new CommonCommand(Confirm);
    }

    #endregion

    #region PROPERTIES

    private AppItem_ViewModel _appItemViewModel;

    private AppTab_ViewModel _appTabViewModel;

    private int _screenNumber;
    
    #endregion
 
    #region NOTIFIABLE_PROPERTIES

    private string _appType;

    public string AppType {
        get {
            return _appType;
        }
        set {
            if(_appType == value)return;
            _appType = value;
        }
    }

    private ObservableCollection<string> _appTypes;

    public ObservableCollection<string> AppTypes {
        get {
            return _appTypes;
        }
        set {
            _appTypes = value;
            RisePropertyChanged(nameof(AppTypes));
        }
    }
    
    private bool _isLifeCycleEnd;

    public bool IsLifeCycleEnd {
        get {
            return _isLifeCycleEnd;
        }
        set {
            if(_isLifeCycleEnd == value)return;
            _isLifeCycleEnd = value;
            RisePropertyChanged(nameof(IsLifeCycleEnd));
        }
    }

    #endregion

    #region COMMANDS

    public ICommand ConfirmCommand { get; private set; }
    
    #endregion

    #region COMMAND_BINDING_METHODS

    private void Confirm(object o = null) {
        if (string.IsNullOrEmpty(AppType)) {
            MessageBox.Show("No Application Selected!");
            return;
        }
        StudyCollectionItem studyCollectionItem = this._appTabViewModel.CurrentSelectedStudyCollectionItem;
        _appItemViewModel = new AppItem_ViewModel(new AppModel(AppType,studyCollectionItem));
        this._appTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[_screenNumber].AppItemSelected =
            _appItemViewModel; 
        IsLifeCycleEnd = true;
    }

    #endregion
    
}