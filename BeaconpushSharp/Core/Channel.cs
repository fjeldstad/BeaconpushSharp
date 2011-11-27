using System;
using System.Collections.Generic;
using BeaconpushSharp.Core;
using BeaconpushSharp.ResponseData;
using System.Linq;

namespace BeaconpushSharp.Core
{
    public class Channel : EntityBase, IChannel
    {
        private readonly string _name;

        public Channel(string name, IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
            : base(requestFactory, jsonSerializer, restClient)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public void Send(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            var data = JsonSerializer.Serialize(message);
            var request = RequestFactory.CreateSendMessageToChannelRequest(Name, data);
            RestClient.Execute(request);
        }

        public IUser[] Users()
        {
            var request = RequestFactory.CreateUsersInChannelRequest(Name);
            var response = RestClient.Execute(request);
            var data = JsonSerializer.Deserialize<UsersInChannelData>(response.Body);
            return data.users.Select(username => new User(username, RequestFactory, JsonSerializer, RestClient)).ToArray();
        }
    }
}