using EventDrivenElements;

namespace EventDrivenStruct.Models; 

public class AppModel : AbstractEventDrivenObject{

    public AppModel() {
        
    }

    public AppModel(string name) {
        AppName = name;
    }

    public string AppName { get; private set; }

    public override string ToString() {
        return AppName;
    }

    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppModel appModel = (AppModel)obj;
        return this.AppName.Equals(appModel.AppName);
    }

    public override int GetHashCode() {
        return this.AppName.GetHashCode();
    }

    #endregion
}