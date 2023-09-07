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

    public void MainWindow_AddAppWindowPopup(AppSequenceManager_ViewModel appSequenceManagerViewModel) {
        AddAppWindow addAppWindow = new AddAppWindow();
        AddAppWindow_ViewModel addAppWindowViewModel = new AddAppWindow_ViewModel(appSequenceManagerViewModel);
        addAppWindow.DataContext = addAppWindowViewModel;
        addAppWindow.Owner = GlobalContext.GetInstance().MainWindow;
        Application.Current.Dispatcher.BeginInvoke(() => {
            addAppWindow.ShowDialog();
        });
    }

}