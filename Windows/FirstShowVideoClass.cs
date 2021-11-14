namespace MediaHelpers.WPFCoreLibrary.Windows;
public class FirstShowVideoClass : IFirstVideoLoader //used for testing
{
    private readonly FirstContainerClass _firstContainerClass;
    public FirstShowVideoClass(FirstContainerClass firstContainerClass)
    {
        _firstContainerClass = firstContainerClass;
    }
    void IFirstVideoLoader.ChoseVideo(IShowTable showChosen)
    {
        _firstContainerClass.DateProgress = DateTime.Now;
        _firstContainerClass.ShowChosen = showChosen;
        _firstContainerClass.RenderType = typeof(FirstVideoProgressComponent);
        FirstWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}