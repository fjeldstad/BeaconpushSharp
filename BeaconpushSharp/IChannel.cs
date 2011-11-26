using System.Collections.Generic;

namespace BeaconpushSharp
{
    public interface IChannel
    {
        string Name { get; }
        void Send(object message);
        IEnumerable<IUser> Users();
    }
}