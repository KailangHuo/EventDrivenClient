using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollectionItem : AbstractEventDrivenObject{

    public StudyCollectionItem(List<Study> studies = null) {
        studyComposition = studies == null ? new List<Study>() : studies;
        studyUidComposition = new List<string>();
        _studyUidHashSet = new HashSet<string>();
        Init();
    }

    private List<Study> studyComposition;

    private List<string> studyUidComposition;

    private HashSet<string> _studyUidHashSet;

    private void Init() {
        for (int i = 0; i < studyComposition.Count; i++) {
            studyUidComposition.Add(studyComposition[i].StudyInstanceId);
            _studyUidHashSet.Add(studyComposition[i].StudyInstanceId);
        }
    }

    public List<Study> GetStudyComposition() {
        return studyComposition;
    }

    /// <summary>
    /// TEST_ONLY
    /// </summary>
    /// <param name="study"></param>
    public void AddInStudyComposition(Study study) {
        if(this._studyUidHashSet.Contains(study.StudyInstanceId)) return;
        this.studyComposition.Add(study);
        studyUidComposition.Add(study.StudyInstanceId);
        _studyUidHashSet.Add(study.StudyInstanceId);
    }

    public List<string> GetStudyUidComposition() {
        return this.studyUidComposition;
    }
    
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

        for (int i = 0; i < studyComposition.Count; i++) {
            s += studyComposition[i].ToString() + ", ";
        }

        return s;
    }
}