using System.Collections.Generic;
using System.Linq;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollectionItem : AbstractEventDrivenModel{

    #region CONSTRUCTION

    public StudyCollectionItem(List<Study> studies = null) {
        studyComposition = studies == null ? new List<Study>() : studies;
        studyUidComposition = new List<string>();
        appendedStudyComposition = new List<Study>();
        _studyHashSet = new HashSet<Study>();
        Init();
        IsLockable = true;
        IsLocked = false;
        StudyAmountChanged();
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

    private List<Study> appendedStudyComposition;

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

    public void StudyAmountChanged() {
        PublishEvent(nameof(StudyAmountChanged), this);
    }

    #endregion

    #region METHODS

    public List<Study> GetStudyComposition() {
        return studyComposition;
    }

    public int GetStudiesNumber() {
        return this.studyComposition.Count + this.appendedStudyComposition.Count;
    }

    public void AppendStudies(List<Study> studies) {
        if (this.studyComposition.Count == 0) {
            ExceptionManager.GetInstance().ThrowAsyncException("Not implemented Study!");
            return;
        }
        
        for (int i = 0; i < studies.Count; i++) {
            if(this._studyHashSet.Contains(studies[i])) continue;
            this.appendedStudyComposition.Add(studies[i]);
            studyUidComposition.Add(studies[i].StudyInstanceId);
            _studyHashSet.Add(studies[i]);
        }
        
        StudyAmountChanged();
    }

    public void Lock() {
        if (IsLockable) this.IsLocked = true;
        else return;
    }

    public void Unlock() {
        this.IsLocked = false;
    }

    public bool IsSinglePatient() {
        return this._studyHashSet.Count <= 1 ? true : false;
    }

    public List<string> GetStudyUidComposition() {
        return this.studyUidComposition;
    }

    #endregion

    #region |||TEST|||

    public void AddInStudyComposition(List<Study> studies) {

        for (int i = 0; i < studies.Count; i++) {
            this.studyComposition.Add(studies[i]);
            studyUidComposition.Add(studies[i].StudyInstanceId);
            _studyHashSet.Add(studies[i]);
        }
        
        StudyAmountChanged();
    }

    #endregion
    
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
        for (int i = 0; i < studyComposition.Count; i++) {
            hashStr += studyComposition[i].StudyInstanceId.GetHashCode();
        }
        return hashStr.GetHashCode();
    }

    #endregion

    public override string ToString() {
        string s = "";

        List<Study> totalList = new List<Study>();
        totalList.AddRange(studyComposition);
        totalList.AddRange(appendedStudyComposition);

        for (int i = 0; i < totalList.Count - 1; i++) {
            s += totalList[i].ToString() + " | ";
        }

        s += totalList[totalList.Count - 1].ToString();
        
        return s;
    }
}