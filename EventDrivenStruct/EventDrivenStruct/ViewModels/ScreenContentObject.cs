using System.Collections.Generic;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class ScreenContentObject {

    public ScreenContentObject(StudyCollectionItem studyCollectionItem, AppSequenceItem appSequenceItem) {
        this.StudyCollectionSet = new HashSet<string>(studyCollectionItem.GetStudyUidComposition());
        if(appSequenceItem == null) return;
        this.AppName = appSequenceItem.AppItemViewModel.AppName;
        this.AppSeqNumber = appSequenceItem.AppSequenceNumber;
    }

    public HashSet<string> StudyCollectionSet { get; private set; }

    public string AppName { get; private set; }

    public int AppSeqNumber { get; private set; }
    
    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        ScreenContentObject screenContentObject = (ScreenContentObject)obj;
        return this.StudyCollectionSet.SetEquals(screenContentObject.StudyCollectionSet)
               && this.AppName.Equals(screenContentObject.AppName)
               && this.AppSeqNumber.Equals(screenContentObject.AppSeqNumber);
    }

    public override int GetHashCode() {
        return (StudyCollectionSet.GetHashCode() + AppName.GetHashCode() + AppSeqNumber.GetHashCode() + "" ).GetHashCode();
    }
}