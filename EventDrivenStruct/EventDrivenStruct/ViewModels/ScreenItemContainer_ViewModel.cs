using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;

namespace EventDrivenStruct.ViewModels; 

public class ScreenItemContainer_ViewModel : AbstractEventDrivenViewModel{

    public ScreenItemContainer_ViewModel() {
        InitSco();
    }
    

    public List<ScreenItem_ViewModel> ScoList;

    private void InitSco() {
        ScoList = new List<ScreenItem_ViewModel>();
        int screenNumber = SystemConfiguration.GetInstance().GetScreenNumber();
        for (int i = 0; i < screenNumber; i++) {
            ScoList.Add(new ScreenItem_ViewModel(i));
        }
    }

    public void UpdateScreenContent(List<ScreenContentObject> screenContentObjects) {
        if (screenContentObjects.Count != this.ScoList.Count) throw new IndexOutOfRangeException();
        for (int i = 0; i < screenContentObjects.Count; i++) {
            this.ScoList[i].TrySwapContent(screenContentObjects[i]);
        }
    }

    public override void UpdateByEvent(string propertyName, object o) {
        if (propertyName.Equals(nameof(AppTitleTab_ViewModel.AppConSeqItemsSelectedChanged))) {
            List<ScreenContentObject> list = (List<ScreenContentObject>)o;
            UpdateScreenContent(list);
        }
    }
}