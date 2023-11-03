using System.Collections.ObjectModel;
using EventDrivenElements;

namespace EventDrivenStruct.ViewModels.testVM; 

public class TextItemManager : AbstractEventDrivenViewModel{

    public TextItemManager() {
        this.ItemList = new ObservableCollection<TextItem>();
        ItemList.Add(new TextItem("AAA"));
        ItemList.Add(new TextItem("BB"));
        ItemList.Add(new TextItem("C"));
    }

    public ObservableCollection<TextItem> ItemList { get; set; }

    public TextItem GetItemByIndex(int index) {
        return ItemList[index];
    }
}