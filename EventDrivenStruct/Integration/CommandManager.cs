namespace Integration;

public class CommandManager {

    private static CommandManager _instance;

    private CommandManager() {
        
    }

    public static CommandManager GetInstance() {
        if (_instance == null) {
            lock (typeof(CommandManager)) {
                if (_instance == null) {
                    _instance = new CommandManager();
                }
            }
        }

        return _instance;
    }
    
    
    
}