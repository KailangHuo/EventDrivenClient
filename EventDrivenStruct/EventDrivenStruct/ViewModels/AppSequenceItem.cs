using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class AppSequenceItem {

    public AppSequenceItem(StudyCollectionItem studyCollectionItem,AppItem_ViewModel appItemViewModel, int appSequenceNumber) {
        this.StudyCollectionItem = studyCollectionItem;
        this.AppItemViewModel = appItemViewModel;
        this.AppSequenceNumber = appSequenceNumber;
        this.ScreenSequenceNumber = -1;

    }

    public StudyCollectionItem StudyCollectionItem;

    public AppItem_ViewModel AppItemViewModel;

    public int AppSequenceNumber;

    public int ScreenSequenceNumber;

    public void SetScreenSequence(int number) {
        this.ScreenSequenceNumber = number;
    }

    public void OffSetScreenSequence() {
        this.ScreenSequenceNumber = -1;
    }

    public override bool Equals(object? obj) {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        AppSequenceItem appSequenceItem = (AppSequenceItem)obj;
        return StudyCollectionItem.Equals(appSequenceItem.StudyCollectionItem)
               && AppItemViewModel.Equals(appSequenceItem.AppItemViewModel)
               && AppSequenceNumber == appSequenceItem.AppSequenceNumber
               && ScreenSequenceNumber == appSequenceItem.ScreenSequenceNumber;
    }

    public override int GetHashCode() {
        return StudyCollectionItem.GetHashCode() 
               + AppItemViewModel.GetHashCode() 
               + AppSequenceNumber.GetHashCode() 
               + ScreenSequenceNumber.GetHashCode();
    }
}