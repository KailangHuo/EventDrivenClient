using System.Reflection.Emit;

namespace Integration;

public class DynamicCommandFactory {
    
    private static DynamicCommandFactory _instance;

    private DynamicCommandFactory() {
        
    }

    public static DynamicCommandFactory GetInstance() {
        if (_instance == null) {
            lock (typeof(DynamicCommandFactory)) {
                if (_instance == null) {
                    _instance = new DynamicCommandFactory();
                }
            }
        }

        return _instance;
    }

    public DynamicCommand CreateCommand(string[] args) {
        if (args.Length == 0) {
            ExceptionManager.GetInstance().ThrowExceptionAndExit("WRONG INPUT", 3);
        }

        DynamicCommand dynamicCommand = new DynamicCommand(args[0]);
        for (int i = 1; i < args.Length; i++) {
            string curStr = args[i];
            
            string paramName = "";
            int endIndex = 0;
            for (int j = 0; j < curStr.Length; j++) {
                if (string.IsNullOrEmpty(paramName) && curStr[j].ToString() == "-") continue;
                if (curStr[j].ToString() == "=") {
                    endIndex = j + 1;
                    break;
                }

                paramName += curStr[j];
            }

            if (string.IsNullOrEmpty(paramName)) {
                ExceptionManager.GetInstance().ThrowExceptionAndExit("WRONG INPUT!", 3);
            }

            string paramValue = curStr.Substring(endIndex, curStr.Length - endIndex);

            dynamicCommand.CommandParamsMap[paramName] = paramValue;

        }

        return dynamicCommand;
    }

}