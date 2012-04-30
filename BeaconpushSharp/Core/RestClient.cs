using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;

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
            SetHeaders(httpRequest, request.Headers);
            if (!string.IsNullOrEmpty(request.Body))
            {
                var bodyBytes = Encoding.UTF8.GetBytes(request.Body);
                httpRequest.ContentLength = bodyBytes.Length;
                using (var requestStream = httpRequest.GetRequestStream())
                {
                    requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                }
            }
            HttpWebResponse httpResponse = null;
            var response = new Response();
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            } 
            catch (WebException ex)
            {
                // Catch WebException to return the response and let
                // the caller handle the status code.
                if (ex.Response == null ||
                    !(ex.Response is HttpWebResponse))
                {
                    throw;
                }
                httpResponse = (HttpWebResponse)ex.Response;
            }

            response.Status = httpResponse.StatusCode;
            using (var responseStream = httpResponse.GetResponseStream())
            {
                if (responseStream != null)
                {
                    var reader = new StreamReader(responseStream, Encoding.UTF8);
                    response.Body = reader.ReadToEnd();
                }
            }

            return response;
        }

        protected static void SetHeaders(HttpWebRequest httpRequest, NameValueCollection headers)
        {
            httpRequest.Headers.Clear();
            foreach (var name in headers.AllKeys)
            {
                switch (name)
                {
                    case "Content-Type":
                        httpRequest.ContentType = headers[name];
                        break;

                    // TODO Add handling of more "protected" headers if needed (see http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.headers.aspx)

                    default:
                        httpRequest.Headers.Add(name, headers[name]);
                        break;
                }
            }
        }
    }
}