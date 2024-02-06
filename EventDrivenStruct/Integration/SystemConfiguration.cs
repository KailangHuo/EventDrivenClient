namespace Integration;

public class SystemConfiguration {
    private static SystemConfiguration _instance;

    private SystemConfiguration() {
        Init();
    }

    public static SystemConfiguration GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemConfiguration)) {
                if (_instance == null) {
                    _instance = new SystemConfiguration();
                }
            }
        }

        return _instance;
    }

    public string SystemDirectory { get; private set; }

    private void Init() {
        string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
        this.SystemDirectory = System.IO.Path.GetDirectoryName(assemblyLocation);
    }

}