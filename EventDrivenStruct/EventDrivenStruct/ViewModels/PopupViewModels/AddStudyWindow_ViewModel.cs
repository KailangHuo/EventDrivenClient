
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AddStudyWindow_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public AddStudyWindow_ViewModel() {
        AddExamItemViewModels = new ObservableCollection<AddingStudyItem_ViewModel>();
        this.Add();
        SetupCommands();
        SetUpApplicationTypeList();
    }

    private void SetupCommands() {
        ConfirmCommand = new CommonCommand(Confirm);
        CancleCommand = new CommonCommand(Cancle);
        AddCommand = new CommonCommand(Add);
    }

    private void SetUpApplicationTypeList() {
        AppTypes = new ObservableCollection<string>();
        List<string> typeList = SystemConfiguration.GetInstance().GetAppList();
        for (int i = 0; i < typeList.Count; i++) {
            AppTypes.Add(typeList[i]);
        }
    }

    #endregion
    
    #region PROPERTY

    private AppModel _appModel;

    #endregion

    #region NOTIFIABLE_PROPERTIES

    public ObservableCollection<AddingStudyItem_ViewModel> AddExamItemViewModels { get; private set; }

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
    
    private ObservableCollection<string> _appTypes;

    public ObservableCollection<string> AppTypes {
        get {
            return _appTypes;
        }
        set {
            if(AppTypes == value) return;
            _appTypes = value;
            RisePropertyChanged(nameof(AppTypes));
        }
    }

    private string _appType;

    public string AppType {
        get {
            return _appType;
        }
        set {
            if(_appType == value)return;
            _appType = value;
            RisePropertyChanged(AppType);
        }
    }

    private bool _onlyOneElement;

    public bool OnlyOneElement {
        get {
            return _onlyOneElement;
        }
        set {
            if (_onlyOneElement == value) return;
            _onlyOneElement = value;
            if (_onlyOneElement) DisableClose();
            else EnableClose();
            RisePropertyChanged(nameof(OnlyOneElement));
        }
    }


    #endregion

    #region COMMANDS

    public ICommand ConfirmCommand { get; private set; }
    
    public ICommand CancleCommand { get; private set; }
    
    public ICommand AddCommand { get; private set; }


    #endregion

    #region COMMAND_BINDING_METHOD

    private void Confirm(object o = null) {
        if (!CheckItemFilledFinished() || string.IsNullOrEmpty(_appType)) {
            MessageBox.Show("参数不完整！");
            return;
        }

        List<Study> studies = new List<Study>();
        for (int i = 0; i < this.AddExamItemViewModels.Count; i++) {
            studies.Add(AddExamItemViewModels[i].GetStudy());
        }
        
        StudyCollectionItem studyCollectionItem = new StudyCollectionItem(studies);
        _appModel = new AppModel(_appType, studyCollectionItem);
  
        if(MainEntry_ModelFacade.GetInstance().AddStudyItemWithApp(studyCollectionItem, _appModel))IsLifeCycleEnd = true;
        
    }

    private void Cancle(object o = null) {
        IsLifeCycleEnd = true;
    }

    private void Add(object o = null) {
        AddingStudyItem_ViewModel addingStudyItemViewModel = new AddingStudyItem_ViewModel();
        this.AddExamItemViewModels.Add(addingStudyItemViewModel);
        addingStudyItemViewModel.RegisterObserver(this);
        OnlyOneElement = this.AddExamItemViewModels.Count == 1;
    }
    
    #endregion

    private bool CheckItemFilledFinished() {
        for (int i = 0; i < this.AddExamItemViewModels.Count; i++) {
            if (!AddExamItemViewModels[i].IsBlankFilledFinished()) {
                return false;
            }
        }

        return true;
    }

    private void DisableClose() {
        for (int i = 0; i < this.AddExamItemViewModels.Count; i++) {
            AddExamItemViewModels[i].CanClose = false;
        }
    }

    private void EnableClose() {
        for (int i = 0; i < AddExamItemViewModels.Count; i++) {
            AddExamItemViewModels[i].CanClose = true;

        }
    }

    public void RemoveOneAddExamItem(AddingStudyItem_ViewModel addingStudyItemViewModel) {
        if (this.AddExamItemViewModels.Contains(addingStudyItemViewModel)) {
            this.AddExamItemViewModels.Remove(addingStudyItemViewModel);
            addingStudyItemViewModel.DeregisterObserver(this);
            OnlyOneElement = this.AddExamItemViewModels.Count == 1;
        }
    }
    
    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(AddingStudyItem_ViewModel.CloseCommand))) {
            AddingStudyItem_ViewModel addingStudyItemViewModel = (AddingStudyItem_ViewModel)o;
            RemoveOneAddExamItem(addingStudyItemViewModel);
            return;
        }
    }
}