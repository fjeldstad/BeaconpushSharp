using System;
using BeaconpushSharp.Core;
using NUnit.Framework;

namespace BeaconpushSharp.Tests.Core
{
    [TestFixture]
    public class DefaultJsonSerializerTests
    {
        [Test]
        public void CanRoundtripToJson()
        {
            var serializer = new DefaultJsonSerializer();
            var originalData = new Data { Message = "test", Number = 42, Assertion = true };

            string serializedData = serializer.Serialize(originalData);
            var deserializedData = serializer.Deserialize<Data>(serializedData);

            Assert.That(deserializedData.Message, Is.EqualTo(originalData.Message));
            Assert.That(deserializedData.Number, Is.EqualTo(originalData.Number));
            Assert.That(deserializedData.Assertion, Is.EqualTo(originalData.Assertion));
        }

        private class Data
        {
            public string Message { get; set; }
            public int Number { get; set; }
            public bool Assertion { get; set; }
        }
    }
}