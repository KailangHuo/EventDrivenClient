using System.Collections.Generic;
using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class Exam : AbstractEventDrivenObject{

    public Exam() {
        PatientInfo = new PatientInfo();
        ApplicationContainer = new ApplicationContainer();
    }

    public PatientInfo PatientInfo { get; set; }

    public string ExamInstanceId { get; set; }

    public ApplicationContainer ApplicationContainer { get; set; }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        Exam t = (Exam)obj;
        return this.ExamInstanceId.Equals(t.ExamInstanceId);
    }

    public override int GetHashCode() {
        return this.ExamInstanceId.GetHashCode();
    }

    #endregion
}