namespace MediaHelpers.WPFCoreLibrary.MediaControls;
public class MP3Player : BaseMediaPlayer, IMP3Player
{
    string IMP3Player.TimeElapsedLabel()
    {
        if (IsPlaying == false)
        {
            return "0:00";
        }
        var intTime = TimeElapsedSeconds();
        return $"{Math.Floor((double)intTime / 60)}:{GetRight(intTime)}";
    }
    static string GetRight(int Time)
    {
        return (Time % 60).ToString().PadLeft(2, (char)0);
    }
    string IMP3Player.TotalInLabel()
    {
        if (IsPlaying == false)
        {
            return "0:00";
        }
        var songL = Length();
        return $"{Math.Floor((double)songL / 60)}:{GetRight(songL)}";
    }

    EnumMusicState IMP3Player.GetState()
    {
        if (IsPlaying == true)
        {
            return EnumMusicState.Playing;
        }
        return EnumMusicState.Paused;
    }
}