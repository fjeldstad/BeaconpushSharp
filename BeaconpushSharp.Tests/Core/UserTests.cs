using System;
using System.Net;
using BeaconpushSharp.Core;
using BeaconpushSharp.ResponseData;
using NUnit.Framework;
using Rhino.Mocks;

namespace BeaconpushSharp.Tests.Core
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void UserImplementsIUser()
        {
            Assert.That(typeof(IUser).IsAssignableFrom(typeof(User)));
        }

        [Test]
        public void UserInheritsFromEntityBase()
        {
            Assert.That(typeof(EntityBase).IsAssignableFrom(typeof(User)));
        }

        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();

            Assert.Throws<ArgumentNullException>(() => new User(null, requestFactory, jsonSerializer, restClient));
            Assert.Throws<ArgumentNullException>(() => new User(string.Empty, requestFactory, jsonSerializer, restClient));
        }

        [Test]
        public void ConstructorSetsProperties()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();

            var user = new User("username", requestFactory, jsonSerializer, restClient);

            Assert.That(user.Username, Is.EqualTo("username"));
        }

        [Test]
        public void IsOnlineExecutesCorrectRequest()
        {
            var user = GetUser("username");
            var request = MockRepository.GenerateStub<IRequest>();
            user.RequestFactory.Stub(r => r.CreateIsUserOnlineRequest(user.Username)).Return(request);

            try
            {
                user.IsOnline();
            }
            catch { }

            user.RestClient.AssertWasCalled(r => r.Execute(request));
        }

        [Test]
        public void IsOnlineReturnsTrueOnHttpOk()
        {
            var user = GetUser("username");
            var request = MockRepository.GenerateStub<IRequest>();
            user.RequestFactory.Stub(r => r.CreateIsUserOnlineRequest(user.Username)).Return(request);
            var response = new Response
            {
                Status = HttpStatusCode.OK
            };
            user.RestClient.Stub(r => r.Execute(request)).Return(response);

            bool isOnline = user.IsOnline();

            Assert.That(isOnline, Is.True);
        }

        [Test]
        public void IsOnlineReturnsFalseOnHttpNotFound()
        {
            var user = GetUser("username");
            var request = MockRepository.GenerateStub<IRequest>();
            user.RequestFactory.Stub(r => r.CreateIsUserOnlineRequest(user.Username)).Return(request);
            var response = new Response
                           {
                               Status = HttpStatusCode.NotFound
                           };
            user.RestClient.Stub(r => r.Execute(request)).Return(response);

            bool isOnline = user.IsOnline();

            Assert.That(isOnline, Is.False);
        }

        [Test]
        public void ForceSignOutExecutesCorrectRequest()
        {
            var user = GetUser("username");
            var request = MockRepository.GenerateStub<IRequest>();
            user.RequestFactory.Stub(r => r.CreateForceUserSignOutRequest(user.Username)).Return(request);

            user.ForceSignOut();

            user.RestClient.AssertWasCalled(r => r.Execute(request));
        }

        [Test]
        public void SendThrowsOnNullArguments()
        {
            var user = GetUser("username");

            Assert.Throws<ArgumentNullException>(() => user.Send(null));
        }

        [Test]
        public void SendExecutesCorrectRequest()
        {
            var user = GetUser("username");
            user.JsonSerializer.Stub(j => j.Serialize("message")).Return("message");
            var request = MockRepository.GenerateStub<IRequest>();
            user.RequestFactory.Stub(r => r.CreateSendMessageToUserRequest(user.Username, "message")).Return(request);

            user.Send("message");

            user.RestClient.AssertWasCalled(r => r.Execute(request));
        }

        private TestUser GetUser(string username)
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();
            return new TestUser(username, requestFactory, jsonSerializer, restClient);
        }

        private class TestUser : User
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

            public TestUser(string username, IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
                : base(username, requestFactory, jsonSerializer, restClient)
            {
            }
        }
    }
}