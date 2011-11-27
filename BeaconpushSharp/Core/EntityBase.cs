using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeaconpushSharp.Core
{
    public abstract class EntityBase
    {
        private readonly IRequestFactory _requestFactory;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRestClient _restClient;

        protected IRequestFactory RequestFactory
        {
            get { return _requestFactory; }
        }

        protected IJsonSerializer JsonSerializer
        {
            get { return _jsonSerializer; }
        }

        protected IRestClient RestClient
        {
            get { return _restClient; }
        }

        protected EntityBase(IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
        {
            if (requestFactory == null)
            {
                throw new ArgumentNullException("requestFactory");
            }
            if (jsonSerializer == null)
            {
                throw new ArgumentNullException("jsonSerializer");
            }
            if (restClient == null)
            {
                throw new ArgumentNullException("restClient");
            }
            _requestFactory = requestFactory;
            _jsonSerializer = jsonSerializer;
            _restClient = restClient;
        } 
    }
}
