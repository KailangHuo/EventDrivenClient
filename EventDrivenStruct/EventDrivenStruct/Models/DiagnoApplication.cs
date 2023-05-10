using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class DiagnoApplication : AbstractEventDrivenObject{
    
    public string ApplicationType;

    public string Content;
    
    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        DiagnoApplication t = (DiagnoApplication)obj;
        return this.ApplicationType.Equals(t.ApplicationType);
    }

    public override int GetHashCode() {
        return this.ApplicationType.GetHashCode();
    }

    #endregion
}