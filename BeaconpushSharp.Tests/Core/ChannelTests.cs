using System;
using System.Net;
using BeaconpushSharp.Core;
using BeaconpushSharp.ResponseData;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace BeaconpushSharp.Tests.Core
{
    [TestFixture]
    public class ChannelTests
    {
        [Test]
        public void ChannelImplementsIChannel()
        {
            Assert.That(typeof(IChannel).IsAssignableFrom(typeof(Channel)));
        }

        [Test]
        public void ChannelInheritsFromEntityBase()
        {
            Assert.That(typeof(EntityBase).IsAssignableFrom(typeof(Channel)));
        }

        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();

            Assert.Throws<ArgumentNullException>(() => new Channel(null, requestFactory, jsonSerializer, restClient));
            Assert.Throws<ArgumentNullException>(() => new Channel(string.Empty, requestFactory, jsonSerializer, restClient));
        }

        [Test]
        public void ConstructorSetsProperties()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();

            var channel = new Channel("name", requestFactory, jsonSerializer, restClient);

            Assert.That(channel.Name, Is.EqualTo("name"));
        }

        [Test]
        public void SendThrowsOnNullArguments()
        {
            var channel = GetChannel("name");

            Assert.Throws<ArgumentNullException>(() => channel.Send(null));
        }

        [Test]
        public void SendExecutesCorrectRequest()
        {
            var channel = GetChannel("name");
            channel.JsonSerializer.Stub(j => j.Serialize("message")).Return("message");
            var request = MockRepository.GenerateStub<IRequest>();
            channel.RequestFactory.Stub(r => r.CreateSendMessageToChannelRequest(channel.Name, "message")).Return(request);

            channel.Send("message");

            channel.RestClient.AssertWasCalled(r => r.Execute(request));
        }

        [Test]
        public void UsersExecutesCorrectRequest()
        {
            var channel = GetChannel("name");
            var request = MockRepository.GenerateStub<IRequest>();
            channel.RequestFactory.Stub(r => r.CreateUsersInChannelRequest(channel.Name)).Return(request);

            try
            {
                channel.Users();
            }
            catch {}

            channel.RestClient.AssertWasCalled(r => r.Execute(request));
        }

        [Test]
        public void UsersReturnsCorrectDataWhenUsersExist()
        {
            var channel = GetChannel("name");
            var request = MockRepository.GenerateStub<IRequest>();
            channel.RequestFactory.Stub(r => r.CreateUsersInChannelRequest(channel.Name)).Return(request);
            var response = new Response
            {
                Status = HttpStatusCode.OK,
                Body = "test"
            };
            channel.RestClient.Stub(r => r.Execute(request)).Return(response);
            var data = new UsersInChannelData()
                       {
                           users = new[]
                                   {
                                       "username1",
                                       "username2",
                                       "username3"
                                   }
                       };
            channel.JsonSerializer.Stub(j => j.Deserialize<UsersInChannelData>(Arg<string>.Is.Anything)).Return(data);

            var users = channel.Users();

            Assert.That(users.Select(u => u.Username).SequenceEqual(data.users));
        }

        [Test]
        public void UsersReturnsCorrectDataWhenNoUsersExist()
        {
            var channel = GetChannel("name");
            var request = MockRepository.GenerateStub<IRequest>();
            channel.RequestFactory.Stub(r => r.CreateUsersInChannelRequest(channel.Name)).Return(request);
            var response = new Response
            {
                Status = HttpStatusCode.OK,
                Body = "test"
            };
            channel.RestClient.Stub(r => r.Execute(request)).Return(response);
            var data = new UsersInChannelData();
            channel.JsonSerializer.Stub(j => j.Deserialize<UsersInChannelData>(Arg<string>.Is.Anything)).Return(data);

            var users = channel.Users();

            Assert.That(users, Is.Empty);
        }

        private TestChannel GetChannel(string name)
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();
            return new TestChannel(name, requestFactory, jsonSerializer, restClient);
        }

        private class TestChannel : Channel
        {
            public new IRequestFactory RequestFactory
            {
                get { return base.RequestFactory; }
            }

            public new IJsonSerializer JsonSerializer
            {
                get { return base.JsonSerializer; }
            }

            public new IRestClient RestClient
            {
                get { return base.RestClient; }
            }

            public TestChannel(string name, IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
                : base(name, requestFactory, jsonSerializer, restClient)
            {
            }
        }
    }
}