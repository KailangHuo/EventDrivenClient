using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class PatientAdminAppManager_ViewModel : AbstractEventDrivenViewModel{

    public PatientAdminAppManager_ViewModel() {
        initPaSeq();
    }

    private void initPaSeq() {
        _paAppSequenceItems = new List<AppSequenceItem>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            _paAppSequenceItems.Add(null);
        }
    }

    private PatientAdminCenterApp_ViewModel PatientAdminCenterAppViewModel;

    private List<AppSequenceItem> _paAppSequenceItems;

    public void InvokePaAt(int number) {
        if(number >= SystemConfiguration.GetInstance().GetScreenNumber() || number < 0) return;
        
        // TODO:不要再重新装填了, 直接发送
        
        AppSequenceItem paAqqSeq = new AppSequenceItem(PatientAdminCenterAppViewModel, 0);
        if (_paAppSequenceItems[number] == null) {
            for (int i = 0; i < _paAppSequenceItems.Count; i++) {
                if (_paAppSequenceItems[i] != null) {
                    _paAppSequenceItems = null;
                    break;
                }
            }

            _paAppSequenceItems[number] = paAqqSeq;
        }
        
        SelectionFinished();
    }

    public void InitPaCenter(PatientAdminCenterApp_ViewModel patientAdminCenterAppViewModel) {
        this.PatientAdminCenterAppViewModel = patientAdminCenterAppViewModel;
        InvokePaAt(0);
    }

    public void SelectionFinished() {
        PublishEvent(nameof(SelectionFinished), _paAppSequenceItems);
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(StudyContainer_ViewModel.RemoveStudyEvent))) {
            InvokePaAt(0);
        }
    }
}