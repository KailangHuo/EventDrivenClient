using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class StudyContainer_ViewModel : AbstractEventDrivenViewModel{

    public StudyContainer_ViewModel() {
        _studyViewModels = new ObservableCollection<Study_ViewModel>();
    }

    private ObservableCollection<Study_ViewModel> _studyViewModels;

    private Study_ViewModel _selectedStudy;

    public Study_ViewModel SelectedStudy {
        get { 
            return _selectedStudy;
        }
        set {
            if(_selectedStudy == value) return;
            _selectedStudy = value;
            PublishEvent(nameof(SelectedStudy), _selectedStudy);
            RisePropertyChanged(nameof(SelectedStudy));
        }
    }

    private void AddStudyViewModel(StudyCollectionItem item) {
        Study_ViewModel studyViewModel = new Study_ViewModel(item);
        // 不用判断是否存在, 因为在Model层处理好了
        this._studyViewModels.Add(studyViewModel);
    }

    private void RemoveStudyViewModel(StudyCollectionItem item) {
        Study_ViewModel studyViewModel = new Study_ViewModel(item);
        this._studyViewModels.Remove(studyViewModel);
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
        
    }
}