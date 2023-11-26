using System.Windows;
using EventDrivenElements;
using EventDrivenStruct.Models;
using EventDrivenStruct.Views;

namespace EventDrivenStruct.ViewModels; 

public class PopupManager : AbstractEventDrivenViewModel{
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

    public void MainWindow_AddWindowPopup(int screenIndex) {
        AddStudyWindow addExamWindow = new AddStudyWindow();
        AddStudyWindow_ViewModel addStudyWindowViewModel = new AddStudyWindow_ViewModel();
        addExamWindow.DataContext = addStudyWindowViewModel;
        addExamWindow.Owner = GlobalContext.GetInstance().GetWindowOwnerByIndex(screenIndex);
        Application.Current.Dispatcher.BeginInvoke(() => {
            addExamWindow.ShowDialog();
        });
    }

    public void MainWindow_AddAppWindowPopup(AppTab_ViewModel appTabViewModel, int screenIndex) {
        AddAppWindow addAppWindow = new AddAppWindow();
        AddAppWindow_ViewModel addAppWindowViewModel = new AddAppWindow_ViewModel(appTabViewModel, screenIndex);
        addAppWindow.DataContext = addAppWindowViewModel;
        addAppWindow.Owner = GlobalContext.GetInstance().GetWindowOwnerByIndex(screenIndex);
        Application.Current.Dispatcher.BeginInvoke(() => {
            addAppWindow.ShowDialog();
        });
    }

    public void MainWindow_AppendWindowPopup(AppTab_ViewModel appTabViewModel, int screenIndex) {
        if (appTabViewModel.CurrentSelectedStudyCollectionItem == null) {
            ExceptionManager.GetInstance().ThrowAsyncException("No Study to Append to!");
            return;
        }

        AppendStudyWindow appendStudyWindow = new AppendStudyWindow();
        AppendStudyWindow_ViewModel addStudyWindowViewModel = new AppendStudyWindow_ViewModel(appTabViewModel);
        appendStudyWindow.DataContext = addStudyWindowViewModel;
        appendStudyWindow.Owner = GlobalContext.GetInstance().GetWindowOwnerByIndex(screenIndex);
        Application.Current.Dispatcher.BeginInvoke(() => {
            appendStudyWindow.ShowDialog();
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