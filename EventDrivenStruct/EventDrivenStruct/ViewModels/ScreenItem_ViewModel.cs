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
        if(ScreenContentObject != null) Hide();
        
        if (string.IsNullOrEmpty(screenContentObject.AppName) || (ScreenContentObject != null &&ScreenContentObject.Equals(screenContentObject))) {
            NothigHappend();
            return;
        }
        
        if (!string.IsNullOrEmpty(screenContentObject.AppName) ) {
            this.ScreenContentObject = screenContentObject;
        }

        Awake();
    }
    
    private void Awake() {
        Debug.WriteLine("AWAKE >>> " 
                        + ScreenContentObject.AppName 
                        + "'s " + ScreenContentObject.AppSeqNumber
                        + " at " + ScreenIndex
                        + " with " + ScreenContentObject.StudyCollectionSet.ToString());
    }

    private void Hide() {
        Debug.WriteLine("HIDE >>> " 
                        + ScreenContentObject.AppName 
                        + "'s " + ScreenContentObject.AppSeqNumber
                        + " at " + ScreenIndex
                        + " with " + ScreenContentObject.StudyCollectionSet.ToString());
    }

    private void Close() {
        Debug.WriteLine("CLOSE >>> " 
                        + ScreenContentObject.AppName 
                        + "'s " + ScreenContentObject.AppSeqNumber
                        + " at " + ScreenIndex
                        + " with " + ScreenContentObject.StudyCollectionSet.ToString());
    }
    
    private void NothigHappend() {
        Debug.WriteLine("Screen >>> " + ScreenIndex + " 未作出动作..");
    }

}