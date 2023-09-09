using System.Collections.Generic;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ConfigurationLoader; 

public class TestStudyAppNode {

    public string patientName;

    public string patientGender;

    public string patientAge;

    public string studyUid;

    public string appName;

    public AppModel ConvertToAppModel() {
        Study study = new Study(patientName, patientGender, patientAge, studyUid);
        List<Study> studyList = new List<Study>();
        studyList.Add(study);
        StudyCollectionItem studyCollectionItem = new StudyCollectionItem(studyList);
        AppModel appModel = new AppModel(appName, studyCollectionItem);
        return appModel;
    }
    

}