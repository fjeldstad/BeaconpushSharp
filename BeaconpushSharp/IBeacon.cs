using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeaconpushSharp
{
    public interface IBeacon
    {
        long OnlineUserCount();
        IChannel Channel(string name);
        IUser User(string name);
    }
}
