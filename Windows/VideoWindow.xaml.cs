namespace MediaHelpers.WPFCoreLibrary.Windows;
/// <summary>
/// Interaction logic for VideoWindow.xaml
/// </summary>
public partial class VideoWindow : Window
{
    public static int BottomHeight { get; set; } = 80;
    public VideoWindow()
    {
        VideoPlayer.CurrentWindow = this;
        Resources.Add("services", StartUp.GetProvider());
        InitializeComponent();
        Bottoms.Height = new GridLength(BottomHeight);
    }
}