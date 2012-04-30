using System;
using System.Linq;
using BeaconpushSharp.Core;
using NUnit.Framework;

namespace BeaconpushSharp.Tests.Core
{
    [TestFixture]
    public class RequestFactoryTests
    {
        [Test]
        public void CloudConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestFactory(null, "test", "test"));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory(string.Empty, "test", "test"));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory("test", "test", null));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory("test", "test", string.Empty));
        }

        [Test]
        public void OnSiteConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestFactory(null, "test"));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory(string.Empty, "test"));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory("test", null));
            Assert.Throws<ArgumentNullException>(() => new RequestFactory("test", string.Empty));
        }

        [Test]
        public void CloudConstructorSetsProperties()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new TestRequestFactory(apiKey, secretKey, baseUrl);

            Assert.That(requestFactory.ApiKey, Is.EqualTo(apiKey));
            Assert.That(requestFactory.SecretKey, Is.EqualTo(secretKey));
            Assert.That(requestFactory.BaseUrl, Is.EqualTo(baseUrl));
        }

        [Test]
        public void OnSiteConstructorSetsProperties()
        {
            var baseUrl = "http://example.com";
            var operatorId = "operatorId";
            var requestFactory = new TestRequestFactory(operatorId, baseUrl);

            Assert.That(requestFactory.OperatorId, Is.EqualTo(operatorId));
            Assert.That(requestFactory.BaseUrl, Is.EqualTo(baseUrl));
        }

        [Test]
        public void AuthHeaderIsNotSetWhenSecretKeyIsNullOrEmpty()
        {
            var baseUrl = "http://example.com";
            var operatorId = "operatorId";
            var requestFactory = new TestRequestFactory(operatorId, baseUrl);

            var request = requestFactory.CreateOnlineUserCountRequest();

            Assert.That(!request.Headers.AllKeys.Contains("X-Beacon-Secret-Key"));
        }

        [Test]
        public void CreateOnlineUserCountRequestReturnsCorrectRequest()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new RequestFactory(apiKey, secretKey, baseUrl);

            var request = requestFactory.CreateOnlineUserCountRequest();

            request.AssertCorrectSecret(secretKey);
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.GET);
            request.AssertCorrectUrl(baseUrl, apiKey, "users");
            Assert.That(string.IsNullOrEmpty(request.Body));
        }

        [Test]
        public void CreateIsUserOnlineRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey", "http://example.com");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateIsUserOnlineRequest(null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateIsUserOnlineRequest(string.Empty));
        }

        [Test]
        public void CreateIsUserOnlineRequestReturnsCorrectRequest()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new RequestFactory(apiKey, secretKey, baseUrl);

            var request = requestFactory.CreateIsUserOnlineRequest("username");

            request.AssertCorrectSecret(secretKey);
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.GET);
            request.AssertCorrectUrl(baseUrl, apiKey, "users/username");
            Assert.That(string.IsNullOrEmpty(request.Body));
        }

        [Test]
        public void CreateForceUserSignOutRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey", "http://example.com");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateForceUserSignOutRequest(null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateForceUserSignOutRequest(string.Empty));
        }

        [Test]
        public void CreateForceUserSignOutRequestReturnsCorrectRequest()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new RequestFactory(apiKey, secretKey, baseUrl);

            var request = requestFactory.CreateForceUserSignOutRequest("username");

            request.AssertCorrectSecret(secretKey);
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.DELETE);
            request.AssertCorrectUrl(baseUrl, apiKey, "users/username");
            Assert.That(string.IsNullOrEmpty(request.Body));
        }

        [Test]
        public void CreateSendMessageToUserRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey", "http://example.com");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest(null, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest(string.Empty, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest("username", null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToUserRequest("username", string.Empty));
        }

        [Test]
        public void CreateSendMessageToUserRequestReturnsCorrectRequest()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new RequestFactory(apiKey, secretKey, baseUrl);

            var request = requestFactory.CreateSendMessageToUserRequest("username", "message");

            request.AssertCorrectSecret(secretKey);
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.POST);
            request.AssertCorrectUrl(baseUrl, apiKey, "users/username");
            Assert.That(request.Body, Is.EqualTo("message"));
        }

        [Test]
        public void CreateUsersInChannelRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey", "http://example.com");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateUsersInChannelRequest(null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateUsersInChannelRequest(string.Empty));
        }

        [Test]
        public void CreateUsersInChannelRequestReturnsCorrectRequest()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new RequestFactory(apiKey, secretKey, baseUrl);

            var request = requestFactory.CreateUsersInChannelRequest("name");

            request.AssertCorrectSecret(secretKey);
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.GET);
            request.AssertCorrectUrl(baseUrl, apiKey, "channels/name");
            Assert.That(string.IsNullOrEmpty(request.Body));
        }

        [Test]
        public void CreateSendMessageToChannelRequestThrowsOnNullArguments()
        {
            var requestFactory = new RequestFactory("apiKey", "secretKey", "http://example.com");

            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest(null, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest(string.Empty, "message"));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest("name", null));
            Assert.Throws<ArgumentNullException>(() => requestFactory.CreateSendMessageToChannelRequest("name", string.Empty));
        }

        [Test]
        public void CreateSendMessageToChannelRequestReturnsCorrectRequest()
        {
            var baseUrl = "http://example.com";
            var apiKey = "apiKey";
            var secretKey = "secretKey";
            var requestFactory = new RequestFactory(apiKey, secretKey, baseUrl);

            var request = requestFactory.CreateSendMessageToChannelRequest("name", "message");

            request.AssertCorrectSecret(secretKey);
            request.AssertCorrectContentType();
            request.AssertCorrectMethod(HttpVerb.POST);
            request.AssertCorrectUrl(baseUrl, apiKey, "channels/name");
            Assert.That(request.Body, Is.EqualTo("message"));
        }

        private class TestRequestFactory : RequestFactory
        {
            public new string OperatorId
            {
                get { return base.OperatorId; }
            }

            public new string ApiKey
            {
                get { return base.ApiKey; }
            }

            public new string SecretKey
            {
                get { return base.SecretKey; }
            }

            public new string BaseUrl
            {
                get { return base.BaseUrl; }
            }

            public TestRequestFactory(string apiKey, string secretKey, string baseUrl)
                : base(apiKey, secretKey, baseUrl)
            {
            }

            public TestRequestFactory(string operatorId, string baseUrl)
                : base(operatorId, baseUrl)
            {
            }
        }
    }
}