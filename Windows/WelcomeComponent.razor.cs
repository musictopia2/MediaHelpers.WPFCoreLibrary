namespace MediaHelpers.WPFCoreLibrary.Windows;
public partial class WelcomeComponent<E>
    where E : class, IEpisodeTable
{
    [Inject]
    private IStartLoadingViewModel? Begins { get; set; }

    [Inject]
    private TelevisionVideoLoaderClass<E>? Loader { get; set; }
    [Inject]
    private TelevisionContainerClass<E>? Container { get; set; }

    private bool _rets;
    protected override void OnInitialized()
    {
        _rets = Begins!.CanPlay;
        if (_rets)
        {
            //if you are able to play, then needs to run the video loader to load up.
            Loader!.ChoseEpisode(Container!.EpisodeChosen!);
        }
    }
}