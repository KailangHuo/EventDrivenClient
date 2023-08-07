using System.Diagnostics;
using EventDrivenElements;

namespace EventDrivenStruct.ViewModels; 

public class Screen_ViewModel : AbstractEventDrivenViewModel{

    public Screen_ViewModel(int index) {
        _screenIndex = index;
    }

    private int _screenIndex;
    
    private AppSequenceItem _content;

    public AppSequenceItem Content {
        get {
            return _content;
        }
        set {
            if (_content == value) {
                IgnoreAction();
                return;
            }

            HideContent();
            _content = value;
            ShowContent();
        }
    }

    private void IgnoreAction() {
        PrintToDebugConsole("IGNORED...");
    }

    private void HideContent() {
        if(_content == null) return;
        PrintToDebugConsole(Content.GetSequenceInformation() + " HIDE");
    }

    private void ShowContent() {
        if (_content == null) {
            PrintToDebugConsole("SHOW NULL");
            return;
        }
        PrintToDebugConsole(Content.GetSequenceInformation() + " SHOW");
    }
    
    public void TryUpdateContent(AppSequenceItem appSequenceItem) {
        Content = appSequenceItem;
    }

    public void PrintToDebugConsole(string str) {
        Debug.WriteLine(this + str);
    }

    public override string ToString() {
        return "[SCREEN " + _screenIndex + "]>>> ";
    }
}