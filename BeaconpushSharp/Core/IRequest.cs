using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace BeaconpushSharp.Core
{
    public interface IRequest
    {
        WebHeaderCollection Headers { get; }
        HttpVerb Method { get; set; }
        Uri Url { get; set; }
        string Body { get; set; }
    }
}