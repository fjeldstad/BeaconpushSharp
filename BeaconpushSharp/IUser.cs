namespace BeaconpushSharp
{
    public interface IUser
    {
        string Username { get; }
        bool IsOnline();
        void ForceSignOut();
        void Send(object message);
    }
}