using System.Dynamic;

namespace Integration;

public class DynamicCommand {

    public DynamicCommand(string commandName) {
        this.CommandName = commandName;
        this.CommandParamsMap = new Dictionary<string, string>();
    }

    public string CommandName;

    public Dictionary<string, string> CommandParamsMap;

}