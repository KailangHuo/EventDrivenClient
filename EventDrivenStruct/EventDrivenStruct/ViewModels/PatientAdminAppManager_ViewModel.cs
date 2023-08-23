using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class PatientAdminAppManager_ViewModel : AbstractEventDrivenViewModel{

    public PatientAdminAppManager_ViewModel() {
        currentPaCenterNumber = -1;
        resetPaSeqList();
    }

    private void resetPaSeqList() {
        _paAppSequenceItems = new List<AppSequenceItem>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            _paAppSequenceItems.Add(null);
        }
    }

    private PatientAdminCenterApp_ViewModel PatientAdminCenterAppViewModel;

    private List<AppSequenceItem> _paAppSequenceItems;

    private int currentPaCenterNumber;

    public void InvokePaAt(int number) {
        // TODO: 关闭当前检查和所有的时候出发了remove event, 因为curNumber == 默认的0, 所以没有显示, 需要修复 
        if(number >= SystemConfiguration.GetInstance().GetScreenNumber() || number < 0) return;
        if(currentPaCenterNumber == number) return;
        currentPaCenterNumber = number;
        resetPaSeqList();
        _paAppSequenceItems[number] = new AppSequenceItem(PatientAdminCenterAppViewModel, 0);
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
        if (propertyName.Equals(nameof(StudyContainer_ViewModel.RemoveStudyBroadCast))) {
            InvokePaAt(0);
        }

        if (propertyName.Equals(nameof(StudyContainer_ViewModel.ClearAll))) {
            InvokePaAt(0);
        }
    }
}