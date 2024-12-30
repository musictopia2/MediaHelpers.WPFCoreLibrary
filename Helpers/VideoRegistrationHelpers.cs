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
    public static IServiceCollection RegisterWPFMoviesLoaderBaseProcesses<M>(this IServiceCollection services)
        where M : class, IMainMovieTable
    {
        services.AddSingleton<MovieVideoLoaderClass<M>>()
            .AddTransient<IDisplay, MainDisplay>()
            .RegisterVideoPlayer();
        return services;
    }
    public static IServiceCollection RegisterWPFMoviesLoaderRerunProcesses<M>(this IServiceCollection services)
        where M : class, IMainMovieTable
    {
        services.RegisterWPFMoviesLoaderBaseProcesses<M>()
            .RegisterCoreLocalRerunLoaderMovieServices<M>();
        return services;
    }
}