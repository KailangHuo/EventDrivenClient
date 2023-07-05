using System.Collections.Generic;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class ScreenContentObject {

    public ScreenContentObject(StudyCollectionItem studyCollectionItem, AppSequenceItem appSequenceItem) {
        this.StudyCollectionStrs = studyCollectionItem.GetStudyUidComposition();
        this.AppName = appSequenceItem.AppItemViewModel.AppName;
        this.appSeqNumber = appSequenceItem.AppSequenceNumber;
    }

    public List<string> StudyCollectionStrs { get; private set; }

    public string AppName { get; private set; }

    public int appSeqNumber { get; private set; }

    public int screenNumber { get; private set; }
    

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        ScreenContentObject screenContentObject = (ScreenContentObject)obj;
        if (screenContentObject.StudyCollectionStrs == null 
            || screenContentObject.AppSequenceItem == null) return false;
        return this.StudyCollectionStrs.Equals(screenContentObject.StudyCollectionStrs)
               && this.AppSequenceItem.Equals(screenContentObject.AppSequenceItem);
    }

    public override int GetHashCode() {
        return (StudyCollectionStrs.GetHashCode() + AppSequenceItem.GetHashCode() + "").GetHashCode();
    }
}