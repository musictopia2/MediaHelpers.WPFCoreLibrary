namespace MediaHelpers.WPFCoreLibrary.Windows;
public class TelevisionVideoLoaderClass<E>(TelevisionContainerClass<E> container) 
    where E: class, IEpisodeTable
{
    private readonly TelevisionContainerClass<E> _container = container;
    public void ChoseEpisode(E episode)
    {
        _container.EpisodeChosen = episode;
        MainVideoProgressComponent.ProgressRenderType = typeof(TelevisionBarComponent);
        VideoWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}