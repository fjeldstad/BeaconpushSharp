using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BeaconpushSharp.Core;
using NUnit.Framework;
using Rhino.Mocks;
using BeaconpushSharp.ResponseData;

namespace BeaconpushSharp.Tests
{
    [TestFixture]
    public class BeaconTests
    {
        [Test]
        public void BeaconImplementsIBeacon()
        {
            Assert.That(typeof (IBeacon).IsAssignableFrom(typeof (Beacon)));
        }

        [Test]
        public void BeaconInheritsFromEntityBase()
        {
            Assert.That(typeof (EntityBase).IsAssignableFrom(typeof (Beacon)));
        }

        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new Beacon(null, "test", "http://example.com"));
            Assert.Throws<ArgumentNullException>(() => new Beacon(string.Empty, "test", "http://example.com"));
            Assert.Throws<ArgumentNullException>(() => new Beacon("test", null, "http://example.com"));
            Assert.Throws<ArgumentNullException>(() => new Beacon("test", string.Empty, "http://example.com"));
            Assert.Throws<ArgumentNullException>(() => new Beacon("test", "test", null));
            Assert.Throws<ArgumentNullException>(() => new Beacon("test", "test", string.Empty));
        }

        [Test]
        public void OnlineUserCountExecutesCorrectRequest()
        {
            var beacon = GetBeacon();
            var request = MockRepository.GenerateStub<IRequest>();
            beacon.RequestFactory.Stub(r => r.CreateOnlineUserCountRequest()).Return(request);

            try
            {
                beacon.OnlineUserCount();
            }
            catch {}

            beacon.RestClient.AssertWasCalled(r => r.Execute(request));
        }

        [Test]
        public void OnlineUserCountReturnsCorrectValue()
        {
            var beacon = GetBeacon();
            var request = MockRepository.GenerateStub<IRequest>();
            beacon.RequestFactory.Stub(r => r.CreateOnlineUserCountRequest()).Return(request);
            var response = new Response
                           {
                               Status = HttpStatusCode.OK,
                               Body = "test"
                           };
            beacon.RestClient.Stub(r => r.Execute(request)).Return(response);
            var data = new OnlineUserCountData { online = 42 };
            beacon.JsonSerializer.Stub(j => j.Deserialize<OnlineUserCountData>(Arg<string>.Is.Anything)).Return(data);

            var count = beacon.OnlineUserCount();

            Assert.That(count, Is.EqualTo(data.online));
        }

        [Test]
        public void OnlineUserCountThrowsOnUnexpectedResponseStatus()
        {
            var beacon = GetBeacon();
            var request = MockRepository.GenerateStub<IRequest>();
            beacon.RequestFactory.Stub(r => r.CreateOnlineUserCountRequest()).Return(request);
            var response = new Response
            {
                Status = HttpStatusCode.BadRequest,
                Body = "errorMessage"
            };
            var errorData = new ErrorData
            {
                status = (int)response.Status,
                message = response.Body
            };
            beacon.JsonSerializer.Stub(j => j.Deserialize<ErrorData>(response.Body)).Return(errorData);
            beacon.RestClient.Stub(r => r.Execute(request)).Return(response);

            Assert.Throws<BeaconpushException>(() => beacon.OnlineUserCount());
        }

        [Test]
        public void ChannelThrowsOnNullArgument()
        {
            var beacon = GetBeacon();

            Assert.Throws<ArgumentNullException>(() => beacon.Channel(null));
            Assert.Throws<ArgumentNullException>(() => beacon.Channel(string.Empty));
        }

        [Test]
        public void ChannelReturnsChannel()
        {
            var beacon = GetBeacon();

            var channel = beacon.Channel("name");

            Assert.That(channel, Is.Not.Null);
            Assert.That(channel.Name, Is.EqualTo("name"));
        }

        [Test]
        public void UserThrowsOnNullArgument()
        {
            var beacon = GetBeacon();

            Assert.Throws<ArgumentNullException>(() => beacon.User(null));
            Assert.Throws<ArgumentNullException>(() => beacon.User(string.Empty));
        }

        [Test]
        public void UserReturnsUser()
        {
            var beacon = GetBeacon();

            var user = beacon.User("username");

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo("username"));
        }

        private TestBeacon GetBeacon()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();
            return new TestBeacon(requestFactory, jsonSerializer, restClient);
        }

        private class TestBeacon : Beacon
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

            public TestBeacon(string apiKey, string secretKey, string baseUrl)
                : base(apiKey, secretKey, baseUrl)
            {
            }

            public TestBeacon(IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
                : base(requestFactory, jsonSerializer, restClient)
            {
            }
        }
    }
}
