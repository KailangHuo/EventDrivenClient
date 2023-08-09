using System.Collections.Generic;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.Models; 

public class StudyCollection : AbstractEventDrivenObject{

    public StudyCollection() {
        StudyCollectionItems = new List<StudyCollectionItem>();
        _studyHashSet = new HashSet<Study>();
        MaxStudyNumber = SystemConfiguration.GetInstance().GetMaxStudyNumber();
    }

    private List<StudyCollectionItem> StudyCollectionItems;

    private HashSet<Study> _studyHashSet;

    private int MaxStudyNumber;

    public bool AddStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (this.ContainsAnyStudy(studyCollectionItem.GetStudyComposition())) {
            return false;
        }
        if (this.StudyCollectionItems.Count == MaxStudyNumber) return false;
        
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
            // TODO: Delete完成后必须要终止对应的进程
        }
    }

    public void DeleteAllStudyCollectionItem() {
        if (this.StudyCollectionItems.Count > 0) {
            StudyCollectionItems = new List<StudyCollectionItem>();
            _studyHashSet = new HashSet<Study>();
            PublishEvent(nameof(DeleteAllStudyCollectionItem), null);
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

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyAppMappingManager.RemoveStudyAppMapObj))) {
            StudyAppMappingObj obj = (StudyAppMappingObj)o;
            StudyCollectionItem studyCollectionItem = obj.StudyCollectionItem;
            DeleteStudyCollectionItem(studyCollectionItem);
        }
    }
}