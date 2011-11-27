using System;
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
            var requestFactory = MockRepository.GenerateStub<IRequestFactory>();
            var jsonSerializer = MockRepository.GenerateStub<IJsonSerializer>();
            var restClient = MockRepository.GenerateStub<IRestClient>();

            var entity = new TestEntity(requestFactory, jsonSerializer, restClient);

            Assert.That(entity.RequestFactory, Is.Not.Null);
            Assert.That(entity.JsonSerializer, Is.Not.Null);
            Assert.That(entity.RestClient, Is.Not.Null);
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
        }
    }
}