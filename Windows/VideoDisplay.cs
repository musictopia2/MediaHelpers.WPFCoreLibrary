namespace MediaHelpers.WPFCoreLibrary.Windows;
public class VideoDisplay : UserControl
{
    public VideoDisplay()
    {
        IDisplay display = StartUp.GetDisplay();
        Content = display;
    }
}