namespace MediaHelpers.WPFCoreLibrary.Windows;
public class MusicDisplay : UserControl
{
    private IPlayPauseClass? _pauses;
    private readonly KeyboardHook _keys = new();
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
            _pauses.PlayPause();
        }
    }
}