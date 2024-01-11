namespace MediaHelpers.WPFCoreLibrary.Windows;
public class TelevisionVideoLoaderClass(TelevisionContainerClass container
        ) : ITelevisionVideoLoader
{
    private readonly TelevisionContainerClass _container = container;

    void ITelevisionVideoLoader.ChoseEpisode(IEpisodeTable episode)
    {
        _container.EpisodeChosen = episode;
        MainVideoProgressComponent.ProgressRenderType = typeof(TelevisionBarComponent);
        VideoWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}