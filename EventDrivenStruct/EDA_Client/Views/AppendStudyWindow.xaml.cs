using System.Windows;

namespace EventDrivenStruct.Views; 

public partial class AppendStudyWindow : Window {
    public AppendStudyWindow() {
        InitializeComponent();
    }
    
    private void AddExamWindow_OnSizeChanged(object sender, SizeChangedEventArgs e) {
        
        // Get the window and its old and new sizes
        Window window = sender as Window;
        double oldWidth = e.PreviousSize.Width;
        double oldHeight = e.PreviousSize.Height;
        double newWidth = e.NewSize.Width;
        double newHeight = e.NewSize.Height;

        // Calculate the difference between the old and new sizes
        double widthDiff = newWidth - oldWidth;
        double heightDiff = newHeight - oldHeight;

        // Adjust the window position to keep the geometric center unchanged
        window.Left -= widthDiff / 2;
        window.Top -= heightDiff / 2;
    }
    
    private void Button_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) {
        if ((bool)e.NewValue == false) {
            this.Close();
        }
    }
}