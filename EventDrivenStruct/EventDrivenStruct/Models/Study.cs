using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class Study : AbstractEventDrivenObject{

    public Study() {
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