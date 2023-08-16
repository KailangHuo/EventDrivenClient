using System.Diagnostics;
using EventDrivenElements;

namespace EventDrivenStruct.ViewModels; 

public class Screen_ViewModel : AbstractEventDrivenViewModel{

    public Screen_ViewModel(int index) {
        _screenIndex = index;
        ShowContent();
    }

    private int _screenIndex;

    private string _studyText;

    public string StudyText {
        get {
            return _studyText;
        }
        set {
            if (_studyText == value) return;
            _studyText = value;
            RisePropertyChanged(nameof(StudyText));
        }
    }



    private string _appText;

    public string AppText {
        get {
            return _appText;
        }
        set {
            if(_appText == value) return;
            _appText = value;
            RisePropertyChanged(nameof(AppText));
        }
    }


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
        AppText = "NULL";
        StudyText = "";
        PrintToDebugConsole( " HIDE" + Content.GetSequenceInformation() );
    }

    private void ShowContent() {
        if (_content == null) {
            AppText = "NULL";
            StudyText = "";
            PrintToDebugConsole("SHOW NULL");
            return;
        }

        AppText = _content.AppItemViewModel.AppName + " | " + _content.AppSequenceNumber;
        StudyText = _content.AppItemViewModel.AppModel.StudyCollectionItem.ToString();
        PrintToDebugConsole(" SHOW" + Content.GetSequenceInformation()  );
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