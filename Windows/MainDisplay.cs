namespace MediaHelpers.WPFCoreLibrary.Windows;
public class MainDisplay : UserControl, IDisplay
{
    private readonly VideoPlayer _thisV;
    protected IVideoPlayerViewModel MainVM { get; set; }
    public MainDisplay(VideoPlayer player, IVideoPlayerViewModel videoPlayerViewModel)
    {
        MainVM = videoPlayerViewModel;
        _thisV = player;
        MediaElement element = _thisV.Element1;
        CreateVideoEvents();
        Content = element;
    }
    #region VideoEvents
    private void CreateVideoEvents()
    {
        _thisV!.MouseClick += ThisV_MouseClick;
        _thisV.CursorChanged += ThisV_CursorChanged;
    }
    protected virtual void PossibleHideButtons() //others can always show it as visible.
    {
        MainVM!.PlayButtonVisible = false;
        MainVM.CloseButtonVisible = false;
        MainVM.StateHasChanged?.Invoke();
    }
    protected virtual void PossibleShowButtons()
    {
        MainVM!.PlayButtonVisible = true;
        MainVM.CloseButtonVisible = true;
        MainVM.StateHasChanged?.Invoke();
    }
    private void ThisV_CursorChanged(bool visible)
    {
        if (_thisV!.ProcessingBeginning() == true)
        {
            return;
        }
        if (_thisV!.Element1.Visibility == Visibility.Hidden)
        {
            MainVM!.PlayButtonVisible = false;
            MainVM.CloseButtonVisible = true;
            Button3VisibleFalse();
            return;
        }
        if (visible == true)
        {
            PossibleShowButtons();
            CursorVisibleWithSelection();
        }
        else
        {
            PossibleHideButtons();
            Button3VisibleFalse();
        }
    }
    private void ThisV_MouseClick()
    {
        MainVM!.FullScreen = !MainVM.FullScreen;
        MainVM.StateHasChanged?.Invoke();
    }
    #endregion
    protected virtual void Button3VisibleFalse()
    {

    }
    protected virtual void CursorVisibleWithSelection()
    {

    }
}