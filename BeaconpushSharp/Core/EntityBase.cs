using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using BeaconpushSharp.ResponseData;

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

        protected void ThrowOnUnexpectedStatusCode(IResponse response)
        {
            ThrowOnUnexpectedStatusCode(
                response, 
                HttpStatusCode.OK, 
                HttpStatusCode.Created, 
                HttpStatusCode.NoContent);
        }

        protected void ThrowOnUnexpectedStatusCode(IResponse response, params HttpStatusCode[] expectedStatusCodes)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            if (expectedStatusCodes == null)
            {
                throw new ArgumentNullException("expectedStatusCodes");
            }
            if (!expectedStatusCodes.Any(status => status == response.Status))
            {
                ErrorData errorData = null;
                try
                {
                    errorData = JsonSerializer.Deserialize<ErrorData>(response.Body);
                }
                catch {}
                if (errorData != null)
                {
                    throw new BeaconpushException(errorData.status, errorData.message);
                }
                throw new BeaconpushException((int)response.Status, response.Body);
            }
        }
    }
}
