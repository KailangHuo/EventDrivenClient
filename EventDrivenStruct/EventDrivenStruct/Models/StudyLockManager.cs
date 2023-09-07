using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.Models; 

public class StudyLockManager : AbstractEventDrivenObject {

    public StudyLockManager() {
        this._studyList = new List<StudyCollectionItem>();
        this.maxLockNumber = SystemConfiguration.GetInstance().GetMaxStudyNumber() 
                             - SystemConfiguration.GetInstance().GetRemainStudyUnlockNumber();
        this.currentLockNumber = 0;
    }

    private int maxLockNumber;

    private int currentLockNumber;

    private List<StudyCollectionItem> _studyList;

    public void AddItem(StudyCollectionItem studyCollectionItem) {
        this._studyList.Add(studyCollectionItem);
        studyCollectionItem.RegisterObserver(this);
        if(isLockFull) LockFull();
        else PermitLock();
    }

    public void RemoveItem(StudyCollectionItem studyCollectionItem) {
        if (this._studyList.Contains(studyCollectionItem)) {
            studyCollectionItem.Unlock();
            this._studyList.Remove(studyCollectionItem);
            studyCollectionItem.DeregisterObserver(this);
        }
    }

    #region NOTIFIABLE_PROPERTIES

    private bool isLockFull;

    public bool IsLockFull {
        get {
            return isLockFull;
        }
        set {
            if(isLockFull == value)return;
            isLockFull = value;
            if(isLockFull) LockFull();
            else PermitLock();
            PublishEvent(nameof(IsLockFull), isLockFull);
        }
    }

    #endregion

    public void AddLockNumber() {
        if (currentLockNumber == maxLockNumber) return;
        currentLockNumber++;
        if (currentLockNumber == maxLockNumber) IsLockFull = true;
    }

    public void ReduceLockNumber() {
        if(currentLockNumber == 0) return;
        currentLockNumber--;
        IsLockFull = false;
    }

    private void LockFull() {
        for (int i = 0; i < _studyList.Count; i++) {
            StudyCollectionItem studyCollectionItem = _studyList[i];
            studyCollectionItem.IsLockable = studyCollectionItem.IsLocked ? true : false;
        }
    }

    private void PermitLock() {
        for (int i = 0; i < _studyList.Count; i++) {
            _studyList[i].IsLockable = true;
        }
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyCollectionItem.IsLocked))) {
            bool hasLockedOne = (bool)o;
            if (hasLockedOne) AddLockNumber();
            else ReduceLockNumber();
        }
    }
}