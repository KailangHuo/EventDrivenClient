using System.Diagnostics;
using EventDrivenElements;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class ScreenItem_ViewModel : AbstractEventDrivenViewModel{


    public ScreenItem_ViewModel(int screenIndex) {
        this.ScreenIndex = screenIndex;
        ScreenContentObject = null;
    }

    public ScreenContentObject ScreenContentObject;

    public int ScreenIndex;

    public void TrySwapContent(ScreenContentObject screenContentObject) {
        if (ScreenContentObject != null && ScreenContentObject.Equals(screenContentObject)) {
            NothigHappend();
            return;
        }
        SwapContent(screenContentObject);
    }

    public void CloseThis() {
        Close();
        ScreenContentObject = null;
    }

    private void SwapContent(ScreenContentObject screenContentObject) {
        if(ScreenContentObject?.AppSequenceItem?.AppItemViewModel != null) Hide();
        this.ScreenContentObject = screenContentObject;
        if (this.ScreenContentObject?.AppSequenceItem?.AppItemViewModel == null) {
            NothigHappend();
            return;
        }
        Awake();
    }
    
    private void Awake() {
        Debug.WriteLine("AWAKE >>> " 
                        + ScreenContentObject.AppSequenceItem.AppItemViewModel.AppName 
                        + "'s " + ScreenContentObject.AppSequenceItem.AppSequenceNumber
                        + " at " + ScreenIndex
                        + " with " + ScreenContentObject.StudyCollectionStrs.ToString());
    }

    private void Hide() {
        Debug.WriteLine("HIDE >>> " 
                        + ScreenContentObject.AppSequenceItem.AppItemViewModel.AppName 
                        + "'s " + ScreenContentObject.AppSequenceItem.AppSequenceNumber
                        + " at " + ScreenIndex
                        + " with " + ScreenContentObject.StudyCollectionStrs.ToString());
    }

    private void Close() {
        Debug.WriteLine("CLOSE >>> " 
                        + ScreenContentObject.AppSequenceItem.AppItemViewModel.AppName 
                        + "'s " + ScreenContentObject.AppSequenceItem.AppSequenceNumber
                        + " at " + ScreenIndex
                        + " with " + ScreenContentObject.StudyCollectionStrs.ToString());
    }
    
    private void NothigHappend() {
        Debug.WriteLine("Screen >>> " + ScreenIndex + " 未作出动作..");
    }

}