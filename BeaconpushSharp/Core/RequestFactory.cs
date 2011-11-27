﻿using System;
using System.Net;
using System.Web;

namespace BeaconpushSharp.Core
{
    public class RequestFactory : IRequestFactory
    {
        protected static readonly string BaseUrl = "http://api.beaconpush.com/1.0.0/";

        private readonly string _apiKey;
        private readonly string _secretKey;

        protected string ApiKey
        {
            get { return _apiKey; }
        }

        protected string SecretKey
        {
            get { return _secretKey; }
        }

        public RequestFactory(string apiKey, string secretKey)
        {
            if (apiKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException("apiKey");
            }
            if (secretKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException("secretKey");
            }
            _apiKey = apiKey;
            _secretKey = secretKey;
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
            request.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            request.Headers.Add("X-Beacon-Secret-Key", SecretKey);
            request.Method = method;
            request.Url = new Uri(BaseUrl + ApiKey + "/" + resource.Replace("//", "/"));
            request.Body = body;
            return request;
        }

        public IRequest CreateOnlineUserCountRequest()
        {
            return CreateRequest("users");
        }

        public IRequest CreateIsUserOnlineRequest(string username)
        {
            if (username.IsNullOrEmpty())
            {
                throw new ArgumentNullException("username");
            }
            return CreateRequest("users/" + HttpUtility.UrlEncode(username));
        }

        public IRequest CreateForceUserSignOutRequest(string username)
        {
            if (username.IsNullOrEmpty())
            {
                throw new ArgumentNullException("username");
            }
            return CreateRequest(HttpVerb.DELETE, "users/" + HttpUtility.UrlEncode(username));
        }

        public IRequest CreateSendMessageToUserRequest(string username, string message)
        {
            if (username.IsNullOrEmpty())
            {
                throw new ArgumentNullException("username");
            }
            if (message.IsNullOrEmpty())
            {
                throw new ArgumentNullException("message");
            }
            return CreateRequest(HttpVerb.POST, "users/" + HttpUtility.UrlEncode(username), message);
        }


        public IRequest CreateUsersInChannelRequest(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            return CreateRequest("channels/" + HttpUtility.UrlEncode(name));
        }

        public IRequest CreateSendMessageToChannelRequest(string name, string message)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            if (message.IsNullOrEmpty())
            {
                throw new ArgumentNullException("message");
            }
            return CreateRequest(HttpVerb.POST, "channels/" + HttpUtility.UrlEncode(name), message);
        }
    }
}