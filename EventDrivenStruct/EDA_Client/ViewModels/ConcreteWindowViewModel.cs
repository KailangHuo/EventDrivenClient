using EventDrivenElements;

namespace EventDrivenStruct.ViewModels; 

public class ConcreteWindowViewModel : AbstractEventDrivenViewModel{

    public ConcreteWindowViewModel(MainWindow_ViewModel mainWindowViewModel, int decoIndex) {
        this.MainWindowViewModel = mainWindowViewModel;
        this.ScreenIndex = decoIndex;
        this.ScreenName = "屏幕 " + ScreenIndex;
        this.SelectedAppSequenceManager = null;
        mainWindowViewModel.AppTabViewModel.RegisterObserver(this);
        this.SelectedScreenViewModel = mainWindowViewModel.ScreenManagerViewModel.ScreenCollection[ScreenIndex];
    }

    public int ScreenIndex { get; set; }

    public string ScreenName { get; set; }

    public MainWindow_ViewModel MainWindowViewModel { get; set; }

    public AppSequenceManager_ViewModel SelectedAppSequenceManager { get; set; }

    public Screen_ViewModel SelectedScreenViewModel { get; set; }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(AppTab_ViewModel.SelectedAppItemContainer))) {
            //TODO: 选取后重选会导致选中有问题
            this.SelectedAppSequenceManager
                = this.MainWindowViewModel.AppTabViewModel.SelectedAppItemContainer.AppSequenceManagerCollection[
                    ScreenIndex];
            RisePropertyChanged(nameof(SelectedAppSequenceManager));
            return;
        }

    }
}