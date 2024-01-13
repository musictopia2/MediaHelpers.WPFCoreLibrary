namespace MediaHelpers.WPFCoreLibrary.Windows;
public class TelevisionVideoLoaderClass<E>(TelevisionContainerClass<E> container
        ) : ITelevisionVideoLoader<E>
    where E: class, IEpisodeTable
{
    private readonly TelevisionContainerClass<E> _container = container;

    void ITelevisionVideoLoader<E>.ChoseEpisode(E episode)
    {
        _container.EpisodeChosen = episode;
        MainVideoProgressComponent.ProgressRenderType = typeof(TelevisionBarComponent);
        VideoWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}