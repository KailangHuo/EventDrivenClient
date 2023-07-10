using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppTitleSequenceItem {

    public AppTitleSequenceItem(AppTitleItem_ViewModel appTitleItemViewModel, int appSequenceNumber) {
        this.AppTitleItemViewModel = appTitleItemViewModel;
        this.AppSequenceNumber = appSequenceNumber;
    }

    public AppTitleItem_ViewModel AppTitleItemViewModel { get; private set; }

    public int AppSequenceNumber{ get; private set; }

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppTitleSequenceItem appTitleSequenceItem = (AppTitleSequenceItem)obj;
        return this.AppTitleItemViewModel.Equals(appTitleSequenceItem.AppTitleItemViewModel)
               && this.AppSequenceNumber == appTitleSequenceItem.AppSequenceNumber;
    }

    public override int GetHashCode() {
        return (AppTitleItemViewModel.ToString() + AppSequenceNumber.ToString() + "").GetHashCode();
    }
}