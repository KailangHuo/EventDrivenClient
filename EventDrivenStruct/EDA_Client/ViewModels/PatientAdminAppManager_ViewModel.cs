using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class PatientAdminAppManager_ViewModel : AbstractEventDrivenViewModel{

    public PatientAdminAppManager_ViewModel() {
        CurrentPaCenterNumber = 0;
        initPaSeqList();
    }

    private void initPaSeqList() {
        _paAppSequenceItems = new List<AppSequenceItem>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            _paAppSequenceItems.Add(null);
        }
    }

    private PatientAdminCenterApp_ViewModel PatientAdminCenterAppViewModel;

    private List<AppSequenceItem> _paAppSequenceItems;

    private int currentPaCenterNumber;

    public int CurrentPaCenterNumber {
        get {
            return currentPaCenterNumber;
        }
        set {
            if(currentPaCenterNumber == value) return;
            currentPaCenterNumber = value;
        }
    }

    private bool _isInvoked;

    public bool IsInvoked {
        get {
            return _isInvoked;
        }
        set {
            if(_isInvoked == value) return;
            _isInvoked = value;
        }
    }

    public void InvokePaAt(int number) {
        if(number >= SystemConfiguration.GetInstance().GetScreenNumber() || number < 0) return;
        if(_isInvoked && currentPaCenterNumber == number) return;
        _paAppSequenceItems[currentPaCenterNumber] = null;
        CurrentPaCenterNumber = number;
        _paAppSequenceItems[currentPaCenterNumber] = new AppSequenceItem(PatientAdminCenterAppViewModel, 0);
        IsInvoked = true;
        PaSelectionFinished();
    }

    public void InitPaCenter(PatientAdminCenterApp_ViewModel patientAdminCenterAppViewModel) {
        this.PatientAdminCenterAppViewModel = patientAdminCenterAppViewModel;
        InvokePaAt(0);
    }

    public void PaSelectionFinished() {
        PublishEvent(nameof(PaSelectionFinished), _paAppSequenceItems);
    }

    private void HidePa() {
        _paAppSequenceItems[CurrentPaCenterNumber] = null;
        CurrentPaCenterNumber = 0;
        IsInvoked = false;
    }

    public override void UpdateByEvent(string propertyName, object o) {
        
        if (propertyName.Equals(nameof(AppTab_ViewModel.IsExpanded))) {
            bool appTabExpands = (bool)o;
            if(appTabExpands)HidePa();
            return;    
        }

        if (propertyName.Equals(nameof(StudyContainer_ViewModel.RemoveStudyBroadCast))) {
            InvokePaAt(this.CurrentPaCenterNumber);
            return;
        }
    }
}