using System;
using System.Net;
using System.Text;

namespace BeaconpushSharp.Core
{
    public class RestClient : IRestClient
    {
        public IResponse Execute(IRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (request.Url == null)
            {
                throw new ArgumentException("The request URL must have a value.");
            }
            var httpRequest = (HttpWebRequest)WebRequest.Create(request.Url);
            httpRequest.Method = request.Method.ToString();
            httpRequest.Headers.Clear();
            httpRequest.Headers.Add(request.Headers);
            if (!request.Body.IsNullOrEmpty())
            {
                var bodyBytes = Encoding.UTF8.GetBytes(request.Body);
                using (var requestStream = httpRequest.GetRequestStream())
                {
                    requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                }
            }
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var response = new Response();
            response.Status = httpResponse.StatusCode;
            if (httpResponse.ContentLength > 0)
            {
                using (var responseStream = httpResponse.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        var bodyBytes = new byte[responseStream.Length];
                        response.Body = Encoding.UTF8.GetString(bodyBytes);
                    }
                }
            }
            return response;
        }
    }
}