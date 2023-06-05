using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyAppMappingObj : AbstractEventDrivenObject {

    public StudyAppMappingObj() {

    }

    public StudyCollectionItem StudyCollectionItem;

    public List<AppModel> AppList;

}