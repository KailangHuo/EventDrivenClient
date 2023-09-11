using System.Windows;
using EventDrivenElements;
using EventDrivenStruct.Models;
using EventDrivenStruct.Views;

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

    public void MainWindow_AddWindowPopup() {
        AddExamWindow addExamWindow = new AddExamWindow();
        AddExamWindow_ViewModel addExamWindowViewModel = new AddExamWindow_ViewModel();
        addExamWindow.DataContext = addExamWindowViewModel;
        addExamWindow.Owner = GlobalContext.GetInstance().MainWindow;
        Application.Current.Dispatcher.BeginInvoke(() => {
            addExamWindow.ShowDialog();
        });
    }

    public void MainWindow_AddAppWindowPopup(AppTab_ViewModel appTabViewModel, int screenNumber) {
        AddAppWindow addAppWindow = new AddAppWindow();
        AddAppWindow_ViewModel addAppWindowViewModel = new AddAppWindow_ViewModel(appTabViewModel, screenNumber);
        addAppWindow.DataContext = addAppWindowViewModel;
        addAppWindow.Owner = GlobalContext.GetInstance().MainWindow;
        Application.Current.Dispatcher.BeginInvoke(() => {
            addAppWindow.ShowDialog();
        });
    }

    public void AsyncPopupWindow(string content) {
        MessageBox.Show(content);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(ExceptionManager.ThrowAsyncException))) {
            string content = (string)o;
            AsyncPopupWindow(content);
            return;
        }
    }
}