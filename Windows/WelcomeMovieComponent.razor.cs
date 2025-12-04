namespace MediaHelpers.WPFCoreLibrary.Windows;
public partial class WelcomeMovieComponent<M>
    where M: class, IMainMovieTable
{
    [Inject]
    private IStartLoadingViewModel? Begins { get; set; }
    [Inject]
    private MovieVideoLoaderClass<M>? Loader { get; set; }
    [Inject]
    private MovieContainerClass<M>? Container { get; set; }
    private void LoadEpisode()
    {
        Loader!.ChoseMovie(Container!.MovieChosen!);
    }
}