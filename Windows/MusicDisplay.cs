namespace MediaHelpers.WPFCoreLibrary.Windows;
public class MusicDisplay : UserControl
{
    private IPlayPauseClass? _pauses;
    private readonly KeyboardHook _keys = new();
    // Static dictionary to register external handlers
    private static readonly Dictionary<EnumKey, Action> _extraHotkeys = [];
    public MusicDisplay()
    {
        MusicRegistrationHelpers.MusicControl = this;
    }
    public void LoadMusicUI()
    {
        MP3Player player = StartUp.GetMP3Player();
        _pauses = StartUp.GetPlayPause();
        if (_pauses == null)
        {
            throw new CustomBasicException("Failed to get the pause interface.  Rethink");
        }
        _keys.KeyUp += Keys_KeyUp;
        Content = player.Element1;
    }
    private void Keys_KeyUp(EnumKey key)
    {
        if (_pauses == null)
        {
            throw new CustomBasicException("Model was never sent when using media key.  Rethink");
        }
        if (key == EnumKey.MediaPlayPause && _pauses.CanPause == true) 
        {
            if (BasicSongProgressViewModel.CanMusicPlay() == false)
            {
                return; //because if music can't even play, then ignore.  no popup because maybe another system is used (?)
                //if i run into problems, then may need a button to let me know the status.
            }
            _pauses.PlayPause();
            return;
        }
        foreach (var item in _extraHotkeys)
        {
            if (key == item.Key)
            {
                item.Value?.Invoke();
            }
        }
    }
    public static void RegisterHotkey(EnumKey key, Action action)
    {
        _extraHotkeys[key] = action;
    }

    // Optional: method to clear handlers (useful for testing or unloading)
    public static void UnregisterHotkey(EnumKey key)
    {
        _extraHotkeys.Remove(key);
    }
}