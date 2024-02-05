namespace Integration;

public class ExceptionManager {
    
    private static ExceptionManager _instance;

    private ExceptionManager() {
        
    }

    public static ExceptionManager GetInstance() {
        if (_instance == null) {
            lock (typeof(ExceptionManager)) {
                if (_instance == null) {
                    _instance = new ExceptionManager();
                }
            }
        }

        return _instance;
    }

    public void ThrowExceptionAndExit(string content, int exitCode) {
        Console.WriteLine(content);
        Environment.Exit(exitCode);
    }
}