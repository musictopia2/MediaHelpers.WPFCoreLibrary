namespace MediaHelpers.WPFCoreLibrary.Helpers;
public static class VideoRegistrationHelpers
{
    //public static void RegisterTelevisionFirstrunProcesses(this ServiceCollection services)
    //{
    //    services.AddSingleton<ITelevisionListLogic, TelevisionListFirstrunLogic>();
    //    services.AddSingleton<ITelevisionShellViewModel, TelevisionFirstrunShellViewModel>();
    //    services.AddSingleton<ITelevisionLoaderLogic, TelevisionFirstrunLoaderLogic>();
    //    services.RegisterBaseTelevisionProcesses();
    //}
    //public static void RegisterTelevisionRerunProcesses(this ServiceCollection services)
    //{
    //    services.AddSingleton<ITelevisionListLogic, TelevisionListRerunLogic>();
    //    services.AddSingleton<ITelevisionShellLogic, TelevisionRerunsShellLogic>();
    //    services.AddSingleton<ITelevisionShellViewModel, TelevisionRerunsShellViewModel>();
    //    services.AddSingleton<ITelevisionHolidayLogic, TelevisionHolidayLogic>();
    //    services.AddSingleton<TelevisionHolidayViewModel>();
    //    services.AddSingleton<ITelevisionLoaderLogic, TelevisionRerunsLoaderLogic>();
    //    services.RegisterBaseTelevisionProcesses();
    //}
    
    public static IServiceCollection RegisterWPFTelevisionLoaderRerunProcesses(this IServiceCollection services)
    {
        services.RegisterCoreLocalRerunLoaderTelevisionServices()
            .RegisterWPFLoaderBaseProcesses();
        return services;
    }
    private static IServiceCollection RegisterWPFLoaderBaseProcesses(this IServiceCollection services)
    {
        services.AddSingleton<ITelevisionVideoLoader, TelevisionVideoLoaderClass>()
            .AddTransient<IDisplay, MainDisplay>()
            .RegisterVideoPlayer();
        return services;
    }

    //public static ServiceCollection RegisterMockRemoteControls(this ServiceCollection services)
    //{
    //    services.AddSingleton<ITelevisionRemoteControlHostService, MockTelevisionRemoteControlHostService>();
    //    return services;
    //}
    //public static void RegisterBaseTelevisionProcesses(this ServiceCollection services)
    //{
    //    services.RegisterVideoPlayer();
    //    services.AddTransient<IDisplay, MainDisplay>();
    //    services.AddSingleton<TelevisionListViewModel>();
    //    services.AddSingleton<ITelevisionVideoLoader, TelevisionVideoLoaderClass>();
    //    services.AddSingleton<TelevisionContainerClass>();
    //    services.AddSingleton<TelevisionLoaderViewModel>();
    //    services.AddSingleton<IVideoPlayerViewModel>(pp => pp.GetRequiredService<TelevisionLoaderViewModel>());
    //    services.AddSingleton<ITelevisionLoaderViewModel>(pp => pp.GetRequiredService<TelevisionLoaderViewModel>());
    //}
    public static IServiceCollection RegisterVideoPlayer(this IServiceCollection services)
    {
        services.AddSingleton<VideoPlayer>();
        services.AddSingleton<IFullVideoPlayer>(pp => pp.GetRequiredService<VideoPlayer>());
        services.AddSingleton<ISimpleVideoPlayer>(pp => pp.GetRequiredService<VideoPlayer>());
        services.AddSingleton<IPausePlayer>(pp => pp.GetRequiredService<VideoPlayer>());
        return services;
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