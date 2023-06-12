﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EventDrivenElements;
using EventDrivenStruct.Models;
using EventDrivenStruct.ViewModels;

/**
 * 原则:
 * 1. 真实更新发生后, 必须立刻广播更新事件, 这两者是原子动作
 *
 * 优势
 * 1. 可以支持前所未有的多屏处理
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
            MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();
            facade.RegisterObserver(mainWindowViewModel);
            
            UnitTest1(mainWindowViewModel);
            /*GlobalContext.GetInstance().RegisterModelFacade(MainEntry_ModelFacade.GetInstance());
            Current.Dispatcher.BeginInvoke(() => {
                MainWindow mainWindow = new MainWindow();
                MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
                
                GlobalContext.GetInstance().RegisterMainWindow(mainWindow);
                GlobalContext.GetInstance().RegisterMainWindowViewModel(mainWindowViewModel);
                
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.Show();
            });*/
        }

        private void UnitTest1(MainWindow_ViewModel mainWindowViewModel) {
            
            var studies = mainWindowViewModel.StudyContainerViewModel;
            var screen1 = mainWindowViewModel.AppTabViewModel.SelectedAppContainer?.
                AppSequenceManagerCollection[0];
            var screen2 = mainWindowViewModel.AppTabViewModel.SelectedAppContainer?.
                AppSequenceManagerCollection[1];
            
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();
            AppModel review2d = new AppModel("Review2D");
            AppModel review3d = new AppModel("Review3D");
            AppModel mmfusion = new AppModel("MMFusion");
            AppModel filming = new AppModel("Filming");

            StudyCollectionItem laoWang = MakeItem("老王",2);
            StudyCollectionItem laoLi = MakeItem("老李", 3);
                
            
            facade.AddStudyItemWithApp(laoWang, review2d);
            facade.AddStudyItemWithApp(laoWang, mmfusion);


            mainWindowViewModel.AppTabViewModel.SelectedAppContainer.AppSequenceManagerCollection[0].ChangedSelection(new AdvancedApp_ViewModel(review2d));
            AppModel oncology = new AppModel("Oncology");
            facade.AddStudyItemWithApp(laoWang, oncology);

        }

        private StudyCollectionItem MakeItem(string param1, int times) {
            StudyCollectionItem studyCollectionItem = new StudyCollectionItem();

            for (int i = 0; i < times; i++) {
                Study study = new Study(i + ". " + param1);
                studyCollectionItem.AddInStudyComposition(study);
            }
            
            return studyCollectionItem;
        }
        

    }
}