using System;
using System.Collections.Specialized;

namespace BeaconpushSharp.Core
{
    public class Request : IRequest
    {
        private readonly NameValueCollection _headers = new NameValueCollection();

        public NameValueCollection Headers
        {
            get { return _headers; }
        }

        public HttpVerb Method { get; set; }

        public Uri Url { get; set; }

        public string Body { get; set; }
    }
}