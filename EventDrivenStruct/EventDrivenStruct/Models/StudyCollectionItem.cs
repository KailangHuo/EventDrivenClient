using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollectionItem : AbstractEventDrivenObject{

    public StudyCollectionItem() {
        studyComposition = new List<Study>();
        studyUidComposition = new List<string>();
        InitStudyUidComposition();
    }

    private List<Study> studyComposition;

    private List<string> studyUidComposition;

    private void InitStudyUidComposition() {
        for (int i = 0; i < studyComposition.Count; i++) {
            studyUidComposition.Add(studyComposition[i].StudyInstanceId);
        }
    }

    public List<Study> GetStudyComposition() {
        return studyComposition;
    }

    public void AddInStudyComposition(Study study) {
        this.studyComposition.Add(study);
    }

    public List<string> GetStudyUidComposition() {
        return this.studyUidComposition;
    }

    public override string ToString() {
        string s = "";

        for (int i = 0; i < studyComposition.Count; i++) {
            s += studyComposition[i].ToString() + ", ";
        }

        return s;
    }
}