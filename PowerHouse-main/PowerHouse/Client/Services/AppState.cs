namespace PowerHouse.Client.Services;
public class AppState : IAppState
{
    private bool _loggedIn;
    public event Action OnChange;
    public bool LoggedIn
    {
        get { return _loggedIn; }
        set
        {
            if (_loggedIn != value)
            {
                _loggedIn = value;
                NotifyStateChanged();
            }
        }
    }

    public void NotifyStateChanged() => OnChange?.Invoke();
}
