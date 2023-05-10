using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class ExamAppMappingObj : AbstractEventDrivenObject {

    public ExamAppMappingObj() {
        AppList = new List<DiagnoApplication>();
    }

    public Exam Exam;

    public List<DiagnoApplication> AppList;

}