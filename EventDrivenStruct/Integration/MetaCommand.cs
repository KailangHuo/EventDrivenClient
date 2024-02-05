namespace Integration;

public class MetaCommand {

    public MetaCommand() {
        
    }

    public string CommandName;

    public CommandTypeEnum CommandType;

    public CommandPriorityEnum CommandPriority;

    public HashSet<string> CommandParamNameSet;

    public void SetCommandParameterNamesSet() {
    }

}

public enum CommandTypeEnum {
    WAKEUP,
    GENERIC,
    ERROR_DEFAULT,
}

public enum CommandPriorityEnum {
    NORMAL,
    HIGH,
    ERROR_DEFAULT,
}