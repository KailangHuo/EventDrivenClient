using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class StudyContainer_ViewModel : AbstractEventDrivenViewModel{

    #region CONSTRUCTION

    public StudyContainer_ViewModel() {
        StudyViewModels = new ObservableCollection<Study_ViewModel>();
        SetupCommands();
    }

    private void SetupCommands() {
        CloseSelectedCommand = new CommonCommand(CloseSelected);
        ClearAllCommand = new CommonCommand(ClearAll);
        TriggerSelectedCommand = new CommonCommand(TriggerSelected);
    }

    #endregion

    #region NOTIFIABLE_PROPERTIES

    public ObservableCollection<Study_ViewModel> StudyViewModels { get; private set; }

    private bool _hasItem;

    public bool HasItem {
        get {
            return _hasItem;
        }
        set {
            if(_hasItem == value) return;
            _hasItem = value;
            RisePropertyChanged(nameof(HasItem));
        }
    }

    private Study_ViewModel _selectedStudy;
    public Study_ViewModel SelectedStudy {
        get { 
            return _selectedStudy;
        }
        set {
            if(_selectedStudy == value) return;
            _selectedStudy = value;
            HasItem = !(_selectedStudy == null);
            PublishEvent(nameof(SelectedStudy), _selectedStudy);
            RisePropertyChanged(nameof(SelectedStudy));
        }
    }

    public void RemoveStudyBroadCast() {
        PublishEvent(nameof(RemoveStudyBroadCast), null);
    }

    #endregion
    
    #region COMMANDS

    public ICommand CloseSelectedCommand { get; private set; }

    public ICommand ClearAllCommand { get; private set; }

    public ICommand TriggerSelectedCommand { get; private set; }

    #endregion

    #region COMMAND_BINDING_METHODS

    public void CloseSelected(object o = null) {
        MainEntry_ModelFacade.GetInstance().DeleteStudyItem(SelectedStudy.StudyCollectionItem);
        RemoveStudyBroadCast();
    }

    public void ClearAll(object o = null) {
        MainEntry_ModelFacade.GetInstance().DeleteAllStudy();
        RemoveStudyBroadCast();
    }

    public void TriggerSelected(object o = null) {
        if(SelectedStudy == null) return;
        PublishEvent(nameof(TriggerSelected), null);
    }

    #endregion

    private void AddStudyViewModel(StudyCollectionItem item) {
        Study_ViewModel studyViewModel = new Study_ViewModel(item);
        item.RegisterObserver(studyViewModel);
        // 不用判断是否存在, 因为在Model层处理好了
        this.StudyViewModels.Insert(0,studyViewModel);
        UpdateSelectedStudy();
    }

    private void RemoveStudyViewModel(StudyCollectionItem item) {
        Study_ViewModel studyViewModel = new Study_ViewModel(item);
        this.StudyViewModels.Remove(studyViewModel);
        UpdateSelectedStudy();
        RemoveStudyBroadCast();
    }

    private void RemoveAllStudyViewModels() {
        this.StudyViewModels = new ObservableCollection<Study_ViewModel>();
        RisePropertyChanged(nameof(StudyViewModels));
        UpdateSelectedStudy();
    }

    private void UpdateSelectedStudy() {
        if (StudyViewModels.Count > 0) SelectedStudy = StudyViewModels[0];
        else SelectedStudy = null;
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyCollection.AddStudyCollectionItem))) {
            StudyCollectionItem item = (StudyCollectionItem)o;
            this.AddStudyViewModel(item);
        }

        if (propertyName.Equals(nameof(StudyCollection.DeleteStudyCollectionItem))) {
            StudyCollectionItem item = (StudyCollectionItem)o;
            this.RemoveStudyViewModel(item);
        }

        if (propertyName.Equals(nameof(StudyCollection.DeleteAllStudyCollectionItem))) {
            RemoveAllStudyViewModels();
        }
    }
}