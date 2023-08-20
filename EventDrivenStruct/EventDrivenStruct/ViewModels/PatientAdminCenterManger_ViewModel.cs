using System.Collections.Generic;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;

namespace EventDrivenStruct.ViewModels; 

public class PatientAdminCenterManger_ViewModel : AbstractEventDrivenViewModel {

    private PatientAdminCenterManger_ViewModel() {
        this.PaSeqItems = new List<AppSequenceItem>();
        init();
    }

    private static PatientAdminCenterManger_ViewModel _instance;

    public static PatientAdminCenterManger_ViewModel GetInstance() {
        if (_instance == null) {
            lock (typeof(PatientAdminCenterManger_ViewModel)) {
                if (_instance == null) {
                    _instance = new PatientAdminCenterManger_ViewModel();
                }
            }
        }

        return _instance;
    }

    private void init() {
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            PaSeqItems.Add(null);
        }
        InvokePaCenterAt(0);
    }

    private List<AppSequenceItem> PaSeqItems;

    public void InvokePaCenterAt(int number) {
        
    }

}