using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class Study : AbstractEventDrivenObject{

    public Study() {
    }

    public Study(string name, string gender, string age, string uid) {
        PatientName = name;
        PatientAge = age;
        PatientGender = gender;
        ExamInstanceId = uid;
    }

    public Study(string one) {
        PatientName = one + "'s Name";
        PatientAge = one + "'s Age";
        PatientGender = one + "'s Gender";
        ExamInstanceId = one + "'s Uid";
    }

    public string PatientName { get; set; }
    
    public string PatientGender { get; set; }

    public string PatientAge { get; set; }
    public string ExamInstanceId { get; set; }
    

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        Study t = (Study)obj;
        return this.ExamInstanceId.Equals(t.ExamInstanceId);
    }

    public override int GetHashCode() {
        return this.ExamInstanceId.GetHashCode();
    }

    #endregion
}