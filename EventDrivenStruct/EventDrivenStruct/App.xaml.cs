using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EventDrivenElements;
using EventDrivenStruct.ViewModels;

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
            Current.Dispatcher.BeginInvoke(() => {
                MainWindow mainWindow = new MainWindow();
                MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
                GlobalContext.GetInstance().RegisterMainWindowViewModel(mainWindow);
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.Show();
            });
        }
        
    }
}