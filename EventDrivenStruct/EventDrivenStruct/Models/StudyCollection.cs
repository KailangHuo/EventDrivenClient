using System.Collections.Generic;
using System.Linq;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyCollection : AbstractEventDrivenObject{

    public StudyCollection() {
        StudyCollectionItems = new List<StudyCollectionItem>();
        StudyHashSet = new HashSet<Study>();
    }

    private List<StudyCollectionItem> StudyCollectionItems;

    private HashSet<Study> StudyHashSet;

    public void AddStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (Contains(studyCollectionItem)) {
            AddItemFailed(studyCollectionItem);
            return;
        }
        StudyCollectionItems.Add(studyCollectionItem);
        PublishEvent(nameof(AddStudyCollectionItem), studyCollectionItem);
    }

    public void DeleteAddStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (this.StudyCollectionItems.Contains(studyCollectionItem)) StudyCollectionItems.Remove(studyCollectionItem);
    }

    private bool Contains(StudyCollectionItem studyCollectionItem) {
        HashSet<Study> set = new HashSet<Study>(studyCollectionItem.GetStudyComposition());
        return this.StudyHashSet.IsSupersetOf(set);
    }

    private void AddItemFailed(StudyCollectionItem studyCollectionItem) {
        PublishEvent(nameof(AddItemFailed), studyCollectionItem);
    }

}