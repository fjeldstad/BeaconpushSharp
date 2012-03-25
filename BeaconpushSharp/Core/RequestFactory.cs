using System;
using System.Net;
using System.Web;

namespace BeaconpushSharp.Core
{
    public class RequestFactory : IRequestFactory
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _baseUrl;

        protected string ApiKey
        {
            get { return _apiKey; }
        }

        protected string SecretKey
        {
            get { return _secretKey; }
        }

        protected string BaseUrl
        {
            get { return _baseUrl; }
        }

        public RequestFactory(string apiKey, string secretKey, string baseUrl)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException("apiKey");
            }
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("secretKey");
            }
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException("baseUrl");
            }
            _apiKey = apiKey;
            _secretKey = secretKey;
            _baseUrl = baseUrl;
        }

        protected virtual IRequest CreateRequest(string resource)
        {
            return CreateRequest(HttpVerb.GET, resource, null);
        }

        protected virtual IRequest CreateRequest(HttpVerb method, string resource)
        {
            return CreateRequest(method, resource, null);
        }

        protected virtual IRequest CreateRequest(HttpVerb method, string resource, string body)
        {
            var request = new Request();
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("X-Beacon-Secret-Key", SecretKey);
            request.Method = method;
            request.Url = new Uri(BaseUrl.TrimEnd('/') + "/" + ApiKey + "/" + resource.Replace("//", "/").TrimStart('/'));
            request.Body = body;
            return request;
        }

        public IRequest CreateOnlineUserCountRequest()
        {
            return CreateRequest("users");
        }

        public IRequest CreateIsUserOnlineRequest(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            return CreateRequest("users/" + HttpUtility.UrlEncode(username));
        }

        public IRequest CreateForceUserSignOutRequest(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            return CreateRequest(HttpVerb.DELETE, "users/" + HttpUtility.UrlEncode(username));
        }

        public IRequest CreateSendMessageToUserRequest(string username, string message)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }
            return CreateRequest(HttpVerb.POST, "users/" + HttpUtility.UrlEncode(username), message);
        }


        public IRequest CreateUsersInChannelRequest(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            return CreateRequest("channels/" + HttpUtility.UrlEncode(name));
        }

        public IRequest CreateSendMessageToChannelRequest(string name, string message)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message");
            }
            return CreateRequest(HttpVerb.POST, "channels/" + HttpUtility.UrlEncode(name), message);
        }
    }
}