namespace MediaHelpers.WPFCoreLibrary.Windows;
public class FirstDisplay : UserControl //used to test the displays need to allow for testing
{
    private readonly FirstContainerClass _container;
    public FirstDisplay()
    {
        ServiceProvider pp = StartUp.GetProvider();
        _container = pp.GetService<FirstContainerClass>()!;
        TextBlock text = new();
        text.Foreground = Brushes.White;
        text.FontSize = 100;
        text.Text = $"{_container.ShowChosen!.ShowName} XAML"; //will be replaced by actual video.
        Content = text;
    }
}