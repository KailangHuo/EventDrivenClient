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
        StudyInstanceId = uid;
    }

    public Study(string one) {
        PatientName = one + "'s Name";
        PatientAge = one + "'s Age";
        PatientGender = one + "'s Gender";
        StudyInstanceId = one + "'s Uid";
    }

    public string PatientName { get; set; }
    
    public string PatientGender { get; set; }

    public string PatientAge { get; set; }
    public string StudyInstanceId { get; set; }
    

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        Study t = (Study)obj;
        return this.StudyInstanceId.Equals(t.StudyInstanceId);
    }

    public override int GetHashCode() {
        return this.PatientName.GetHashCode();
    }

    #endregion

    public override string ToString() {
        return this.PatientName;
    }
}