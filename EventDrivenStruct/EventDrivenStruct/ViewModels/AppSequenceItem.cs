using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceItem {

    public AppSequenceItem(AppItem_ViewModel appItemViewModel, int appSequenceNumber) {
        this.AppItemViewModel = appItemViewModel;
        this.AppSequenceNumber = appSequenceNumber;
    }

    private int ScreenSeqNumber;

    public AppItem_ViewModel AppItemViewModel { get; private set; }

    public int AppSequenceNumber{ get; private set; }

    public void SetScreenSeqNumber(int number) {
        this.ScreenSeqNumber = number;
    }

    public void ResetScreenSeqNumber() {
        SetScreenSeqNumber(-1);
    }

    public void InvokeApp() {
        if(ScreenSeqNumber > -1) this.AppItemViewModel.AppModel.InvokePartialAt(AppSequenceNumber, ScreenSeqNumber);
    }

    public void HideApp() {
        this.AppItemViewModel.AppModel.HidePartialAt(AppSequenceNumber, ScreenSeqNumber);
    }

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppSequenceItem appSequenceItem = (AppSequenceItem)obj;
        return this.AppItemViewModel.Equals(appSequenceItem.AppItemViewModel)
               && this.AppSequenceNumber == appSequenceItem.AppSequenceNumber;
    }

    public override int GetHashCode() {
        return (AppItemViewModel.ToString() + AppSequenceNumber.ToString() + "").GetHashCode();
    }
}