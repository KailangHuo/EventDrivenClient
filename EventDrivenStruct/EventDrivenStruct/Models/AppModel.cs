using System.Diagnostics;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.Models; 

public class AppModel : AbstractEventDrivenObject{

    public AppModel() {
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
    }

    public AppModel(string name) {
        AppName = name;
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
    }

    public AppModel(string name, StudyCollectionItem studyCollectionItem) {
        AppName = name;
        StudyCollectionItem = studyCollectionItem;
        MaxScreenConfigNumber = SystemConfiguration.GetInstance().GetAppConfigInfo(AppName).MaxConfigScreenNumber;
    }

    public string AppName { get; private set; }

    public StudyCollectionItem StudyCollectionItem;

    public int MaxScreenConfigNumber { get; private set; }

    public void AwakeAt(int appIndex, int screenIndex) {
        Debug.WriteLine("AWAKE => AppName: " + AppName + ": " + appIndex + " | Studies: " +
                        StudyCollectionItem.GetStudyUidComposition()
                        + " | Screen: " + screenIndex);
    }

    public void Sleep() {
        Debug.WriteLine("SLEEP => " + "AppName: " + AppName +  " | Studies: " + StudyCollectionItem.GetStudyUidComposition());
    }

    public void Close() {
        Debug.WriteLine("CLOSE => " + "AppName: " + AppName +  " | Studies: " + StudyCollectionItem.GetStudyUidComposition());
    }


    public override string ToString() {
        return AppName;
    }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppModel appModel = (AppModel)obj;
        return this.AppName.Equals(appModel.AppName);
    }

    public override int GetHashCode() {
        return this.AppName.GetHashCode();
    }

    #endregion
}