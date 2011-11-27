using System.Net;
using System;

namespace BeaconpushSharp.Core
{
    public class Request : IRequest
    {
        private readonly WebHeaderCollection _headers = new WebHeaderCollection();

        public WebHeaderCollection Headers
        {
            get { return _headers; }
        }

        public HttpVerb Method { get; set; }

        public Uri Url { get; set; }

        public string Body { get; set; }
    }
}