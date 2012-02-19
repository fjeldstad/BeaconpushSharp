using BeaconpushSharp.Core;
using NUnit.Framework;

namespace BeaconpushSharp.Tests.Core
{
    public static class IRequestExtensions
    {
        public static void AssertCorrectSecret(this IRequest request, string secretKey)
        {
            Assert.That(request.Headers["X-Beacon-Secret-Key"], Is.EqualTo(secretKey));
        }

        public static void AssertCorrectContentType(this IRequest request)
        {
            Assert.That(request.Headers["Content-Type"], Is.EqualTo("application/json"));
        }

        public static void AssertCorrectUrl(this IRequest request, string baseUrl, string apiKey, string secretKey, string expectedPath)
        {
            var expectedUrl = string.Format("{0}/{1}/{2}", baseUrl.TrimEnd('/'), apiKey, expectedPath.Replace("//", "/").TrimStart('/').TrimEnd('/'));
            Assert.That(request.Url.ToString(), Is.EqualTo(expectedUrl));
        }

        public static void AssertCorrectMethod(this IRequest request, HttpVerb method)
        {
            Assert.That(request.Method, Is.EqualTo(method));
        }
    }
}