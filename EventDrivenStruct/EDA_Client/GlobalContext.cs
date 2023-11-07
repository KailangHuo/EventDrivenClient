using System.Collections.Generic;
using System.Configuration.Internal;
using System.Windows;
using EventDrivenStruct.Models;
using EventDrivenStruct.ViewModels;

namespace EventDrivenStruct; 

public class GlobalContext {
    private static GlobalContext _globalContext ;

    private GlobalContext() {
        initSystem();
    }

    private void initSystem() {
        ExceptionManager.GetInstance().RegisterObserver(PopupManager.GetInstance());
        SubWindowList = new List<Window>();
    }

    public static GlobalContext GetInstance() {
        if (_globalContext == null) {
            lock (typeof(GlobalContext)) {
                if (_globalContext == null) {
                    _globalContext = new GlobalContext();
                }
            }
        }

        return _globalContext;
    }

    private List<Window> SubWindowList;

    public MainWindow_ViewModel MainWindowViewModel { get; private set; }

    public MainEntry_ModelFacade MainEntryModelFacade { get; private set; }

    public Window GetWindowOwnerByIndex(int screenNumber) {
        return this.SubWindowList[screenNumber];
    }

    public void ShowDialogs() {
        Application.Current.Dispatcher.BeginInvoke(() => {
            for (int i = 0; i < this.SubWindowList.Count; i++) {
                SubWindowList[i].Show();
            }
        });
    }
    

    public void RegisterSubWindow(Window subWindow) {
        this.SubWindowList.Add(subWindow);
    }

    public void RegisterMainWindowViewModel(MainWindow_ViewModel mainWindowViewModel) {
        this.MainWindowViewModel = mainWindowViewModel;
        BuildObservantsRelationship();
    }

    public void RegisterModelFacade(MainEntry_ModelFacade mainEntryModelFacade) {
        this.MainEntryModelFacade = mainEntryModelFacade;
        BuildObservantsRelationship();
    }

    private void BuildObservantsRelationship() {
        if(MainWindowViewModel != null && MainEntryModelFacade != null) MainEntryModelFacade.RegisterObserver(MainWindowViewModel);
    }

}