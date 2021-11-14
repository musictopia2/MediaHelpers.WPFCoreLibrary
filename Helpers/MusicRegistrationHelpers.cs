namespace MediaHelpers.WPFCoreLibrary.Helpers;
public static class MusicRegistrationHelpers
{
    //this is needed so the main window can call a method.
    public static MusicDisplay? MusicControl { get; set; }
    public static ServiceCollection RegisterMusicPlayer(this ServiceCollection services)
    {
        services.AddSingleton<MP3Player>();
        services.AddSingleton<IMP3Player>(pp => pp.GetRequiredService<MP3Player>());
        return services;
    }
    public static ServiceCollection RegisterBasicMusicProcesses(this ServiceCollection services)
    {
        services.AddSingleton<IPrepareSong, PrepareSong>()
            .AddSingleton<IMusicShuffleProcesses, MusicShuffleProcesses>()
            .AddSingleton<ChangeSongContainer>();

        return services;
    }
    public static ServiceCollection RegisterBasicJukeboxServices(this ServiceCollection services)
    {
        services.RegisterMusicPlayer()
            .RegisterBasicMusicProcesses()
            .AddSingleton<BasicSongProgressViewModel>()
            .AddSingleton<IPlayPauseClass>(pp => pp.GetRequiredService<BasicSongProgressViewModel>())
            .AddSingleton<INextSongClass>(pp => pp.GetRequiredService<BasicSongProgressViewModel>())
            .AddSingleton<JukeboxViewModel>()
            .AddSingleton<JukeboxLogic>()
            .AddSingleton<IJukeboxLogic>(pp => pp.GetRequiredService<JukeboxLogic>())
            .AddSingleton<IProgressMusicPlayer>(pp => pp.GetRequiredService<JukeboxLogic>());
        return services;
    }
    public static ServiceCollection RegisterBasicPlaylistSongServices(this ServiceCollection services)
    {
        services.RegisterMusicPlayer();
        services.RegisterBasicMusicProcesses();
        services.AddSingleton<PlaylistSongProgressViewModel>() 
            .AddSingleton<BasicSongProgressViewModel>(pp => pp.GetRequiredService<PlaylistSongProgressViewModel>())
            .AddSingleton<IPlayPauseClass>(pp => pp.GetRequiredService<PlaylistSongProgressViewModel>())
            .AddSingleton<INextSongClass>(pp => pp.GetRequiredService<PlaylistSongProgressViewModel>())
            .AddSingleton<PlaylistSongBuilderViewModel>()
            .AddSingleton<PlaylistSongDashboardViewModel>();
        return services;
    }
}