using System;
using System.Collections.Specialized;

namespace BeaconpushSharp.Core
{
    public interface IRequest
    {
        NameValueCollection Headers { get; }
        HttpVerb Method { get; set; }
        Uri Url { get; set; }
        string Body { get; set; }
    }
}