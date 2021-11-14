namespace MediaHelpers.WPFCoreLibrary.Windows;
public class TelevisionVideoLoaderClass : ITelevisionVideoLoader
{
    private readonly TelevisionContainerClass _container;
    public TelevisionVideoLoaderClass(TelevisionContainerClass container
        )
    {
        _container = container;
    }
    void ITelevisionVideoLoader.ChoseEpisode(IEpisodeTable episode)
    {
        _container.EpisodeChosen = episode;
        MainVideoProgressComponent.ProgressRenderType = typeof(TelevisionBarComponent);
        VideoWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}