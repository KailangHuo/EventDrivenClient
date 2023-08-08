using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;
using EventDrivenStruct.ViewModels;

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
            MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();
            facade.RegisterObserver(mainWindowViewModel);

            
            GlobalContext.GetInstance().RegisterModelFacade(MainEntry_ModelFacade.GetInstance());
            Current.Dispatcher.BeginInvoke(() => {
                MainWindow mainWindow = new MainWindow();
                //MainWindow_ViewModel mainWindowViewModel = new MainWindow_ViewModel();
                
                GlobalContext.GetInstance().RegisterMainWindow(mainWindow);
                GlobalContext.GetInstance().RegisterMainWindowViewModel(mainWindowViewModel);
                
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.Show();
            });
            //UnitTest1(mainWindowViewModel);
        }

        public void UnitTest1(MainWindow_ViewModel mainWindowViewModel) {
            
            var studies = mainWindowViewModel.StudyContainerViewModel;
            var screen1 = mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer?.
                AppSequenceManagerCollection[0];
            var screen2 = mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer?.
                AppSequenceManagerCollection[1];
            
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();
            StudyCollectionItem laoWang = MakeItem("老王",1);
            StudyCollectionItem laoLi = MakeItem("老李", 3);


            AppModel laowang_review2D = new AppModel("Review2D", laoWang);
            AppModel laowang_maxTest = new AppModel("MAXTEST", laoWang);
            AppModel laownag_filming = new AppModel("Filming", laoWang);
            AppModel laoli_MMFusion = new AppModel("MMFusion", laoLi);
            
            AppModel review2d = new AppModel("Review2D");
            AppModel review3d = new AppModel("Review3D");
            AppModel mmfusion = new AppModel("MMFusion");
            AppModel filming = new AppModel("Filming");
            AppModel maxtest = new AppModel("MAXTEST");

            var screenManager = mainWindowViewModel.ScreenManagerViewModel.ScreenCollection;
                
            // facade添加一个Study并打开应用
            facade.AddStudyItemWithApp(laoWang, laowang_maxTest);
            
            // facade添加一个Study并打开应用
            facade.AddStudyItemWithApp(laoWang, laowang_review2D);
            
            // 在laowang的Study下切换应用
            mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[4].SelectToOpen(laownag_filming);
            mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[3].ChangedSelection(laowang_maxTest);
            mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[1].ChangedSelection(laowang_maxTest);
            
            // 在前端点击按钮关闭app -> 当前或者背景app
            AppModel appModel = mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer
                .AppSequenceManagerCollection[1].AppItemSelected.AppModel; // laowang_maxTest
            mainWindowViewModel.AppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[1].CloseApp(appModel);
            
            // 在facade关闭App -> 当前app或者背景app
            facade.DeleteApp(laowang_maxTest);
            
            // 在facade添加一条Study并打开对应的app
            facade.AddStudyItemWithApp(laoLi, laoli_MMFusion);

            // 在前端选择并切换Study
            mainWindowViewModel.StudyContainerViewModel.SelectedStudy = new Study_ViewModel(laoWang);
            
            // 在前端选择并切换Study
            mainWindowViewModel.StudyContainerViewModel.SelectedStudy = new Study_ViewModel(laoLi);
            
            // 在前端点击关闭Study -> 当前或者背景
            mainWindowViewModel.StudyContainerViewModel.StudyViewModels[0].CloseStudy();

            // 在facade关闭Study -> 当前和背景Study
            facade.DeleteStudyItem(laoWang);
            
        }

        private StudyCollectionItem MakeItem(string param1, int times) {
            StudyCollectionItem studyCollectionItem = new StudyCollectionItem();

            for (int i = 0; i < times; i++) {
                Study study = new Study(param1+ i + ". " );
                studyCollectionItem.AddInStudyComposition(study);
            }
            
            return studyCollectionItem;
        }
        

    }
}