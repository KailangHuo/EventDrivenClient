using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class Study_ViewModel : AbstractEventDrivenViewModel{

    public Study_ViewModel(StudyCollectionItem studyCollectionItem) {
        this._studyCollectionItem = studyCollectionItem;
        this.PatientName = studyCollectionItem.GetStudyComposition()[0].PatientName;
        this.PatientAge = studyCollectionItem.GetStudyComposition()[0].PatientAge;
        this.PatientGender = studyCollectionItem.GetStudyComposition()[0].PatientGender;
        this.StudyUid = studyCollectionItem.GetStudyComposition()[0].StudyInstanceId;
    }

    public StudyCollectionItem _studyCollectionItem;

    private string _patientName;

    public string PatientName {
        get {
            return _patientName;
        }
        set {
            if(_patientName.Equals(value)) return;
            _patientName = value;
            RisePropertyChanged(nameof(PatientName));
        }
    }

    private string _patientAge;

    public string PatientAge {
        get {
            return _patientAge;
        }
        set {
            if(_patientAge.Equals(value))return;
            _patientAge = value;
            RisePropertyChanged(nameof(PatientAge));
        }
    }

    private string _patientGender;

    public string PatientGender {
        get {
            return _patientGender;
        }
        set {
            if(_patientGender.Equals(value))return;
            _patientGender = value;
            RisePropertyChanged(nameof(PatientGender));
        }
    }

    private string _studyUid;

    public string StudyUid {
        get {
            return _studyUid;
        }
        set {
            if(_studyUid.Equals(value))return;
            _studyUid = value;
            RisePropertyChanged(nameof(StudyUid));
        }
    }

    public void TryDelete(StudyCollectionItem item) {
        if (item == this._studyCollectionItem) {
            PublishEvent(nameof(TryDelete), this);
        }
    }
    
    
    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        Study_ViewModel t = (Study_ViewModel)obj;
        return this._studyCollectionItem == t._studyCollectionItem;
    }

    public override int GetHashCode() {
        return this._studyCollectionItem.GetHashCode();
    }

    #endregion

}