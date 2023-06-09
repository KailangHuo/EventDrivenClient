using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollectionItem : AbstractEventDrivenObject{

    public StudyCollectionItem() {
        studyComposition = new List<Study>();
    }

    private List<Study> studyComposition;

    public List<Study> GetStudyComposition() {
        return studyComposition;
    }

    public void AddInStudyComposition(Study study) {
        this.studyComposition.Add(study);
    }

    public override string ToString() {
        string s = "";

        for (int i = 0; i < studyComposition.Count; i++) {
            s += studyComposition[i].ToString() + ", ";
        }

        return s;
    }
}