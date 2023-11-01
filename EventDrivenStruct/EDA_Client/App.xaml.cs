using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;
using EventDrivenStruct.ViewModels;
using EventDrivenStruct.Views;

/**
 * 原则:
 * 1. 真实更新发生后, 必须立刻广播更新事件, 这两者是原子动作
 *
 * 优势
 * 1. 可以支持前所未有的多屏处理
 * 2. 及其简洁的插拔屏幕逻辑处理
 */


namespace EventDrivenStruct {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        
        protected override void OnExit(ExitEventArgs e) {
            Application.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
            Environment.Exit(0);
        }

        protected override void OnStartup(StartupEventArgs e) {
            /*TestWindowRun();
            return;*/
            
            MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
            GlobalContext.GetInstance().RegisterMainWindowViewModel(mainWindowViewModel);
            GlobalContext.GetInstance().RegisterModelFacade(MainEntry_ModelFacade.GetInstance());
            
            int screenNum = SystemConfiguration.GetInstance().GetScreenNumber();
                
            for (int i = 0; i < screenNum; i++) {
                Window window = new MainWindowView(i);
                if(i == 0) GlobalContext.GetInstance().RegisterMainWindow(window);
                else {
                    GlobalContext.GetInstance().RegisterSubWindow(window);
                }
                window.DataContext = mainWindowViewModel;
            }

            GlobalContext.GetInstance().ShowDialogs();
            
            /*Current.Dispatcher.BeginInvoke(() => {
               
                Window mainWindow = SystemConfiguration.GetInstance().GetScreenNumber() == 5
                    ? new MainWindow_Five()
                    : new MainWindowView();
                
                GlobalContext.GetInstance().RegisterMainWindow(mainWindow);
                GlobalContext.GetInstance().RegisterMainWindowViewModel(mainWindowViewModel);
                
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.Show();
                
            });*/
            //UnitTest1(mainWindowViewModel);
        }

    }
}