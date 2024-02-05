namespace Integration;

public class Command {

    public string CommandName;

    public CommandTypeEnum CommandType;

    public CommandPriorityEnum CommandPriority;

    public string CommandParameterNames;

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