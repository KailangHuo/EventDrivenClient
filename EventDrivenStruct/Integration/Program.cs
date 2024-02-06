using System.Diagnostics;
using Integration;

public class Program {
    public static void Main(string[] args) {
        SystemFacade systemFacade = SystemFacade.GetInstance();
        DynamicCommand dynamicCommand = DynamicCommandFactory.GetInstance().CreateCommand(args);
        systemFacade.TakeCommand(dynamicCommand);
    }
}