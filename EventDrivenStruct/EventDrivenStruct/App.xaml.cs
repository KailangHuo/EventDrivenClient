using System;
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
            
            var x = mainWindowViewModel.StudyContainerViewModel;
            var y = mainWindowViewModel.AppTabViewModel;
            
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();

            StudyCollectionItem laoWangColl = MakeItem("老王", 2);

            AppModel review2D = new AppModel("Review 2D");
            
            facade.AddStudyItemWithApp(laoWangColl, review2D); // 添加 老王 2d -> 成功
            
            facade.DeleteAppFromStudy(laoWangColl, review2D);

            StudyCollectionItem laoZhangColl = MakeItem("老张", 2);

            AppModel review3D = new AppModel("Review 3D");
            
            facade.AddStudyItemWithApp(laoZhangColl, review3D);// 添加 老张 3D -> 成功
            
            facade.AddStudyItemWithApp(laoWangColl, review2D);// 再次添加 老王 2d -> 不能添加应用
            
            facade.AddStudyItemWithApp(laoWangColl, review3D);// 添加 老王 3d -> 成功添加新的应用

            AppModel oncology = new AppModel("OnCology");

            facade.AddStudyItemWithApp(laoWangColl, oncology); // 添加老王 onco -> 成功添加应用
            
            facade.DeleteStudyItem(laoZhangColl);

            StudyCollectionItem laoZhangRedundant = new StudyCollectionItem();
            Study study = new Study("One" + " " + "老张");
            laoZhangRedundant.GetStudyComposition().Add(study);
            
            facade.AddStudyItemWithApp(laoZhangRedundant, review3D);// 添加 老王残 3d -> 不能
            facade.DeleteStudyItem(laoZhangRedundant);// 移除 老王残 -> 不能
            facade.DeleteStudyItem(laoWangColl);// 移除 老王 -> 成功
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