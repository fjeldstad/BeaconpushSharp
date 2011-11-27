using BeaconpushSharp.Core;
using NUnit.Framework;

namespace BeaconpushSharp.Tests.Core
{
    public static class IRequestExtensions
    {
        private static string _baseUrl = "http://api.beaconpush.com/1.0.0/";

        public static void AssertCorrectBaseUrl(this IRequest request)
        {
            Assert.That(request.Url.ToString().StartsWith(_baseUrl));
        }

        public static void AssertCorrectCredentials(this IRequest request, string apiKey, string secretKey)
        {
            Assert.That(request.Headers["X-Beacon-Secret-Key"], Is.EqualTo(secretKey));
            Assert.That(request.Url.ToString().StartsWith(_baseUrl + apiKey + "/"));
        }

        public static void AssertCorrectContentType(this IRequest request)
        {
            Assert.That(request.Headers["Content-Type"], Is.EqualTo("application/json"));
        }

        public static void AssertCorrectPath(this IRequest request, string apiKey, string expectedPath)
        {
            Assert.That(request.Url.ToString(), Is.EqualTo(_baseUrl + apiKey + "/" + expectedPath));
        }

        public static void AssertCorrectMethod(this IRequest request, HttpVerb method)
        {
            Assert.That(request.Method, Is.EqualTo(method));
        }
    }
}