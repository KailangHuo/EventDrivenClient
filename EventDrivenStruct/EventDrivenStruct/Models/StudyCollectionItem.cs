using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollectionItem : AbstractEventDrivenObject{

    #region CONSTRUCTION

    public StudyCollectionItem(List<Study> studies = null) {
        studyComposition = studies == null ? new List<Study>() : studies;
        studyUidComposition = new List<string>();
        _studyUidHashSet = new HashSet<string>();
        Init();
        IsLockable = true;
        IsLocked = false;
        CheckSinglePatient();
    }
    
    private void Init() {
        for (int i = 0; i < studyComposition.Count; i++) {
            studyUidComposition.Add(studyComposition[i].StudyInstanceId);
            _studyUidHashSet.Add(studyComposition[i].StudyInstanceId);
        }
    }

    #endregion

    #region PROPERTIES

    private List<Study> studyComposition;

    private List<string> studyUidComposition;

    private HashSet<string> _studyUidHashSet;

    #endregion

    #region NOTIFIABLE_PROPERTIES

    private bool isLocked;

    public bool IsLocked {
        get {
            return isLocked;
        }
        set {
            if(isLocked == value)return;
            isLocked = value;
            PublishEvent(nameof(IsLocked), isLocked);
        }
    }

    private bool isLockable;

    public bool IsLockable {
        get {
            return isLockable;
        }
        set {
            if(isLockable == value)return;
            isLockable = value;
            PublishEvent(nameof(IsLockable), isLockable);
        }
    }

    private bool _isSinglePatient;

    public bool IsSinglePatient {
        get {
            return _isSinglePatient;
        }
        set {
            if(_isSinglePatient == value)return;
            _isSinglePatient = value;
            PublishEvent(nameof(IsSinglePatient), this);
        }
    }

    #endregion

    #region METHODS

    public List<Study> GetStudyComposition() {
        return studyComposition;
    }

    public void Lock() {
        if (IsLockable) this.IsLocked = true;
        else return;
    }

    public void Unlock() {
        this.IsLocked = false;
    }
    
    public void AddInStudyComposition(Study study) {
        if(this._studyUidHashSet.Contains(study.StudyInstanceId)) return;
        this.studyComposition.Add(study);
        studyUidComposition.Add(study.StudyInstanceId);
        _studyUidHashSet.Add(study.StudyInstanceId);
        CheckSinglePatient();
    }

    private void CheckSinglePatient() {
        IsSinglePatient = this._studyUidHashSet.Count <= 1 ? true : false;
    }

    public List<string> GetStudyUidComposition() {
        return this.studyUidComposition;
    }

    #endregion

    /// <summary>
    /// TEST_ONLY
    /// </summary>
    /// <param name="study"></param>

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        StudyCollectionItem t = (StudyCollectionItem)obj;
        return this._studyUidHashSet.SetEquals(t._studyUidHashSet);
    }

    public override int GetHashCode() {
        if (this._studyUidHashSet.Count == 0) return base.GetHashCode();
        int hashcode = 0;
        for (int i = 0; i < studyUidComposition.Count; i++) {
            hashcode += studyComposition[i].StudyInstanceId.GetHashCode();
        }
        return hashcode;
    }

    #endregion

    public override string ToString() {
        string s = "";

        for (int i = 0; i < studyComposition.Count - 1; i++) {
            s += studyComposition[i].ToString() + " | ";
        }

        s += studyComposition[studyComposition.Count - 1].ToString();

        return s;
    }
}