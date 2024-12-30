namespace MediaHelpers.WPFCoreLibrary.Windows;
public partial class WelcomeMovieComponent<M>
    where M: class, IMainMovieTable
{
    private bool _rets;
    [Inject]
    private IStartLoadingViewModel? Begins { get; set; }
    [Inject]
    private MovieVideoLoaderClass<M>? Loader { get; set; }
    [Inject]
    private MovieContainerClass<M>? Container { get; set; }
    protected override void OnInitialized()
    {
        _rets = Begins!.CanPlay;

        if (_rets)
        {
            LoadEpisode();
            //if you are able to play, then needs to run the video loader to load up.
        }
        else
        {
            Begins.StartLoadingPlayer = LoadEpisode; //blazor won't change because its going to xaml screen with little blazor.
        }
    }
    private void LoadEpisode()
    {
        Loader!.ChoseMovie(Container!.MovieChosen!);
    }
}