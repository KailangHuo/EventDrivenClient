namespace Integration;

public class MetaCommand {

    public MetaCommand() {
        this.CommandParamNameSet = new HashSet<string>();
    }

    public string CommandName;

    public CommandTypeEnum CommandType;

    public CommandPriorityEnum CommandPriority;

    public HashSet<string> CommandParamNameSet;

    public void SetCommandParameterNamesSet(string str) {
        string[] strs = str.Split(",");
        for (int i = 0; i < strs.Length; i++) {
            this.CommandParamNameSet.Add(strs[i]);
        }
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