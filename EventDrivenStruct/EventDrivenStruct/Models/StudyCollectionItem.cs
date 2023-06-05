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
}