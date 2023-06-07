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
        if(!this.AppList.Contains(appModel))this.AppList.Add(appModel);
    }

    public void RemoveAppModel(AppModel appModel) {
        if(this.AppList.Contains(appModel))this.AppList.Remove(appModel);
    }

}