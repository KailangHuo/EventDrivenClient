namespace Integration;

public class SystemFacade {
    
    private static SystemFacade _instance;

    private SystemFacade() {
        this._metaCommandManager = MetaCommandManager.GetInstance();
    }

    public static SystemFacade GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemFacade)) {
                if (_instance == null) {
                    _instance = new SystemFacade();
                }
            }
        }

        return _instance;
    }

    private MetaCommandManager _metaCommandManager;

    public void TakeCommand(string[] args) {
        
    }

}