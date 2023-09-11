using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollectionItem : AbstractEventDrivenObject{

    #region CONSTRUCTION

    public StudyCollectionItem(List<Study> studies = null) {
        studyComposition = studies == null ? new List<Study>() : studies;
        studyUidComposition = new List<string>();
        _studyHashSet = new HashSet<Study>();
        Init();
        IsLockable = true;
        IsLocked = false;
        CheckSinglePatient();
    }
    
    private void Init() {
        for (int i = 0; i < studyComposition.Count; i++) {
            studyUidComposition.Add(studyComposition[i].StudyInstanceId);
            _studyHashSet.Add(studyComposition[i]);
        }
    }

    #endregion

    #region PROPERTIES

    private List<Study> studyComposition;

    private List<string> studyUidComposition;

    private HashSet<Study> _studyHashSet;

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
    
    public void AddInStudyComposition(List<Study> studies) {
        if (this.ContainsAnyStudy(studies)) {
            ExceptionManager.GetInstance().ThrowAsyncException("存在已打开的Study, 不可以append");
            return;
        }

        for (int i = 0; i < studies.Count; i++) {
            this.studyComposition.Add(studies[i]);
            studyUidComposition.Add(studies[i].StudyInstanceId);
            _studyHashSet.Add(studies[i]);
        }
        
        CheckSinglePatient();
    }
    
    private bool ContainsAnyStudy(List<Study> studies) {
        HashSet<Study> hashSet = new HashSet<Study>(studies);
        return this._studyHashSet.Overlaps(hashSet);
    }

    private void CheckSinglePatient() {
        IsSinglePatient = this._studyHashSet.Count <= 1 ? true : false;
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
        return this._studyHashSet.SetEquals(t._studyHashSet);
    }

    public override int GetHashCode() {
        if (this._studyHashSet.Count == 0) return base.GetHashCode();
        string hashStr = "";
        for (int i = 0; i < studyUidComposition.Count; i++) {
            hashStr += studyComposition[i].StudyInstanceId.GetHashCode();
        }
        return hashStr.GetHashCode();
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