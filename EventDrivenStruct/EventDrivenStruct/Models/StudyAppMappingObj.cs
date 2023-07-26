using System.Collections.Generic;
using System.Linq;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyAppMappingObj : AbstractEventDrivenObject {

    public StudyAppMappingObj() {
        AppList = new List<AppModel>();
    }

    public StudyCollectionItem StudyCollectionItem;

    private List<AppModel> AppList;

    public void AddAppModel(AppModel appModel) {
        if (!this.AppList.Contains(appModel)) {
            this.AppList.Add(appModel); 
            PublishEvent(nameof(AddAppModel), appModel);
        }
    }

    public void RemoveAppModel(AppModel appModel) {
        if (this.AppList.Contains(appModel)) {
            appModel.Kill();
            this.AppList.Remove(appModel);
            PublishEvent(nameof(RemoveAppModel), appModel);
            if (AppList.Count == 0) AppListEmpty();
        }
    }

    public void Kill() {
        for (int i = 0; i < AppList.Count; i++) {
            AppList[i].Kill();
        }
    }

    public void AppListEmpty() {
        PublishEvent(nameof(AppListEmpty), this);
    }

}