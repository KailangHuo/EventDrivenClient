using System.Diagnostics;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.Models; 

public class AppModel : AbstractEventDrivenObject{

    public AppModel() {
    }

    public AppModel(string name) {
        AppName = name;
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
    }

    public AppModel(string name, StudyCollectionItem studyCollectionItem) {
        this.AppName = name;
        this.StudyCollectionItem = studyCollectionItem;
    }

    public StudyCollectionItem StudyCollectionItem;

    public string AppName { get; private set; }

    public int MaxScreenConfigNumber { get; private set; }
    
    public void SetupStudies(StudyCollectionItem studyCollectionItem) {
        this.StudyCollectionItem = studyCollectionItem;
    }

    public void InvokePartialAt(int appSequence, int screenSequence) {
        Debug.WriteLine("AWAKE >>> " 
                        + ToString() 
                        + "'s " + appSequence
                        + " at " + screenSequence);
    }
    
    public void HidePartialAt(int appSequence, int screenSequence) {
        Debug.WriteLine("HIDE >>> " 
                        + ToString() 
                        + "'s " + appSequence
                        + " at " + screenSequence);
    }

    public void Kill() {
        Debug.WriteLine("KILL_APP >>>" +ToString());
    }

    public void HideAll() {
        Debug.WriteLine("HIDE_APP >>>" +ToString());
    }
    
    public override string ToString() {
        return this.AppName + " | " + this.StudyCollectionItem.ToString();
    }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppModel appModel = (AppModel)obj;
        return this.AppName.Equals(appModel.AppName) && this.StudyCollectionItem.Equals(appModel.StudyCollectionItem);
    }

    public override int GetHashCode() {
        return this.AppName.GetHashCode() + this.StudyCollectionItem.GetHashCode();
    }

    #endregion
}