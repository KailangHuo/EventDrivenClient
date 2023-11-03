using System;
using System.Collections.ObjectModel;
using EventDrivenElements;
using EventDrivenStruct.ConfigurationLoader;
using EventDrivenStruct.Models;
using EventDrivenStruct.ViewModels.testVM;
using Microsoft.VisualBasic.CompilerServices;

namespace EventDrivenStruct.ViewModels; 

public class TestWindow_ViewModel : AbstractEventDrivenViewModel{

    public TestWindow_ViewModel() {

    }

    public TextItemManager TextItemManager { get; set; }
    
}