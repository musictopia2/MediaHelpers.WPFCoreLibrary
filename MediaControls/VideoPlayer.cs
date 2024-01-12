namespace MediaHelpers.WPFCoreLibrary.MediaControls;
public class VideoPlayer : BaseMediaPlayer, IFullVideoPlayer
{
    public static Window? CurrentWindow { get; set; }
    private DispatcherTimer? _timer;
    private DispatcherTimer? _hideCursorTimer;
    private bool _isFullScreen = false;
    private WindowStyle _prevWindowStyle;
    public event Action<int>? SaveResume;
    private int _howLongBeforeRemovingCursorValue = 1000;
    private int _previousWidth;
    private int _previousHeight;
    private int _secs;
    private BasicList<SkipSceneClass> _privateSkipList = new();
    private int _oldSecs;
    private bool _isInit;
    private readonly TimeSpan _limit = default;
    public event Action? MediaEnded;
    public event Action<string, string>? Progress;
    public event Action? ResumeLater;
    public event Action? MouseClick;
    public event Action<bool>? CursorChanged;
    public static bool IsTesting { get; set; } //if testing, then can't be full screen.
    public bool NoCursor { get; set; }
    public int UpTo { get; set; }
    public bool IsCursorVisible
    {
        get
        {
            return (!(Cursors.None.Equals(Element1.Cursor)));
        }
        set
        {
            if (NoCursor == true || IsTesting)
            {
                return;
            }
            if (value == true)
            {
                Element1.Cursor = Cursors.Arrow;
            }
            else if (value == false)
            {
                EventuallyMove();
                return;
            }
            SetWindowCursor();
            PrivateCursor();
        }
    }
    private void SetWindowCursor()
    {
        if (CurrentWindow is not null)
        {
            CurrentWindow.Cursor = Element1.Cursor;
        }
    }
    private bool _processing;
    private async void EventuallyMove()
    {
        int y;
        y = System.Windows.Forms.Cursor.Position.Y;
        if (y < 950)
        {
            Element1.Cursor = Cursors.None;
            SetWindowCursor();
            CursorChanged?.Invoke(false);
            return;
        }
        _processing = true;
        CursorChanged?.Invoke(false);
        Element1.Cursor = Cursors.None;
        SetWindowCursor();
        await Task.Delay(2000);
        if (IsCursorVisible == false)
        {
            y = System.Windows.Forms.Cursor.Position.Y;
            if (y > 950)
            {
                MoveCursor(400, 800);
                await Task.Delay(10);
                Element1.Cursor = Cursors.Arrow;
                SetWindowCursor();
                await Task.Delay(10);
                Element1.Cursor = Cursors.None;
                SetWindowCursor();
            }
        }
        _processing = false;
    }
    private void PrivateCursor()
    {
        if (_processing)
        {
            return;
        }
        if (CursorChanged == null)
        {
            return;
        }
        CursorChanged(IsCursorVisible);
    }
    protected override void AfterNew()
    {
        _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, new EventHandler(TimerCallback), Element1.Dispatcher);
        _hideCursorTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, new EventHandler(TimerHideCallback), Element1.Dispatcher);
        Savetimer = new DispatcherTimer(TimeSpan.FromSeconds(10), DispatcherPriority.Background, new EventHandler(SaveCallBack), Element1.Dispatcher);
    }
    private void ThisWindow_MouseMove(object sender, MouseEventArgs e)
    {
        StartCursor();
    }
    public bool FullScreen
    {
        get
        {
            return _isFullScreen;
        }
        set
        {
            _isFullScreen = value;
            if (NoCursor == true || IsTesting)
            {
                return;
            }
            if (CurrentWindow == null)
            {
                throw new CustomBasicException("Must have a current window in order to set full screen.  Rethink");
            }
            if (_isFullScreen == true)
            {
                CurrentWindow.WindowStyle = WindowStyle.None;
                CurrentWindow.WindowState = WindowState.Maximized;
                IsCursorVisible = !IsPlaying;
            }
            else
            {
                CurrentWindow.Width = _previousWidth;
                CurrentWindow.Height = _previousHeight;
                CurrentWindow.WindowState = WindowState.Normal;
                CurrentWindow.WindowStyle = _prevWindowStyle;
                _hideCursorTimer!.Stop();
                IsCursorVisible = true;
            }
        }
    }
    public int HowLongBeforeRemovingCursor
    {
        get
        {
            return _howLongBeforeRemovingCursorValue;
        }
        set
        {
            _hideCursorTimer!.Stop();
            _howLongBeforeRemovingCursorValue = value;
            _hideCursorTimer.Interval = TimeSpan.FromMilliseconds(HowLongBeforeRemovingCursor);
            if (FullScreen)
            {
                _hideCursorTimer.Start();
            }
        }
    }
    public TimeSpan TimeLimit
    {
        get
        {
            return _limit;
        }
        set
        {
            var limitTimer = new DispatcherTimer(value, DispatcherPriority.Normal,
                new EventHandler((sender, e) => ResumeLater?.Invoke()), Element1.Dispatcher);
        }
    }
    public double SpeedRatio
    {
        get
        {
            return Element1.SpeedRatio;
        }
        set
        {
            Element1.SpeedRatio = value;
            if (value == .5)
            {
                _timer!.Interval = TimeSpan.FromSeconds(2);
                return;
            }
            else if (value > 3)
            {
                _timer!.Interval = TimeSpan.FromMilliseconds(100);
                return;
            }
            _timer!.Interval = value switch
            {
                1 => TimeSpan.FromSeconds(1),
                2 => TimeSpan.FromMilliseconds(500),
                3 => TimeSpan.FromMilliseconds(333),
                4 => TimeSpan.FromMilliseconds(250),
                5 => TimeSpan.FromMilliseconds(200),
                _ => TimeSpan.FromSeconds(1),
            };
        }
    }
    public DispatcherTimer? Savetimer { get; set; }
    public bool PossibleSkips { get; set; } = true;
    private bool _manuelEnd;
    protected override void OnMediaEnded()
    {
        if (PossibleSkips == false)
        {
            _timer!.Stop();
            _manuelEnd = true;
            MediaEnded!();
            return;
        }
        base.OnMediaEnded();
    }
    public override bool IsFinished()
    {
        throw new CustomBasicException("I don't think that isfinished is used from video.  if i am wrong, rethink");
    }
    public override bool IsPaused()
    {
        return !IsPlaying;
    }
    protected override void AfterPause()
    {
        _blazorHover = false;
        if (NoCursor == false)
        {
            IsCursorVisible = true;
            PrivateCursor();
        }
        _hideCursorTimer!.Stop();
    }
    protected override void AfterResumePlay()
    {
        if (FullScreen == true)
        {
            IsCursorVisible = false;
            PrivateCursor();
        }
    }
    private void AnalyzeSkipList()
    {
        if (_privateSkipList.Count == 0)
        {
            return;
        }
        if (Element1.Position.TotalSeconds == 0)
        {
            return;
        }
        int seconds = (int)Element1.Position.TotalSeconds;
        _privateSkipList.KeepConditionalItems(xx => xx.StartTime > seconds);
        _privateSkipList.Sort();
    }
    public override void AfterStartPlay()
    {
        AnalyzeSkipList();
        _timer!.Start();
        _manuelEnd = false;
        if (DefaultVolume > -1 && Volume > 0)
        {
            Volume = DefaultVolume;
        }

        if (NoCursor == true)
        {
            return;
        }

        if (FullScreen == true)
        {
            IsCursorVisible = false;
        }
    }
    bool _firstInit;
    public void Init()
    {
        if (CurrentWindow == null)
        {
            throw new CustomBasicException("Must set a current window when init.  Possible rethinking is required");
        }
        if (_firstInit)
        {
            _isInit = true;
            return;
        }
        _isInit = true;
        _firstInit = true;
        CurrentWindow.MouseMove += ThisWindow_MouseMove;
        _prevWindowStyle = CurrentWindow.WindowStyle;
        _previousWidth = (int)CurrentWindow.Width;
        _previousHeight = (int)CurrentWindow.Height;
    }
    public void SaveCallBack(object? sender, EventArgs e)
    {
        if (IsPaused() == true || Element1.Visibility == Visibility.Hidden)
        {
            return;
        }
        if (_secs == -1)
        {
            _secs = 0;
        }
        if (_oldSecs != _secs)
        {
            SaveResume!(_secs);
            _oldSecs = _secs;
        }
    }
    public void AddScenesToSkip(BasicList<SkipSceneClass> list)
    {
        _privateSkipList = list.ToBasicList();
    }
    public void TimerHideCallback(object? sender, EventArgs e)
    {
        if (IsTesting)
        {
            return;
        }
        if (_isFullScreen == false)
        {
            return;
        }
        if (IsPlaying == false)
        {
            return;
        }
        if (_blazorHover)
        {
            return;
        }
        if (NoCursor == true)
        {
            _hideCursorTimer!.Stop();
            return;
        }
        if (IsCursorVisible == true)
        {
            _hideCursorTimer!.Stop();
            IsCursorVisible = false;
        }
    }
    public static void MoveCursor(int x, int y)
    {
        System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
    }
    private bool _blazorHover;
    public bool ProcessingBeginning()
    {
        return _isInit;
    }
    public int TimeElapsed()
    {
        return (int)Element1.Position.TotalSeconds;
    }
    public async void TimerCallback(object? sender, EventArgs e)
    {
        _timer!.Stop();
        _isInit = false;
        if (NoCursor == true)
        {
            await EndTimers();
            return;
        }
        if (_isFullScreen == false && IsCursorVisible == false || IsCursorVisible == false && IsPlaying == false)
        {
            IsCursorVisible = true;
            _hideCursorTimer!.Stop();
        }
        if (Element1.Visibility == Visibility.Hidden)
        {
            IsCursorVisible = true;
            _hideCursorTimer!.Stop();
            _timer.Start();
            return;
        }
        await EndTimers();
    }
    private async Task EndTimers()
    {
        if (_manuelEnd)
        {
            return;
        }
        if (Element1.Position >= LengthValue && Element1.Position.TotalSeconds <= 1)
        {
            _timer!.Start();
            return;
        }
        if (DidInit == false)
        {
            _timer!.Start();
            return;
        }
        if (IsPaused() == false)
        {
            _secs = TimeElapsed();
            if (Element1.Position < LengthValue && _privateSkipList.Count > 0)
            {
                var thisSkip = _privateSkipList.First();
                if ((thisSkip.EndTime > 0 || thisSkip.HowLong > 0) && thisSkip.StartTime <= _secs)
                {
                    int endAt;
                    if (thisSkip.EndTime == 0 && thisSkip.HowLong > 0)
                    {
                        endAt = thisSkip.StartTime + thisSkip.HowLong;
                    }
                    else
                    {
                        endAt = thisSkip.EndTime;
                    }
                    _privateSkipList.RemoveSpecificItem(thisSkip);
                    if (endAt < LengthValue.TotalSeconds)
                    {
                        await PlayAsync((int)LengthValue.TotalSeconds, endAt);
                        _timer!.Start();
                        return;
                    }
                }
            }
        }
        if (Element1.Position >= LengthValue && PossibleSkips == true)
        {
            Element1.Stop();
            _timer!.Start();
            _timer.Stop();
            MediaEnded!();
            return;
        }
        if (IsPaused())
        {
            _timer!.Start();
            return;
        }
        if (_secs == -1)
        {
            _secs = 0;
        }
        TimeSpan elapsTimeSpan = TimeSpan.FromSeconds(_secs);
        Progress?.Invoke(string.Format("{0:00}:{1:00}:{2:00}", elapsTimeSpan.Hours, elapsTimeSpan.Minutes, elapsTimeSpan.Seconds), TimeSpan.FromSeconds(MediaLength).ToString());
        _timer!.Start();
    }
    private void StartCursor()
    {
        if (NoCursor == true)
        {
            return;
        }
        if (Element1.Visibility == Visibility.Hidden)
        {
            IsCursorVisible = true;
            return;
        }
        if (FullScreen && !IsCursorVisible)
        {
            IsCursorVisible = true;
            _hideCursorTimer!.Stop();
            _hideCursorTimer.Start();
        }
    }
    protected override void OnMouseUp()
    {
        MouseClick!();
    }
    protected override void OnMouseMove()
    {
        StartCursor();
    }
    bool IFullVideoPlayer.IsCursorVisible()
    {
        return IsCursorVisible;
    }
    public void OtherMouseMove()
    {
        StartCursor();
    }
    public void ShowCursor()
    {
        if (NoCursor)
        {
            return;
        }
        IsCursorVisible = true;
        _blazorHover = true;
    }
    public void HideCursor()
    {
        if (NoCursor)
        {
            return;
        }
        if (IsPaused())
        {
            return;
        }
        if (FullScreen == false)
        {
            return;
        }
        _blazorHover = false;
        IsCursorVisible = false;
    }
}