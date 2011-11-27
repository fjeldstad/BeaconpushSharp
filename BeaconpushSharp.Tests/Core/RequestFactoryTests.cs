using System;
using BeaconpushSharp.Core;
using NUnit.Framework;

namespace BeaconpushSharp.Tests.Core
{
    [TestFixture]
    public class RequestFactoryTests
    {
        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestFactory(null, "test"));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory(string.Empty, "test"));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory("test", null));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory("test", string.Empty));
        }

        [Test]
        public void ConstructorSetsProperties()
        {
            var requestFactory = new TestRequestFactory("apiKey", "secretKey");

            Assert.That(requestFactory.ApiKey, Is.EqualTo("apiKey"));
            Assert.That(requestFactory.SecretKey, Is.EqualTo("secretKey"));
        }

        [Test]
        public void CreateOnlineUserCountRequestReturnsCorrectRequest()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            var request = requestFactory.CreateOnlineUserCountRequest();

            request.AssertCorrectBaseUrl();
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.GET);
            request.AssertCorrectCredentials("apiKey", "secretKey");
            request.AssertCorrectPath("apiKey", "users");
            Assert.That(request.Body.IsNullOrEmpty());
        }

        [Test]
        public void CreateIsUserOnlineRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateIsUserOnlineRequest(null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateIsUserOnlineRequest(string.Empty));
        }

        [Test]
        public void CreateIsUserOnlineRequestReturnsCorrectRequest()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            var request = requestFactory.CreateIsUserOnlineRequest("username");

            request.AssertCorrectBaseUrl();
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.GET);
            request.AssertCorrectCredentials("apiKey", "secretKey");
            request.AssertCorrectPath("apiKey", "users/username");
            Assert.That(request.Body.IsNullOrEmpty());
        }

        [Test]
        public void CreateForceUserSignOutRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateForceUserSignOutRequest(null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateForceUserSignOutRequest(string.Empty));
        }

        [Test]
        public void CreateForceUserSignOutRequestReturnsCorrectRequest()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            var request = requestFactory.CreateForceUserSignOutRequest("username");

            request.AssertCorrectBaseUrl();
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.DELETE);
            request.AssertCorrectCredentials("apiKey", "secretKey");
            request.AssertCorrectPath("apiKey", "users/username");
            Assert.That(request.Body.IsNullOrEmpty());
        }

        [Test]
        public void CreateSendMessageToUserRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest(null, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest(string.Empty, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest("username", null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest("username", string.Empty));
        }

        [Test]
        public void CreateSendMessageToUserRequestReturnsCorrectRequest()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            var request = requestFactory.CreateSendMessageToUserRequest("username", "message");

            request.AssertCorrectBaseUrl();
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.POST);
            request.AssertCorrectCredentials("apiKey", "secretKey");
            request.AssertCorrectPath("apiKey", "users/username");
            Assert.That(request.Body, Is.EqualTo("message"));
        }

        [Test]
        public void CreateUsersInChannelRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateUsersInChannelRequest(null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateUsersInChannelRequest(string.Empty));
        }

        [Test]
        public void CreateUsersInChannelRequestReturnsCorrectRequest()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            var request = requestFactory.CreateUsersInChannelRequest("name");

            request.AssertCorrectBaseUrl();
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.GET);
            request.AssertCorrectCredentials("apiKey", "secretKey");
            request.AssertCorrectPath("apiKey", "channels/name");
            Assert.That(request.Body.IsNullOrEmpty());
        }

        [Test]
        public void CreateSendMessageToChannelRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest(null, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest(string.Empty, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest("name", null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest("name", string.Empty));
        }

        [Test]
        public void CreateSendMessageToChannelRequestReturnsCorrectRequest()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey");

            var request = requestFactory.CreateSendMessageToChannelRequest("name", "message");

            request.AssertCorrectBaseUrl();
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.POST);
            request.AssertCorrectCredentials("apiKey", "secretKey");
            request.AssertCorrectPath("apiKey", "channels/name");
            Assert.That(request.Body, Is.EqualTo("message"));
        }

        private class TestRequestFactory : RequestFactory
        {
            public new string ApiKey
            {
                get { return base.ApiKey; }
            }

            public new string SecretKey
            {
                get { return base.SecretKey; }
            }

            public TestRequestFactory(string apiKey, string secretKey)
                : base(apiKey, secretKey)
            {
            }
        }
    }
}