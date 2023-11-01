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
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();
            
            //会在后续的UI线程发生
            //facade.RegisterObserver(mainWindowViewModel);

            
            GlobalContext.GetInstance().RegisterModelFacade(MainEntry_ModelFacade.GetInstance());
            Current.Dispatcher.BeginInvoke(() => {
                Window mainWindow = SystemConfiguration.GetInstance().GetScreenNumber() == 5
                    ? new MainWindow_Five()
                    : new MainWindowView();
                //MainWindow_Five mainWindow = new MainWindow_Five();
                //MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
                
                GlobalContext.GetInstance().RegisterMainWindow(mainWindow);
                GlobalContext.GetInstance().RegisterMainWindowViewModel(mainWindowViewModel);
                
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.Show();
                //接下来, 将PA按钮添加到每一个屏幕上, 然后提供双屏的参考,
            });
            //UnitTest1(mainWindowViewModel);
        }

    }
}