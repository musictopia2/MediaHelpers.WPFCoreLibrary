namespace MediaHelpers.WPFCoreLibrary.Windows;
/// <summary>
/// Interaction logic for FirstWindow.xaml
/// </summary>
public partial class FirstWindow : Window
{
    public FirstWindow()
    {
        VideoPlayer.CurrentWindow = this; //i think has to be this one.

        //good news is did not make it worse so there is still hope
        Resources.Add("services", StartUp.GetProvider());
        InitializeComponent();
    }
}