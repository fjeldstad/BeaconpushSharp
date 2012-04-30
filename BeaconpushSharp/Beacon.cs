using System;
using BeaconpushSharp.Core;
using BeaconpushSharp.ResponseData;

namespace BeaconpushSharp
{
    public class Beacon : EntityBase, IBeacon
    {
        public Beacon(string apiKey, string secretKey, string baseUrl)
            : this(new RequestFactory(apiKey, secretKey, baseUrl), new DefaultJsonSerializer(), new RestClient())
        {
        }

        public Beacon(string operatorId, string baseUrl)
            : this(new RequestFactory(operatorId, baseUrl), new DefaultJsonSerializer(), new RestClient())
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
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            return new Channel(name, RequestFactory, JsonSerializer, RestClient);
        }

        public IUser User(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            return new User(username, RequestFactory, JsonSerializer, RestClient);
        }
    }
}