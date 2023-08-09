using System.Windows;

namespace EventDrivenStruct; 

public partial class AddExamWindow : Window {
    public AddExamWindow() {
        InitializeComponent();
    }

    private void Button_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
        if ((bool)e.NewValue == false) {
            this.Close();
        }
    }
}