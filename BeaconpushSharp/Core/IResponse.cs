using System.Net;

namespace BeaconpushSharp.Core
{
    public interface IResponse
    {
        HttpStatusCode Status { get; set; }
        string Body { get; set; }
    }
}