using System.Collections.Generic;
using System.Linq;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollection : AbstractEventDrivenObject{

    public StudyCollection() {
        StudyCollectionItems = new List<StudyCollectionItem>();
        _studyHashSet = new HashSet<Study>();
    }

    private List<StudyCollectionItem> StudyCollectionItems;

    private HashSet<Study> _studyHashSet;

    public bool AddStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (this.ContainsAnyStudy(studyCollectionItem.GetStudyComposition())) {
            return false;
        }
        StudyCollectionItems.Add(studyCollectionItem);
        AddStudiesInHashSet(studyCollectionItem.GetStudyComposition());
        PublishEvent(nameof(AddStudyCollectionItem), studyCollectionItem);
        return true;
    }

    public void DeleteStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (this.Contains(studyCollectionItem)) {
            StudyCollectionItems.Remove(studyCollectionItem);
            RemoveStudiesFromHashSet(studyCollectionItem.GetStudyComposition());
            PublishEvent(nameof(DeleteStudyCollectionItem), studyCollectionItem);
        }
    }

    public bool Contains(StudyCollectionItem studyCollectionItem) {
        return this.StudyCollectionItems.Contains(studyCollectionItem);
    }

    private bool ContainsAnyStudy(List<Study> studies) {
        HashSet<Study> hashSet = new HashSet<Study>(studies);
        return this._studyHashSet.Overlaps(hashSet);
    }

    private void AddStudiesInHashSet(List<Study> studies) {
        for (int i = 0; i < studies.Count; i++) {
            this._studyHashSet.Add(studies[i]);
        }
    }

    private void RemoveStudiesFromHashSet(List<Study> studies) {
        for (int i = 0; i < studies.Count; i++) {
            this._studyHashSet.Remove(studies[i]);
        }
    }

}