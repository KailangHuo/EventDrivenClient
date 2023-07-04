using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class ScreenContentObject {

    public ScreenContentObject(StudyCollectionItem studyCollectionItem, AppSequenceItem appSequenceItem) {
        this.StudyCollectionItem = studyCollectionItem;
        this.AppSequenceItem = appSequenceItem;
    }

    public StudyCollectionItem StudyCollectionItem;

    public AppSequenceItem AppSequenceItem;

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        ScreenContentObject screenContentObject = (ScreenContentObject)obj;
        return this.StudyCollectionItem.Equals(screenContentObject.StudyCollectionItem)
               && this.AppSequenceItem.Equals(screenContentObject.AppSequenceItem);
    }

    public override int GetHashCode() {
        return (StudyCollectionItem.GetHashCode() + AppSequenceItem.GetHashCode() + "").GetHashCode();
    }
}