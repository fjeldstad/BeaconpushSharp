namespace BeaconpushSharp.Core
{
    public interface IRequestFactory
    {
        IRequest CreateOnlineUserCountRequest();
        IRequest CreateIsUserOnlineRequest(string username);
        IRequest CreateForceUserSignOutRequest(string username);
        IRequest CreateSendMessageToUserRequest(string username, string message);
        IRequest CreateUsersInChannelRequest(string name);
        IRequest CreateSendMessageToChannelRequest(string name, string message);
    }
}