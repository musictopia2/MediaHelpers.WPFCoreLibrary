namespace MediaHelpers.WPFCoreLibrary.Windows;
public class MovieVideoLoaderClass<M>(MovieContainerClass<M> container)
    where M : class, IMainMovieTable
{
    public void ChoseMovie(M movie)
    {
        container.MovieChosen = movie;
        MainVideoProgressComponent.ProgressRenderType = typeof(MovieBarComponent);
        VideoWindow window = new();
        window.Show();
        StartUp.MainWindow!.Close();
    }
}