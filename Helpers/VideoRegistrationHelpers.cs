namespace MediaHelpers.WPFCoreLibrary.Helpers;
public static class VideoRegistrationHelpers
{
    public static IServiceCollection RegisterWPFTelevisionLoaderRerunProcesses<E>(this IServiceCollection services)
        where E: class, IEpisodeTable
    {
        services.RegisterCoreLocalRerunLoaderTelevisionServices<E>()
            .RegisterWPFTelevisionLoaderBaseProcesses<E>();
        return services;
    }
    public static IServiceCollection RegisterWPFTelevisionLoaderBaseProcesses<E>(this IServiceCollection services)
        where E : class, IEpisodeTable
    {
        services.AddSingleton<TelevisionVideoLoaderClass<E>>()
            .AddTransient<IDisplay, MainDisplay>()
            .RegisterVideoPlayer();
        return services;
    }
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