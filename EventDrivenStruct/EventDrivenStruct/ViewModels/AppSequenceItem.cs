using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceItem {

    public AppSequenceItem(AppItem_ViewModel appItemViewModel, int appSequenceNumber) {
        this.AppItemViewModel = appItemViewModel;
        this.AppSequenceNumber = appSequenceNumber;
    }

    public AppItem_ViewModel AppItemViewModel { get; private set; }

    public int AppSequenceNumber{ get; private set; }

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