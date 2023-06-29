namespace EventDrivenStruct.ViewModels; 

public class AppSequenceItem {

    public AppSequenceItem(AppItem_ViewModel appItemViewModel, int appSequenceNumber) {
        this.AppItemViewModel = appItemViewModel;
        this.AppSequenceNumber = appSequenceNumber;
    }

    public AppItem_ViewModel AppItemViewModel;

    public int AppSequenceNumber;

}