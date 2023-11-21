using System.Collections.Generic;
using System.Linq;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.Models; 

public class StudyCollection : AbstractEventDrivenModel{

    public StudyCollection() {
        StudyCollectionItems = new List<StudyCollectionItem>();
        _studyHashSet = new HashSet<Study>();
        MaxStudyNumber = SystemConfiguration.GetInstance().GetMaxStudyNumber();
        StudyLockManager = new StudyLockManager();
    }

    private List<StudyCollectionItem> StudyCollectionItems;

    private StudyLockManager StudyLockManager;

    private HashSet<Study> _studyHashSet;

    private int MaxStudyNumber;

    public bool AddStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (this.ContainsAnyStudy(studyCollectionItem.GetStudyComposition())) {
            ExceptionManager.GetInstance().ThrowAsyncException("存在已打开Study,不可重复添加!");
            return false;
        }
        if (this.StudyCollectionItems.Count == MaxStudyNumber) {
            ExceptionManager.GetInstance().ThrowAsyncException("已达最大Study数目!不可再增加!");
            return false;
        }
        
        StudyCollectionItems.Add(studyCollectionItem);
        StudyLockManager.AddItem(studyCollectionItem);
        AddStudiesInHashSet(studyCollectionItem.GetStudyComposition());
        PublishEvent(nameof(AddStudyCollectionItem), studyCollectionItem);
        return true;
    }

    public bool AppendStudyToCollectionItem(StudyCollectionItem studyCollectionItem, List<Study> studies) {
        HashSet<Study> studiesHashSet = new HashSet<Study>(studies);
        if (studiesHashSet.Count != studies.Count) {
         ExceptionManager.GetInstance().ThrowAsyncException("添加的Study有重复内容");
         return false;
        }
        if (this.ContainsAnyStudy(studies)) {
            ExceptionManager.GetInstance().ThrowAsyncException("存在已打开Study,不可重复添加!");
            return false;
        }
        studyCollectionItem.AppendStudies(studies);
        AddStudiesInHashSet(studies);
        return true;
    }

    public void DeleteStudyCollectionItem(StudyCollectionItem studyCollectionItem) {
        if (this.Contains(studyCollectionItem)) {
            StudyCollectionItems.Remove(studyCollectionItem);
            StudyLockManager.RemoveItem(studyCollectionItem);
            RemoveStudiesFromHashSet(studyCollectionItem.GetStudyComposition());
            PublishEvent(nameof(DeleteStudyCollectionItem), studyCollectionItem);
            // TODO: Delete完成后必须要终止对应的进程
        }
    }

    public void DeleteAllStudyCollectionItem() {
        if (this.StudyCollectionItems.Count > 0) {
            StudyCollectionItems = new List<StudyCollectionItem>();
            _studyHashSet = new HashSet<Study>();
            StudyLockManager.Clear();
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