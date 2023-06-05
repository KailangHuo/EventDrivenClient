using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class StudyAppMappingObj : AbstractEventDrivenObject {

    public StudyAppMappingObj() {

    }

    public Study Study;

    public List<AppModel> AppList;

}