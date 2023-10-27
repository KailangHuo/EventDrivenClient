using System.Windows;
using System.Windows.Controls;

namespace EventDrivenStruct.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        boxNumber = 0;
        InitializeComponent();
    }

    public int boxNumber;
    
    private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
        ScrollViewer sc = (ScrollViewer)sender;
        sc.ScrollToEnd();
    }
}