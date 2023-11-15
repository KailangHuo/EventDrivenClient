using System.Collections.Generic;
using System.Configuration.Internal;
using System.Windows;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;
using EventDrivenStruct.ViewModels;
using EventDrivenStruct.Views;

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

    public void SetupSystemScreens() {
        int screenNum = SystemConfiguration.GetInstance().GetScreenNumber();
        if(screenNum < 1) ExceptionManager.GetInstance().ThrowAsyncException("Window number less than 0");
        for (int i = 0; i < screenNum; i++) {
            Window window = new MainWindowView();
            GlobalContext.GetInstance().RegisterSubWindow(window);
            window.DataContext = new ConcreteWindowViewModel(this.MainViewModel, i);
        }
    }

    public static void Init() {
        GlobalContext.GetInstance();
    }

    private List<Window> SubWindowList;

    public MainViewModel MainViewModel { get; private set; }

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

    public void RegisterMainWindowViewModel(MainViewModel mainViewModel) {
        this.MainViewModel = mainViewModel;
        BuildObservantsRelationship();
    }

    public void RegisterModelFacade(MainEntry_ModelFacade mainEntryModelFacade) {
        this.MainEntryModelFacade = mainEntryModelFacade;
        BuildObservantsRelationship();
    }

    private void BuildObservantsRelationship() {
        if(MainViewModel != null && MainEntryModelFacade != null) MainEntryModelFacade.RegisterObserver(MainViewModel);
    }

}