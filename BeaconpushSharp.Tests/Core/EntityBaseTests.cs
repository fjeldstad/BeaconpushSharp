using System;
using System.Net;
using BeaconpushSharp.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace BeaconpushSharp.Tests.Core
{
    public class EntityBaseTests
    {
        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();

            Assert.Throws<ArgumentNullException>(() => new TestEntity(null, jsonSerializer, restClient));
            Assert.Throws<ArgumentNullException>(() => new TestEntity(requestFactory, null, restClient));
            Assert.Throws<ArgumentNullException>(() => new TestEntity(requestFactory, jsonSerializer, null));
        }

        [Test]
        public void ConstructorSetsProperties()
        {
            var entity = GetEntityBase();

            Assert.That(entity.RequestFactory, Is.Not.Null);
            Assert.That(entity.JsonSerializer, Is.Not.Null);
            Assert.That(entity.RestClient, Is.Not.Null);
        }

        [Test]
        public void ThrowOnUnexpectedStatusCodeDoesNotThrowOnCommonExpectedStatusCodes()
        {
            var entity = GetEntityBase();

            Assert.DoesNotThrow(() => entity.ThrowOnUnexpectedStatusCode(new Response { Status = HttpStatusCode.OK }));
            Assert.DoesNotThrow(() => entity.ThrowOnUnexpectedStatusCode(new Response { Status = HttpStatusCode.Created }));
            Assert.DoesNotThrow(() => entity.ThrowOnUnexpectedStatusCode(new Response { Status = HttpStatusCode.NoContent }));
        }

        [Test]
        public void ThrowOnUnexpectedStatusCodeThrowsOnNullArguments()
        {
            var entity = GetEntityBase();
            var response = MockRepository.GenerateStub<IResponse>();
            var statusCodes = new[] { HttpStatusCode.OK };

            Assert.Throws<ArgumentNullException>(() => entity.ThrowOnUnexpectedStatusCode(null, statusCodes));
            Assert.Throws<ArgumentNullException>(() => entity.ThrowOnUnexpectedStatusCode(response, null));
        }

        [Test]
        public void ThrowOnUnexpectedStatusCodeThrowsOnHttpBadRequest()
        {
            var entity = GetEntityBase();

            Assert.Throws<BeaconpushException>(() => entity.ThrowOnUnexpectedStatusCode(new Response { Status = HttpStatusCode.BadRequest }));
        }

        [Test]
        public void ThrowOnUnexpectedStatusCodeThrowsOnHttpNotFound()
        {
            var entity = GetEntityBase();

            Assert.Throws<BeaconpushException>(() => entity.ThrowOnUnexpectedStatusCode(new Response { Status = HttpStatusCode.NotFound }));
        }

        private TestEntity GetEntityBase()
        {
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();
            return new TestEntity(requestFactory, jsonSerializer, restClient);
        }

        private class TestEntity : EntityBase
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

            public TestEntity(IRequestFactory requestFactory, IJsonSerializer jsonSerializer, IRestClient restClient)
                : base(requestFactory, jsonSerializer, restClient)
            {
            }

            public new void ThrowOnUnexpectedStatusCode(IResponse response)
            {
                base.ThrowOnUnexpectedStatusCode(response);
            }

            public new void ThrowOnUnexpectedStatusCode(IResponse response, params HttpStatusCode[] expectedStatusCodes)
            {
                base.ThrowOnUnexpectedStatusCode(response, expectedStatusCodes);
            }
        }
    }
}