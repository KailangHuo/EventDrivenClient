using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTitleSequenceItem {

    public AppTitleSequenceItem(AppItem_ViewModel appItemViewModel, int appSequenceNumber) {
        this.AppItemViewModel = appItemViewModel;
        this.AppSequenceNumber = appSequenceNumber;
    }

    public AppItem_ViewModel AppItemViewModel { get; private set; }

    public int AppSequenceNumber{ get; private set; }

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppTitleSequenceItem appTitleSequenceItem = (AppTitleSequenceItem)obj;
        return this.AppItemViewModel.Equals(appTitleSequenceItem.AppItemViewModel)
               && this.AppSequenceNumber == appTitleSequenceItem.AppSequenceNumber;
    }

    public override int GetHashCode() {
        return (AppItemViewModel.ToString() + AppSequenceNumber.ToString() + "").GetHashCode();
    }
}