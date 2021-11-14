namespace MediaHelpers.WPFCoreLibrary.MediaControls;
public class BaseMediaPlayer : IBasicMediaPlayer
{
    public MediaElement Element1;
    protected int DefaultVolumeValue = 100;
    protected TimeSpan LengthValue;
    protected TimeSpan PositionValue;
    protected int MediaLength;
    protected bool IsPlaying = false;
    protected string TempPath = "";
    private readonly UriTypeConverter _thisC = new();
    private bool? _isOpened;
    protected bool ToEnd = true;
    protected bool DidInit;
    protected bool DidEnd = false;
    public BaseMediaPlayer()
    {
        Element1 = new MediaElement
        {
            LoadedBehavior = MediaState.Manual
        };
        Element1.MouseMove += Element1_MouseMove;
        Element1.MouseUp += Element1_MouseUp;
        Element1.MediaOpened += Element1_MediaOpened;
        Element1.MediaFailed += Element1_MediaFailed;
        Element1.MediaFailed += Element1_MediaFailed;
        Element1.SourceUpdated += Element1_SourceUpdated;
        Element1.MediaEnded += Element1_MediaEnded;
        AfterNew();
    }
    private void Element1_MediaFailed(object? sender, ExceptionRoutedEventArgs e)
    {
        ErrorRaised?.Invoke($"Media failed   Message was {e.ErrorException}");
    }
    private void Element1_MediaEnded(object sender, RoutedEventArgs e)
    {
        OnMediaEnded();
    }
    protected virtual void OnMediaEnded()
    {
        DidEnd = true;
    }
    private void Element1_SourceUpdated(object? sender, DataTransferEventArgs e)
    {
        TempPath = Element1.Source.AbsolutePath;
        _isOpened = false;
    }
    protected virtual void AfterNew() { }
    private void Element1_MediaOpened(object sender, RoutedEventArgs e)
    {
        if (_isOpened.HasValue == true)
        {
            _isOpened = true;
        }
    }
    private void Element1_MouseUp(object sender, MouseButtonEventArgs e)
    {
        OnMouseUp();
    }
    private void Element1_MouseMove(object? sender, MouseEventArgs e)
    {
        OnMouseMove();
    }
    public string Path
    {
        get
        {
            return Element1.Source.AbsolutePath;
        }
        set
        {
            if (value.Equals(TempPath))
            {
                return;
            }
            TempPath = value;
            Element1.Source = new Uri(value);
            _isOpened = false;
        }
    }
    public int Volume
    {
        get
        {
            return Convert.ToInt32(Element1.Volume * 100);
        }
        set
        {
            if (Volume == 0)
            {
                Element1.Volume = 0;
                Element1.ScrubbingEnabled = true;
                return;
            }
            Element1.Volume = value / 100;
        }
    }
    public int DefaultVolume
    {
        get
        {
            return DefaultVolumeValue;
        }
        set
        {
            DefaultVolumeValue = value;
        }
    }
    public int MaxVolume { get => 100; }
    public void SetPathBinding(string path)
    {
        Binding thisBind = new(path)
        {
            Converter = (IValueConverter)_thisC
        };
        Element1.SetBinding(MediaElement.SourceProperty, thisBind);
    }
    public void Pause()
    {
        IsPlaying = !IsPlaying;
        if (!IsPlaying)
        {
            Element1.Pause();
            AfterPause();
        }
        else
        {
            Element1.Play();
            AfterResumePlay();
        }
    }
    public virtual void StopPlay()
    {
        IsPlaying = false;
        Element1.Stop();
    }
    private int _privateL = -1;
    public int Length()
    {
        if (_privateL > -1)
        {
            return _privateL;
        }

        if (FileExists(TempPath) == false)
        {
            throw new CustomBasicException($"Path At {TempPath} Does Not Exist");
        }

        return LengthModule.Length(TempPath);
    }
    public event BasicDataFunctions.ErrorRaisedEventHandler? ErrorRaised;
    public virtual void AfterStartPlay() { }
    protected virtual void AfterPause() { }
    protected virtual void AfterResumePlay() { }
    public virtual bool IsPaused()
    {
        return !Element1.CanPause;
    }
    protected virtual void OnMouseUp() { }
    protected virtual void OnMouseMove() { }
    public virtual bool IsFinished()
    {
        if (DidEnd == true)
        {
            DidEnd = false;
            return true;
        }
        return false;
    }
    public int TimeElapsedSeconds()
    {
        double time;
        time = Element1.Position.TotalSeconds;
        return Convert.ToInt32(time);
    }
    public async Task PlayAsync()
    {
        await PlayAsync(Length(), 0);
    }
    public async Task PlayAsync(int position)
    {
        await PlayAsync(Length(), position);
    }
    public async Task PlayAsync(int length, int position)
    {
        DidEnd = false;
        if (position > length)
        {
            return;
        }
        try
        {
            LengthValue = TimeSpan.FromSeconds(length);
            _privateL = length;
            PositionValue = TimeSpan.FromSeconds(position);
            MediaLength = LengthModule.Length(TempPath);
            IsPlaying = true;
            bool sets = false;

            if (!(PositionValue == TimeSpan.Zero))
            {
                Element1.Play();
                do
                {
                    await Task.Delay(10);
                    if (_isOpened == true && sets == false)
                    {
                        Element1.Position = PositionValue;
                        Element1.Play();
                        sets = true;
                    }
                    else if (_isOpened == true || Element1.Position.TotalSeconds >= position)
                        break;

                } while (true);
            }
            else
                Element1.Play();
            DidInit = true;
            AfterStartPlay(); 
        }
        catch
        {

        }
    }
}