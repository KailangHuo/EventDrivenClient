using System.Diagnostics;
using EventDrivenElements;

namespace EventDrivenStruct.ViewModels; 

public class ScreenContentObject_ViewModel : AbstractEventDrivenViewModel{


    public ScreenContentObject_ViewModel() {
        CurAppSequenceItem = null;
    }

    public AppSequenceItem CurAppSequenceItem;

    public int ScreenIndex;

    public void AwakeApp(AppSequenceItem appSequenceItem) {
        if (CurAppSequenceItem == null) Awake();
        
    }











    private void Awake() {
        Debug.WriteLine("AWAKE >>> " 
                        + CurAppSequenceItem.AppItemViewModel.AppName 
                        + "'s " + CurAppSequenceItem.AppSequenceNumber
                        + " at " + ScreenIndex);
    }

    private void Hide() {
        Debug.WriteLine("HIDE >>> " 
                        + CurAppSequenceItem.AppItemViewModel.AppName 
                        + "'s " + CurAppSequenceItem.AppSequenceNumber
                        + " at " + ScreenIndex);
    }

    private void Close() {
        Debug.WriteLine("CLOSE >>> " 
                        + CurAppSequenceItem.AppItemViewModel.AppName 
                        + "'s " + CurAppSequenceItem.AppSequenceNumber
                        + " at " + ScreenIndex);
    }

}