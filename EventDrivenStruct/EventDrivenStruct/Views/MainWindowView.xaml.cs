using System.Windows;
using System.Windows.Controls;

namespace EventDrivenStruct.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
    }
    
    private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
        ScrollViewer sc = (ScrollViewer)sender;
        sc.ScrollToEnd();
    }
}