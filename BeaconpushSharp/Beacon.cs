using System;
using BeaconpushSharp.Core;
using BeaconpushSharp.ResponseData;

namespace BeaconpushSharp
{
    public class Beacon : EntityBase, IBeacon
    {
        public Beacon(string apiKey, string secretKey)
            : this(new RequestFactory(apiKey, secretKey), new DefaultJsonSerializer(), new RestClient())
        {
        }

        public Beacon(IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
            : base(requestFactory, jsonSerializer, restClient)
        {
        }

        public long OnlineUserCount()
        {
            var request = RequestFactory.CreateOnlineUserCountRequest();
            var response = RestClient.Execute(request);
            ThrowOnUnexpectedStatusCode(response);
            return JsonSerializer.Deserialize<OnlineUserCountData>(response.Body).online;
        }

        public IChannel Channel(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            return new Channel(name, RequestFactory, JsonSerializer, RestClient);
        }

        public IUser User(string username)
        {
            if (username.IsNullOrEmpty())
            {
                throw new ArgumentNullException("username");
            }
            return new User(username, RequestFactory, JsonSerializer, RestClient);
        }
    }
}