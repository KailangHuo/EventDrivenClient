using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class AppModel2 : AbstractEventDrivenObject{

    public AppModel2(string appName, StudyCollectionItem studyCollectionItem) {
        this.StudyCollectionItem = studyCollectionItem;
        this.AppName = appName;
    }
    
    public StudyCollectionItem StudyCollectionItem;

    public string AppName;

    public override string ToString() {
        return this.AppName + " | " + this.StudyCollectionItem.ToString();
    }
    
    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppModel2 appModel = (AppModel2)obj;
        return this.AppName.Equals(appModel.AppName) && this.StudyCollectionItem.Equals(appModel.StudyCollectionItem);
    }

    public override int GetHashCode() {
        return this.AppName.GetHashCode() + this.StudyCollectionItem.GetHashCode();
    }

    #endregion
}