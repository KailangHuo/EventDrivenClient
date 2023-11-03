using System.Windows;

namespace EventDrivenStruct.Views;

public partial class TestingController : Window
{
    public TestingController()
    {
        InitializeComponent();
        this.WindowIndex = 1;
    }

    public int WindowIndex { get; set; }
}