namespace MediaHelpers.WPFCoreLibrary.Windows;
public class MovieVideoLoaderClass : IMovieVideoLoader
{
    private readonly MovieContainerClass _container;
    public MovieVideoLoaderClass(MovieContainerClass container)
    {
        _container = container;
    }
    void IMovieVideoLoader.ChoseMovie(IMainMovieTable movie)
    {
        _container.MovieChosen = movie;
        MainVideoProgressComponent.ProgressRenderType = typeof(MovieBarComponent);
        VideoWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}