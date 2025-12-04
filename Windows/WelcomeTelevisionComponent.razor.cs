namespace MediaHelpers.WPFCoreLibrary.Windows;
public partial class WelcomeTelevisionComponent<E>
    where E : class, IEpisodeTable
{
    [Inject]
    private IStartLoadingViewModel? Begins { get; set; }

    [Inject]
    private TelevisionVideoLoaderClass<E>? Loader { get; set; }
    [Inject]
    private TelevisionContainerClass<E>? Container { get; set; }
    private void LoadEpisode()
    {
        Loader!.ChoseEpisode(Container!.EpisodeChosen!);
    }
}