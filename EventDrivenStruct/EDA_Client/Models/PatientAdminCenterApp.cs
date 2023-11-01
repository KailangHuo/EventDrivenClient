namespace EventDrivenStruct.Models; 

public class PatientAdminCenterApp : AppModel{
    
    private static PatientAdminCenterApp _instance;

    private PatientAdminCenterApp(string name) : base(name) {
        Content = "PA_Content";
    }

    public static PatientAdminCenterApp GetInstance() {
        if (_instance == null) {
            lock (typeof(PatientAdminCenterApp)) {
                if (_instance == null) {
                    _instance = new PatientAdminCenterApp("PatientAdmin");
                }
            }
        }

        return _instance;
    }
    

    public string Content;

    public override string ToString() {
        return this.AppName ;
    }
    
    #region HASH_AND_EQUALS

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        PatientAdminCenterApp PaAppModel = (PatientAdminCenterApp)obj;
        return this.AppName.Equals(PaAppModel.AppName) && this.Content.Equals(PaAppModel.Content);
    }

    public override int GetHashCode() {
        return this.AppName.GetHashCode() + this.Content.GetHashCode();
    }

    #endregion
    
}