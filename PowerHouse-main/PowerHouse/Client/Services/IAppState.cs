namespace PowerHouse.Client.Services
{
    public interface IAppState
    {
        bool LoggedIn { get; set; }

        event Action OnChange;

        void NotifyStateChanged();
    }
}