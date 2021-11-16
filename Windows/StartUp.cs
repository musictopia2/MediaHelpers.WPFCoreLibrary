
namespace MediaHelpers.WPFCoreLibrary.Windows;
public static class StartUp
{
    public static Window? MainWindow { get; set; }
    private readonly static ServiceCollection _services = new();
    private static ServiceProvider? _provides;
    public static Action<ServiceCollection>? ExtraServiceProcesses { get; set; }
    private static bool _loaded = false;
    public static ServiceProvider GetProvider()
    {
        if (_loaded)
        {
            return _provides!;
        }
        _services.AddBlazorWebView();
        _services.RegisterWPFServices();
        _services.RegisterBlazorBeginningClasses();
        ExtraServiceProcesses?.Invoke(_services);
        _loaded = true;
        _provides = _services.BuildServiceProvider();
        return _provides;
    }
    public static IDisplay GetDisplay()
    {
        var provider = GetProvider();
        return provider.GetService<IDisplay>()!;
    }
    internal static MP3Player GetMP3Player()
    {
        var provider = GetProvider();
        return provider.GetService<MP3Player>()!;
    }
    internal static IPlayPauseClass GetPlayPause()
    {
        var provider = GetProvider();
        return provider.GetService<IPlayPauseClass>()!;
    }
}