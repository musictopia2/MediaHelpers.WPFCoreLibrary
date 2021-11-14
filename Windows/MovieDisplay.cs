namespace MediaHelpers.WPFCoreLibrary.Windows;
public class MovieDisplay : MainDisplay
{
    private readonly MovieLoaderViewModel _model;
    public MovieDisplay(VideoPlayer player, IVideoPlayerViewModel videoPlayerViewModel) : base(player, videoPlayerViewModel)
    {
        _model = (MovieLoaderViewModel)videoPlayerViewModel;
    }
    protected override void Button3VisibleFalse()
    {
        _model.Button3Visible = false;
        _model.StateHasChanged?.Invoke();
    }
    protected override void CursorVisibleWithSelection()
    {
        if (_model.SelectedItem!.Opening.HasValue == true && _model.SelectedItem.Closing.HasValue == true)
        {
            _model.Button3Visible = false;
        }
        else
        {
            _model.Button3Visible = true;
        }
        _model.StateHasChanged?.Invoke();
    }
}