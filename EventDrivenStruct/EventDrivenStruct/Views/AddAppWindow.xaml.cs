using System.Windows;

namespace EventDrivenStruct.Views; 

public partial class AddAppWindow : Window {
    public AddAppWindow() {
        InitializeComponent();
    }

    private void Cancle_button_OnClick(object sender, RoutedEventArgs e) {
        this.Close();
    }
}