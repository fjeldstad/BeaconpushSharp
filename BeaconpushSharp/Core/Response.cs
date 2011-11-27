using System;
using System.Net;

namespace BeaconpushSharp.Core
{
    public class Response : IResponse
    {
        public HttpStatusCode Status { get; set; }

        public string Body { get; set; }
    }
}