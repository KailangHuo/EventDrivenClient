using System.Windows.Input;
using EventDrivenElements;

namespace EventDrivenStruct.ViewModels; 

public class SystemInfoCollector : AbstractEventDrivenViewModel {
    
    private static SystemInfoCollector _instance;

    private SystemInfoCollector() {
        systemInfoNumber = 0;
        SetupCommands();
    }

    private void SetupCommands() {
        ClearCommand = new CommonCommand(Clear);
    }

    public static SystemInfoCollector GetInstance() {
        if (_instance == null) {
            lock (typeof(SystemInfoCollector)) {
                if (_instance == null) {
                    _instance = new SystemInfoCollector();
                }
            }
        }

        return _instance;
    }

    private int systemInfoNumber;

    private string _content;

    public string Content {
        get {
            return _content;
        }
        set {
            if(_content == value) return;
            _content = value;
            RisePropertyChanged(nameof(Content));
        }
    }

    #region COMMANDS

    public ICommand ClearCommand { get; private set; }

    #endregion

    #region COMMANDS_BINDING_METHODS

    public void Clear(object o = null) {
        Content = "";
        systemInfoNumber = 0;
    }

    #endregion
    

    public void AddToSystemInfo(string s) {
        Content += systemInfoNumber + " " +s + "\n";
        systemInfoNumber++;
    }
    
    

}