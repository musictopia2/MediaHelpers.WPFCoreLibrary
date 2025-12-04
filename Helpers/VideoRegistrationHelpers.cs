namespace MediaHelpers.WPFCoreLibrary.Helpers;
public static class VideoRegistrationHelpers
{
    extension (IServiceCollection services)
    {
        public IServiceCollection RegisterWPFTelevisionLoaderRerunProcesses<E>()
        where E : class, IEpisodeTable
        {
            services.RegisterCoreLocalRerunLoaderTelevisionServices<E>()
                .RegisterWPFTelevisionLoaderBaseProcesses<E>();
            return services;
        }
        public IServiceCollection RegisterWPFTelevisionLoaderBaseProcesses<E>()
            where E : class, IEpisodeTable
        {
            services.AddSingleton<TelevisionVideoLoaderClass<E>>()
                .AddTransient<IDisplay, MainDisplay>()
                .RegisterVideoPlayer();
            return services;
        }
        public IServiceCollection RegisterVideoPlayer()
        {
            services.AddSingleton<VideoPlayer>();
            services.AddSingleton<IFullVideoPlayer>(pp => pp.GetRequiredService<VideoPlayer>());
            services.AddSingleton<ISimpleVideoPlayer>(pp => pp.GetRequiredService<VideoPlayer>());
            services.AddSingleton<IPausePlayer>(pp => pp.GetRequiredService<VideoPlayer>());
            return services;
        }
        public IServiceCollection RegisterWPFMoviesLoaderBaseProcesses<M>()
            where M : class, IMainMovieTable
        {
            services.AddSingleton<MovieVideoLoaderClass<M>>()
                .AddTransient<IDisplay, MainDisplay>()
                .RegisterVideoPlayer();
            return services;
        }
        public IServiceCollection RegisterWPFMoviesLoaderRerunProcesses<M>()
            where M : class, IMainMovieTable
        {
            services.RegisterWPFMoviesLoaderBaseProcesses<M>()
                .RegisterCoreLocalRerunLoaderMovieServices<M>();
            return services;
        }
    }
    
}