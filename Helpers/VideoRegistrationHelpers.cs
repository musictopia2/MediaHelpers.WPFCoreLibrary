namespace MediaHelpers.WPFCoreLibrary.Helpers;
public static class VideoRegistrationHelpers
{
    public static void RegisterTelevisionFirstrunProcesses(this ServiceCollection services)
    {
        services.AddSingleton<ITelevisionListLogic, TelevisionListFirstrunLogic>();
        services.AddSingleton<ITelevisionShellViewModel, TelevisionFirstrunShellViewModel>();
        services.AddSingleton<ITelevisionLoaderLogic, TelevisionFirstrunLoaderLogic>();
        services.RegisterBaseTelevisionProcesses();
    }
    public static void RegisterTelevisionRerunProcesses(this ServiceCollection services)
    {
        services.AddSingleton<ITelevisionListLogic, TelevisionListRerunLogic>();
        services.AddSingleton<ITelevisionShellLogic, TelevisionRerunsShellLogic>();
        services.AddSingleton<ITelevisionShellViewModel, TelevisionRerunsShellViewModel>();
        services.AddSingleton<ITelevisionHolidayLogic, TelevisionHolidayLogic>();
        services.AddSingleton<TelevisionHolidayViewModel>();
        services.AddSingleton<ITelevisionLoaderLogic, TelevisionRerunsLoaderLogic>();
        services.RegisterBaseTelevisionProcesses();
    }
    public static ServiceCollection RegisterMockRemoteControls(this ServiceCollection services)
    {
        services.AddSingleton<ITelevisionRemoteControlHostService, MockTelevisionRemoteControlHostService>();
        return services;
    }
    public static void RegisterBaseTelevisionProcesses(this ServiceCollection services)
    {
        services.RegisterVideoPlayer();
        services.AddTransient<IDisplay, MainDisplay>();
        services.AddSingleton<TelevisionListViewModel>();
        services.AddSingleton<ITelevisionVideoLoader, TelevisionVideoLoaderClass>();
        services.AddSingleton<TelevisionContainerClass>();
        services.AddSingleton<TelevisionLoaderViewModel>();
        services.AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<TelevisionLoaderViewModel>());
    }
    public static void RegisterVideoPlayer(this ServiceCollection services)
    {
        services.AddSingleton<VideoPlayer>();
        services.AddSingleton<IVideoPlayer>(pp => pp.GetRequiredService<VideoPlayer>());
    }
    public static void RegisterMoviesProcesses(this ServiceCollection services)
    {
        services.RegisterVideoPlayer();
        services.AddSingleton<IDisplay, MovieDisplay>();
        services.AddSingleton<MovieListViewModel>();
        services.AddSingleton<IMovieListLogic, MovieListLogic>();
        services.AddSingleton<MovieContainerClass>();
        services.AddSingleton<IMovieLoaderLogic, MovieLoaderLogic>();
        services.AddSingleton<IMovieVideoLoader, MovieVideoLoaderClass>();
        services.AddSingleton<MovieLoaderViewModel>();
        services.AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<MovieLoaderViewModel>());
    }
}