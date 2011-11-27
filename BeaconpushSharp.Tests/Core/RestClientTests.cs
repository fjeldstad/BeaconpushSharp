using System;
using NUnit.Framework;
using BeaconpushSharp.Core;
using Rhino.Mocks;

namespace BeaconpushSharp.Tests.Core
{
    [TestFixture]
    public class RestClientTests
    {
        [Test]
        public void ExecuteThrowsOnNullArgument()
        {
            var restClient = new RestClient();

            Assert.Throws<ArgumentNullException>(() => restClient.Execute(null));
        }

        [Test]
        public void ExecuteThrowsOnInvalidRequestUrl()
        {
            var restClient = new RestClient();
            var request = MockRepository.GenerateStub<IRequest>();
            request.Url = null;

            Assert.Throws<ArgumentException>(() => restClient.Execute(request));
        }
    }
}