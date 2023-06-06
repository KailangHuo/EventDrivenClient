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
            UnitTest();
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

        private void UnitTest() {
            MainEntry_ModelFacade facade = MainEntry_ModelFacade.GetInstance();

            StudyCollectionItem laoWangColl = MakeItem("老王");

            AppModel review2D = new AppModel("Review 2D");
            
            facade.AddStudyItem(laoWangColl, review2D);

            StudyCollectionItem laoZhangColl = MakeItem("老张");

            AppModel review3D = new AppModel("Review 3D");
            
            facade.AddStudyItem(laoZhangColl, review3D);
            
            facade.AddStudyItem(laoWangColl, review2D);
            
            facade.AddStudyItem(laoWangColl, review3D);

            StudyCollectionItem laoZhangRedundant = new StudyCollectionItem();
            Study study = new Study("One" + " " + "老张");
            laoZhangRedundant.GetStudyComposition().Add(study);
            
            facade.AddStudyItem(laoZhangRedundant, review3D);
            facade.DeleteStudyItem(laoZhangRedundant);
        }

        private StudyCollectionItem MakeItem(string param1) {
            StudyCollectionItem studyCollectionItem = new StudyCollectionItem();

            Study studyOne = new Study("One" + " " + param1);
            Study studyTwo = new Study("Two"+ " " + param1);
            Study studyThree = new Study("Three"+ " " + param1);
            
            studyCollectionItem.AddInStudyComposition(studyOne);
            studyCollectionItem.AddInStudyComposition(studyTwo);
            studyCollectionItem.AddInStudyComposition(studyThree);

            return studyCollectionItem;
        }

    }
}