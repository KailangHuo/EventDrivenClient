namespace Integration;

public class SystemFacade {
    
    private static SystemFacade _instance;

    private SystemFacade() {
        this._metaCommandManager = MetaCommandManager.GetInstance();
        this.CommandExecutor = new CommandExecutor();
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

    private const string MutexName = "CLIENT_MUTEX_NAME_STRING";

    private MetaCommandManager _metaCommandManager;

    private CommandExecutor CommandExecutor;

    public void TakeCommand(DynamicCommand command) {
        MetaCommand meta = _metaCommandManager.GetMetaCommand(command.CommandName);
        if ( meta == null) {
            ExceptionManager.GetInstance().ThrowExceptionAndExit("WRONG INPUT!", 3);
        }

        if (meta.CommandPriority == CommandPriorityEnum.HIGH) {
            CommandExecutor.InvokeCommand(command);
        }

        bool creatNew;
        Mutex mutex = new Mutex(true, MutexName, out creatNew);
        if (!creatNew) {
            ExceptionManager.GetInstance().ThrowExceptionAndExit("THE LAST COMMAND IS STILL PROCESSING!", 6);
        }

        if (meta.CommandType == CommandTypeEnum.WAKEUP) {
            CommandExecutor.CallupSoftwareSync(command);
        }

        CommandExecutor.InvokeCommand(command);
    }

}