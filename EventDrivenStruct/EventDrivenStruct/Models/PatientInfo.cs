using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class PatientInfo : AbstractEventDrivenObject{
    
    public string PatientName { get; set; }
    
    public string PatientGender { get; set; }

    public string PatientAge { get; set; }
    
    
    
}