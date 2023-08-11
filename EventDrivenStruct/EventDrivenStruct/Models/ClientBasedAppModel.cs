using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class ClientBasedAppModel : AbstractEventDrivenObject{

    public ClientBasedAppModel(string appName) {
        this.AppName = appName;
        
    }

    public string AppName { get; private set; }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        ClientBasedAppModel clientBasedAppModel = (ClientBasedAppModel)obj;
        return this.AppName.Equals(clientBasedAppModel.AppName);
    }

    public override int GetHashCode() {
        return this.AppName.GetHashCode();
    }

    #endregion
    
    
}