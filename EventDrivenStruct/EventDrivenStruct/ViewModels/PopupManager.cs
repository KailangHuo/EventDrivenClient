using System.Windows;
using EventDrivenElements;

namespace EventDrivenStruct.ViewModels; 

public class PopupManager : AbstractEventDrivenObject{
    private static PopupManager _popupManager ;
    private PopupManager() { }

    public static PopupManager GetInstance() {
        if (_popupManager == null) {
            lock (typeof(PopupManager)) {
                if (_popupManager == null) {
                    _popupManager = new PopupManager();
                }
            }
        }

        return _popupManager;
    }

    private void GenerateMainSec_AddWindow() {
        AddExamWindow addExamWindow = new AddExamWindow();
        AddExamWindow_ViewModel addExamWindowViewModel = new AddExamWindow_ViewModel();
        addExamWindow.Owner = GlobalContext.GetInstance().MainWindow;
        Application.Current.Dispatcher.Invoke(() => {
            addExamWindow.ShowDialog();
        });
    }
    

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(MainWindow_ViewModel.AddExam))) {
            GenerateMainSec_AddWindow();
        }
    }
}